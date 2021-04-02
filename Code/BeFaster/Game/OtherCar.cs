using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeFaster.Game
{
    class OtherCar
    {
        public Route Route
        {
            get { return route; }
        }
        Route route;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        Vector2 position;
        private Vector2 baseScreenSize;
        private Texture2D otherCarLayout;

        public OtherCar (Route route, Vector2 position, Vector2 baseScreenSize)
        {
            this.route = route;
            this.position = position;
            this.baseScreenSize = baseScreenSize;
            this.ecartVitesse = randomSpeed();
            LoadContent();
        }
        private int randomSpeed()
        {
            Random r = new Random();
            return r.Next(10, 25);

        }
        public void LoadContent()
        {
            randomSkin();
        }

        private void randomSkin()
        {
            Random r = new Random();
            int rand = r.Next(1, 9);
            switch (rand){
                case 1:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/green_c");
                    break;
                case 2:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/red");
                    break;
                case 3:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/taxi_c-6");
                    break;
                case 4:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/cdo_c");
                    break;
                case 5:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/green2_c");
                    break;
                case 6:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/orange_c");
                    break;
                case 7:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/orange2_c");
                    break;
                case 8:
                    otherCarLayout = Route.Content.Load<Texture2D>("Sprites/Cars/violet_c");
                    break;
            }
        }
        private float speed;
        private float ecartVitesse;

        public void update(GameTime gametime)
        {
            position.Y = position.Y + Route.Speed - ecartVitesse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(otherCarLayout, position, Color.White);
        }
    }
}
