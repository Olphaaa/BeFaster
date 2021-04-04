using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Game1.Game
{
    internal class Route
    {
        private Texture2D layerRoute1;
        private Texture2D layerRoute2;

        private Vector2 routeLane1 = new Vector2(300, -200);
        private Vector2 routeLane2 = new Vector2(490, -200);
        private Vector2 routeLane3 = new Vector2(680, -200);
        private Vector2 routeLane4 = new Vector2(870, -200);
        private bool isAccelerating;
        private int downLimit;
        private int upLimit;
        public ContentManager Content
        {
            get { return content; }
        }

        public MainCar MainCar
        {
            get { return mainCar; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        float speed;
        public List<OtherCar> Othercars
        {
            get { return othercars; }
        }
        List<OtherCar> othercars;

        //private List<OtherCar> othercars = new List<OtherCar>();

        MainCar mainCar;

        ContentManager content;
        private Rectangle test;
        private Vector2 baseScreenSize;

        private Rectangle bg1;
        private Rectangle bg2;

        public Vector2 positionAuDessus = new Vector2(0, -4035);
        private Vector2 routeLocation1 = Vector2.Zero;
        private Vector2 routeLocation2 = new Vector2(0, -4035);
        private int newY = -4035;
        private int tailleImage = 4041;
        private int rand;
        /// <summary>
        /// Contructeur d'une route + initialisation des variables
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="content"></param>
        /// <param name="baseScreenSize"></param>
        public Route(IServiceProvider serviceProvider, ContentManager content, Vector2 baseScreenSize)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            this.content = content;
            this.baseScreenSize = baseScreenSize;
            upLimit = 50;
            downLimit = 5;
            layerRoute1 = Content.Load<Texture2D>("Route/road_big");
            layerRoute2 = Content.Load<Texture2D>("Route/road_big");
            speed = 9;
            othercars = new List<OtherCar>();
            LoadCar(10, 10);

        }

        /// <summary>
        /// méthode appellée à chaque frame pour faire le traitement de la route et tout ce qui s'en suit 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="x">axe X de l'accelerateur</param>
        /// <param name="isAccelerating">savoir si on appuie sur l'écran</param>
        /// <param name="firstTouch">si le jeu a commence</param>
        public void update(GameTime gameTime, float x, bool isAccelerating, bool firstTouch)
        {
            this.isAccelerating = isAccelerating;
            if (isAccelerating)
            {
                speed = (float)(speed + 0.5);
                //Console.WriteLine("Je fonce !!");
            }
            else
            {
                speed = (float)(speed - 0.7);
                //Console.WriteLine("Je ralenti !!");
            }
            checkSpeed();
            if (firstTouch)
            {
                updateOtherCar(gameTime);
            }
            updateRoute(gameTime, x);
            if (MainCar.IsDestroyed)
                DespawnAllOtherCar();
        }
       
        /// <summary>
        /// Limite la vitesse
        /// </summary>
        private void checkSpeed()
        {
            if (speed >= upLimit)
                speed = upLimit;
            else if (speed <= downLimit)
            {
                speed = downLimit;
            }
        }
        /// <summary>
        /// permet de renseigner une vitesse minimum
        /// </summary>
        /// <param name="downLimit">limite minimum de la vitesse</param>
        public void setDownLimit(int downLimit)
        {
            this.downLimit = downLimit;
        }
        /// <summary>
        /// Méthode update de la route qui permet de faire le deffilement de la route.
        /// Appel l'update de la voiture principale
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="x">axe X de l'accelerometre</param>
        private void updateRoute(GameTime gameTime, float x)
        {
            routeLocation1.Y = routeLocation1.Y + speed;
            routeLocation2.Y = routeLocation2.Y + speed;

            if (routeLocation2.Y + tailleImage >= baseScreenSize.Y && routeLocation2.Y + tailleImage <= baseScreenSize.Y + speed)
            {
                routeLocation1.Y = positionAuDessus.Y - 1780;
            }
            if (routeLocation1.Y + tailleImage >= baseScreenSize.Y && routeLocation1.Y + tailleImage <= baseScreenSize.Y + speed)
            {
                routeLocation2.Y = positionAuDessus.Y - 1780;
            }

            mainCar.update(gameTime, x, othercars);
        }

        /// <summary>
        /// Update de toutes les autres voiture sur la route
        /// Si une autre voiture est trop loin de la voiture principale, alors elle disparait 
        /// </summary>
        /// <param name="gameTime"></param>
        private void updateOtherCar(GameTime gameTime)
        {
            randomSpawn();
            OtherCar asuppr = null;
            foreach (OtherCar oc in othercars)
            {
                oc.update(gameTime);
                oc.suivreUneVoiture();
                if (oc.Position.Y >= baseScreenSize.Y *4)
                {
                    asuppr = oc;
                }
            }
            if (asuppr != null)
            {
                othercars.Remove(asuppr);
            }
        }
        /// <summary>
        /// Permet de faire spawn des vehicule à different timing.
        /// Cette méthode evite de faire supperposer les voitures lors de l'apparition
        /// </summary>
        private void randomSpawn()
        {
            Random r = new Random();
            if (isAccelerating)
            {
                rand = r.Next(1, 15);
            }
            else
            {
                rand = r.Next(1, 50);
            }

            if (rand == 1)
            {
                OtherCar oc = new OtherCar(this, RandomRouteLane(), baseScreenSize);
                //LoadOtherCar(oc);
                foreach (OtherCar car in othercars)
                {
                    if (oc.Collide(car))
                    {
                        return;
                    }
                }
                LoadOtherCar(oc);
            }
        }

        /// <summary>
        /// Ajoute une la nouvelle voiture à la collection othercars
        /// </summary>
        /// <param name="oc">La nouvelle voiture créée</param>
        private void LoadOtherCar(OtherCar oc)
        {
            foreach(OtherCar car in othercars)
            {
                if((oc.Position.X+oc.GetLayout.Width)>car.Position.X&&(oc.Position.Y+oc.GetLayout.Height)>car.Position.Y&& (oc.Position.Y + oc.GetLayout.Height)<(car.Position.Y+car.GetLayout.Height)&& (oc.Position.X + oc.GetLayout.Width) < (car.Position.X + car.GetLayout.Width))
                {
                    return;
                }
            }
            othercars.Add(oc);
        }

        /// <summary>
        /// Enlève toutes les voitures existantes de la route 
        /// </summary>
        private void DespawnAllOtherCar()
        {
            othercars.Clear();
        }

        /// <summary>
        /// Permet de choisir aléatoirement une voie sur la route
        /// </summary>
        /// <returns></returns>
        private Vector2 RandomRouteLane()
        {
            Random r = new Random();
            int rand = r.Next(1, 5);
            switch (rand)
            {
                case 1:
                    return routeLane1;
                    break;
                case 2:
                    return routeLane2;
                    break;
                case 3:
                    return routeLane3;
                    break;
                case 4:
                    return routeLane4;
                    break;
                default:
                    return routeLane1;
                    break;
            }
        }
        /// <summary>
        /// initialise une Voiture principale
        /// </summary>
        /// <param name="x">position X</param>
        /// <param name="y">position Y</param>
        public void LoadCar(int x, int y)
        {
            mainCar = new MainCar(new Vector2(x, y), this, baseScreenSize);
        }
        /// <summary>
        /// Dessine la route et la voiture principale
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawRoute(gameTime, spriteBatch);
            MainCar.Draw(gameTime, spriteBatch);
            DrawOtherCars(gameTime, spriteBatch);
        }

        /// <summary>
        /// Dessine la route
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        private void DrawRoute(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(layerRoute1, routeLocation1, Color.White);
            spriteBatch.Draw(layerRoute2, routeLocation2, Color.White);
            
        }
        /// <summary>
        /// Dessin toutes les autre voitures
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        private void DrawOtherCars(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (OtherCar oc in othercars)
                oc.Draw(gameTime, spriteBatch);
        }
    }
}