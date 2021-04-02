using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeFaster.Game
{
    public class Car
    {
        protected Texture2D layoutCar;
        protected float positionMilieu;
        //public List<OtherCar> othercars;
        protected Vector2 baseScreenSize;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;
        public Route Route
        {
            get { return route; }
            set { route = value; }
        }
        Route route;

        /*public Car(Vector2 position,Route route ,Vector2 baseScreenSize)
        {
            positionMilieu = baseScreenSize.X / 2;
            this.route = route;
            this.baseScreenSize = baseScreenSize;

        }*/
    }
}
