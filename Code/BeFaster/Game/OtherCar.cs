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

        public Texture2D GetLayout {
            get
            {
                return otherCarLayout;
            }
        }


        public OtherCar (Route route, Vector2 position, Vector2 baseScreenSize)
        {
            this.route = route;
            this.position = position;
            this.baseScreenSize = baseScreenSize;
            this.ecartVitesse = randomSpeed();
            colored = Color.White;
            LoadContent();
        }
        private int randomSpeed()
        {
            Random r = new Random();
            return r.Next(10, 15);

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
            position.Y = position.Y + (Route.Speed - ecartVitesse);
        }
        public Color Colored
        {
            get { return colored; }
            set { colored = value; }
        }
        Color colored;
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(otherCarLayout, position, Color.White);
            //Texture2D whiteRectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            //whiteRectangle.SetData(new[] {colored });
            //spriteBatch.Draw(whiteRectangle, new Rectangle((int)position.X, (int)position.Y, GetLayout.Width, GetLayout.Height),colored);
        }

        

        public Vector2 getTopLeft()
        {
            return position;
        }
        public Vector2 getTopRight()
        {
            return new Vector2(position.X + GetLayout.Width, position.Y);
        }
        public Vector2 getBottomLeft()
        {
            return new Vector2(position.X, position.Y + GetLayout.Height);
        }
        public Vector2 getBottomRight()
        {
            return new Vector2(position.X + GetLayout.Width, position.Y + GetLayout.Height);
        }

        public void suivreUneVoiture()
        {
            foreach (OtherCar oc in Route.Othercars)
            {
                if (oc.position.X == this.position.X && (this.position.Y > oc.position.Y || this.position.Y < oc.position.Y + oc.GetLayout.Height))
                {
                    //la voiture courante suit la voiture
                    //Console.WriteLine("Bouges ta caisse connard !! ");
                    oc.ecartVitesse = this.ecartVitesse;
                }
            }
        }
    }
}
