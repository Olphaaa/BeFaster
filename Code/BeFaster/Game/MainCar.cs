using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Game1.Game
{
    class MainCar
    {
        private Texture2D layoutMainCar;
        private Rectangle rectangleCar;
        private float positionMilieu;
        private List<OtherCar> othercars;
        private float moy;
        private Vector2 baseScreenSize;

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
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;
        public bool IsDestroyed
        {
            get { return isDestroyed; }
        }
        bool isDestroyed;
        public Texture2D GetLayout
        {
            get
            {
                return layoutMainCar;
            }
        }

        public MainCar(Vector2 position,Route route,Vector2 baseScreenSize)
        {
            this.route = route;
            this.baseScreenSize = baseScreenSize;
            positionMilieu = baseScreenSize.X / 2;
            LoadContent();
            Reset(position);
            rectangleCar = new Rectangle((int)position.X, (int)position.Y, GetLayout.Width, GetLayout.Height);
            ResetMilieuBas();

        }

        private void Reset(Vector2 position)
        {
            //Position = position;
            Position = new Vector2(position.X,position.Y);
            isDestroyed = false;
           //sprite.PlayAnimation(layoutMainCar);
        }
        private void ResetMilieuBas()
        {
            float tailleW = baseScreenSize.X /2;
            float tailleH = baseScreenSize.Y-500;

            Position = new Vector2(tailleW, tailleH);
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, GetLayout.Width, GetLayout.Height); 
            rectangleCar = rect;
            isDestroyed = false;
           // sprite.PlayAnimation(layoutMainCar);
        }
        private void LoadContent()
        {
            //idleAnnimation = new Animation(Route.Content.Load<Texture2D>("Sprites/Cars/red"), 0.1f, false);
            //idleAnnimation = new Animation(route.Content.Load<Texture2D>("Route/road"), 0.1f, false);
            layoutMainCar = Route.Content.Load<Texture2D>("Sprites/Cars/red");
        }
        private List<float> moyE = new List<float>();
        public void update(GameTime gametime,float x, float y, float z,List<OtherCar> Cars)
        {
            this.othercars = Cars;
            if (moyE.Count >= 15)
            {
                moyE.RemoveAt(0);
                moyE.Add(x);
            }
            else
            {
                moyE.Add(x);
            }
            moy = moyE.Average();

            if (CanMove())
            {
                rectangleCar.X = (int)-(((baseScreenSize.X * moy) - positionMilieu) + 80);

            }
        }
           

        public bool CanMove()
        {
            OtherCar test = null;
            if((-(((baseScreenSize.X * moy) - positionMilieu) + 80) <= 200 || -(((baseScreenSize.X * moy) - positionMilieu) + 80) >= baseScreenSize.X - 260))
            {
                return false;
            }
            if (othercars == null)
            {
                Console.WriteLine("BOUH");
            }
            if (othercars != null)
            {
                foreach (OtherCar car in othercars)
                {
                    //taille.X = (car.GetLayout.Width *) / 100;   
                    /* if ((position.X >= car.Position.X && position.X <= (car.Position.X + car.GetLayout.Width*0.85) && position.Y >= car.Position.Y  && (position.Y) <=(car.Position.Y+ car.GetLayout.Height*0.85))||
                         (car.Position.X>position.X&&car.Position.X<(position.X+layoutMainCar.Width*0.85)&&(car.Position.Y+car.GetLayout.Height*0.85)>position.Y&&(car.Position.Y+car.GetLayout.Height*0.85)<(position.Y+layoutMainCar.Height*0.85))||
                         ((position.X+layoutMainCar.Width*0.85) >= car.Position.X && (position.Y+layoutMainCar.Height*0.85) >= car.Position.Y && (position.X+layoutMainCar.Width*0.85) <= (car.Position.X + car.GetLayout.Width*0.85) && (position.Y+layoutMainCar.Height*0.85) <= (car.Position.Y + car.GetLayout.Height*0.85))||
                         ((car.Position.X+car.GetLayout.Width*0.85) > position.X && (car.Position.X+car.GetLayout.Width*0.85) < (position.X + layoutMainCar.Width*0.85) && (car.Position.Y+car.GetLayout.Height*0.85) > position.Y && (car.Position.Y+car.GetLayout.Height*0.85) < (position.Y + layoutMainCar.Height*0.85)))
                     {
                         Console.WriteLine("AIE JE ME SUIS FAIT MAL");
                         isDestroyed = true;
                         return false;
                     }*/
                    if (this.Collide(car))
                    {
                        Console.WriteLine("AIE JE ME SUIS FAIT MAL");
                        //test = car;
                        isDestroyed = true;
                        return false;
                    }
                }
            }
            if(test != null)
            {
                othercars.Remove(test);
            }
            //Console.WriteLine("La j'suis bien");
            return true;
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
        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(layoutMainCar, rectangleCar, Color.White);
        }
        public bool Collide(OtherCar other)
        {
            Rectangle BoundingRectangle = new Rectangle((int)other.Position.X, (int)other.Position.Y, other.GetLayout.Width, other.GetLayout.Height);
            if (BoundingRectangle.Intersects(new Rectangle((int)rectangleCar.X, (int)rectangleCar.Y, GetLayout.Width, GetLayout.Height)))
            {
                return true;
            }
            return false;
        }
    }
}
