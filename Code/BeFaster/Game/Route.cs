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
            layerRoute1 = Content.Load<Texture2D>("Route/road_big_debug");
            layerRoute2 = Content.Load<Texture2D>("Route/road_big_debug_2");
            LoadCar(10, 10);
        }

        private Vector2 positionAuDessus = new Vector2(0, -4035);
        private Vector2 routeLocation1 = Vector2.Zero;
        private Vector2 routeLocation2 = new Vector2(0, -4035);
        private int newY = -4035;
        
        private float speed=25;
        private int i1,i2;

        
        public void update(GameTime gameTime)
        {
            

            routeLocation1.Y = positionAuDessus.Y+ i1 *speed;
            routeLocation2.Y = i2 *speed;
            
           
            if (routeLocation1.Y >= baseScreenSize.Y) //quand 1 arrive en bas
            {
                routeLocation1 = positionAuDessus;
                i1 = 0;
            }
            if (routeLocation1.Y + 4035 >= baseScreenSize.Y)//des qu'on peut afficher
            {
                //coucou
                routeLocation2.Y = positionAuDessus.Y;
                i2 = -10;
            }

            i1++;
            i2++;
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