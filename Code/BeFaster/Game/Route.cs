using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

namespace Game1.Game
{
    internal class Route
    {
        private Texture2D layerRoute1;
        private Texture2D layerRoute2;
        public ContentManager Content
        {
            get { return content; }
        }

        public MainCar MainCar
        {
            get { return mainCar; }
        }
        MainCar mainCar;

        ContentManager content;
        private Rectangle test;
        private Vector2 baseScreenSize;

        private Rectangle bg1;
        private Rectangle bg2;

        public Route(IServiceProvider serviceProvider, ContentManager content, Vector2 baseScreenSize)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            this.content = content;
            this.baseScreenSize = baseScreenSize;
            layerRoute1 = Content.Load<Texture2D>("Route/road_big");
            layerRoute2 = Content.Load<Texture2D>("Route/road_big");
            LoadCar(10, 10);
        }

        private Vector2 positionAuDessus = new Vector2(0, -4035);
        private Vector2 routeLocation1 = Vector2.Zero;
        private Vector2 routeLocation2 = new Vector2(0, -4035);
        private int newY = -4035;
        private int tailleImage = 4041;

        private float speed=25;
        private int i1,i2;

        
        public void update(GameTime gameTime, float x, float y, float z)
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

            mainCar.update(gameTime, x, y, z);
        }

        public void LoadCar(int x, int y)
        { 
            mainCar = new MainCar(new Vector2(x,y),this, baseScreenSize);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            DrawRoute(spriteBatch);
            MainCar.Draw(gameTime, spriteBatch);
        }

        private void DrawRoute(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(layerRoute1, routeLocation1, Color.White);
            spriteBatch.Draw(layerRoute2, routeLocation2, Color.White);
        }
    }
}