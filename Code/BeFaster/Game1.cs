using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using BeFaster.Game;
using Xamarin.Essentials;
using System;
using Microsoft.Xna.Framework.Input.Touch;

namespace BeFaster
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Route route;
        private GameTime gametime;
        private SpriteFont fontScore;
        private SpriteFont fontText;


        private Rectangle mainFrame;
        private Matrix globalTransformation;
        private Vector2 jeu;
        private Vector2 baseScreenSize = new Vector2(1242, 2208);

        private float xAccel;
        private float yAccel;
        private float zAccel;
        
        private bool enPartie;
        private bool partieEnCours;
        private bool debutJeu;
        private bool firstTouch;
        private bool isAccelerating;
        
        private int score;

       //public static bool firstTouch { get; private set; }


        /// <summary>
        /// Constructeur du jeu principal
        /// Permet d'initialisé les valeurs
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Accelerometer.Start(SensorSpeed.Game);
            firstTouch = false;
            enPartie = false;
            partieEnCours = true;
            debutJeu = true;
        }

        /// <summary>
        /// Permet de charger les resources / contents
        /// </summary>
        protected override void LoadContent()
        {
            this.Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            route = new Route(Services, Content, baseScreenSize);
            ScalePresentationArea();
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            if (Accelerometer.IsMonitoring)
                return;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Default);

        }

        /// <summary>
        /// Li les données de l'acceleromtère du téléphone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            //route.update(gametime, e.Reading.Acceleration.X, e.Reading.Acceleration.Y, e.Reading.Acceleration.Z);
            xAccel = e.Reading.Acceleration.X;
            yAccel = e.Reading.Acceleration.Y;
            zAccel = e.Reading.Acceleration.Z;
        }

        private int backbufferWidth, backbufferHeight;

        /// <summary>
        /// Defini la taille de l'écran
        /// </summary>
        public void ScalePresentationArea()
        {
            //Work out how much we need to scale our graphics to fill the screen
            backbufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            backbufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            float horScaling = backbufferWidth / baseScreenSize.X;
            float verScaling = backbufferHeight / baseScreenSize.Y;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            globalTransformation = Matrix.CreateScale(screenScalingFactor);
            Console.WriteLine("Screen Size - Width[" + GraphicsDevice.PresentationParameters.BackBufferWidth + "] Height [" + GraphicsDevice.PresentationParameters.BackBufferHeight + "]");
        }
        
        /// <summary>
        /// Méthode appelé  chaque frame de l'application.
        /// Elle permet d'apperler par la suite chaque update du modèle 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (partieEnCours)
            {
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                // TODO: Add your update logic here
                this.gametime = gametime;
                touchTest();
                if (route.MainCar.IsDestroyed)
                    firstTouch = false;
                route.update(gameTime, xAccel, isAccelerating,firstTouch);
                if (enPartie)
                {
                    score = (int)(score + route.Speed / 15);
                    if (score >= 1000 || true)
                    {
                        route.setDownLimit(9);
                    }
                }
                if (route.MainCar.IsDestroyed)
                {
                    enPartie = false;
                    partieEnCours = false;
                }
                base.Update(gameTime);
            }
            else
            {
                touchRelancer();
            }
        }

        /// <summary>
        /// Detecte si le joueur appuie sur l'écran et lance la partie
        /// </summary>
        private void touchTest()
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)
            {
                if (touchCollection[0].State == TouchLocationState.Moved || touchCollection[0].State == TouchLocationState.Pressed)
                {
                    isAccelerating = true;
                    firstTouch = true;
                    enPartie = true;
                    debutJeu = false;
                    route.setDownLimit(9);
                }
                else
                    isAccelerating = false;
            }
        }
        /// <summary>
        /// Fin de partie pour recommencer une nouvelle
        /// </summary>
        private void touchRelancer()
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                if (touchCollection[0].State == TouchLocationState.Pressed)
                {
                    graphics.IsFullScreen = false;
                    Content.RootDirectory = "Content";
                    IsMouseVisible = true;
                    firstTouch = false;
                    enPartie = false;
                    partieEnCours = true;
                    debutJeu = true;
                    score = 0;
                    route = new Route(Services, Content, baseScreenSize);
                }
            }
        }

        /// <summary>
        /// Affiche les contents/données sur l'écran du téléphone 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, globalTransformation);

            route.Draw(gameTime, spriteBatch);
            fontScore = Content.Load<SpriteFont>("Font/Score");
            fontText = Content.Load<SpriteFont>("Font/Text");
            if (debutJeu)
            {
                jeu.X = (baseScreenSize.X / 2) - 450;
                jeu.Y = (baseScreenSize.Y / 2) - 50;
                spriteBatch.DrawString(fontText, "appuyer pour accelerer", jeu, Color.Red);
            }
            if (partieEnCours)
                spriteBatch.DrawString(fontScore, "score: " + score, Vector2.Zero, Color.WhiteSmoke);
            if (!partieEnCours)
            {
                jeu.X = (baseScreenSize.X / 2) - 125;
                jeu.Y = (baseScreenSize.Y / 2) - 50;
                spriteBatch.DrawString(fontScore, "perdu ", jeu, Color.Red);
                jeu.X = (baseScreenSize.X / 2) - 300;
                jeu.Y = jeu.Y + 100;
                spriteBatch.DrawString(fontScore, "votre score: \n" + score, jeu, Color.WhiteSmoke);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
