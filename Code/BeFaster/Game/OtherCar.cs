using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeFaster.Game
{
    public class OtherCar : Car
    {

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        Vector2 position;
        private Vector2 baseScreenSize;
        private Texture2D otherCarLayout;
        public Rectangle HitBox
        {
            get { return hitBox; }
        }
        Rectangle hitBox;

        public Texture2D GetLayout {
            get
            {
                return otherCarLayout;
            }
        }


        public OtherCar (Route route, Vector2 position, Vector2 baseScreenSize)
        {
            base.Route = route;
            this.position = position;
            this.baseScreenSize = baseScreenSize;
            this.ecartVitesse = randomSpeed();
            //hitBox = new Rectangle((int)position.X, (int)position.Y, GetLayout.Height, GetLayout.Height);
            LoadContent();
        }

        public bool collide(OtherCar other)
        {
            if (getTopLeft().X < other.getTopLeft().X && getTopRight().X > other.getTopLeft().X) return true;
            if (getTopLeft().X < other.getTopRight().X && getTopRight().X > other.getTopRight().X) return true;

            if (getTopLeft().Y < other.getTopLeft().Y && getBottomLeft().Y > other.getTopLeft().Y) return true;
            if (getTopLeft().Y < other.getBottomLeft().Y && getBottomLeft().Y > other.getBottomLeft().Y) return true;
            return false;
        }


        private Vector2 getTopLeft(){
            return position;
        }
        private Vector2 getTopRight(){
            return new Vector2(position.X, position.Y + GetLayout.Width);
        }
        private Vector2 getBottomLeft()
        {
            return new Vector2(position.X + GetLayout.Width, position.Y);
        }
        private Vector2 getBottomRight()
        {
            return new Vector2(position.X + GetLayout.Width, position.Y + GetLayout.Height);
        }


        private int GetWidth()
        {
            return GetLayout.Width;
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
