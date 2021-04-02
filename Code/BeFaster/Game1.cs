﻿using Game1.Game;
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
        private Texture2D textureRoute;
        private Texture2D textureVoiture;
        private Rectangle mainFrame;
        Vector2 baseScreenSize = new Vector2(1242, 2208);
        private Matrix globalTransformation;
        private AccelerometerTest accelerometre;
        private float xAccel;
        private float yAccel;
        private float zAccel;

        //public static bool firstTouch { get; private set; }
        public bool FirstTouch
        {
            get { return firstTouch; }
        }
        bool firstTouch;
        public Vector2 getBaseScreenSize()
        {
            return baseScreenSize;
        }

        private Route route;
        private CarsSpawner carsSpawner;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Accelerometer.Start(SensorSpeed.Game);
            firstTouch = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            route = new Route(Services, Content, baseScreenSize);
            //carsSpawner = new CarsSpawner(route, baseScreenSize);
            ScalePresentationArea();
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            if (Accelerometer.IsMonitoring)
                return;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Default);
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            //route.update(gametime, e.Reading.Acceleration.X, e.Reading.Acceleration.Y, e.Reading.Acceleration.Z);
            xAccel = e.Reading.Acceleration.X;
            yAccel = e.Reading.Acceleration.Y;
            zAccel = e.Reading.Acceleration.Z;
        }

        private int backbufferWidth, backbufferHeight;

        public void ScalePresentationArea()
        {
            //Work out how much we need to scale our graphics to fill the screen
            backbufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            backbufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            float horScaling = backbufferWidth / baseScreenSize.X;
            float verScaling = backbufferHeight / baseScreenSize.Y;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            globalTransformation = Matrix.CreateScale(screenScalingFactor);
            System.Diagnostics.Debug.WriteLine("Screen Size - Width[" + GraphicsDevice.PresentationParameters.BackBufferWidth + "] Height [" + GraphicsDevice.PresentationParameters.BackBufferHeight + "]");
        }
        private GameTime gametime;
        private bool isAccelerating;


        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            // TODO: Add your update logic here
            this.gametime = gametime;
            touchTest();
            if (route.MainCar.IsDestroyed)
                firstTouch = false;
            route.update(gameTime,xAccel,yAccel,zAccel,isAccelerating, firstTouch);
            base.Update(gameTime);
        }

        private void touchTest()
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)
            {
                //Only Fire Select Once it's been released
                if (touchCollection[0].State == TouchLocationState.Moved || touchCollection[0].State == TouchLocationState.Pressed)
                {
                    isAccelerating = true;
                    firstTouch = true;
                }
                else
                    isAccelerating = false;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, globalTransformation);

            route.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
