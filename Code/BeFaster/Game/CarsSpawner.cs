using BeFaster.Game;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BeFaster.Game
{
    class CarsSpawner
    {
        public Route Route
        {
            get { return route; }
        }
        Route route;
        private Vector2 baseScreenSize;

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
        private Texture2D layoutTaxi;
        private Texture2D layoutGreen;
        private Texture2D layoutRed;

        public CarsSpawner(Route route, Vector2 baseScreenSize)
        {
            this.route = route;
            this.baseScreenSize = baseScreenSize;
            LoadContent();
        }
        private List<Texture2D> layouts = new List<Texture2D>();
        private void LoadContent()
        {
            //idleAnnimation = new Animation(Route.Content.Load<Texture2D>("Sprites/Cars/red"), 0.1f, false);
            //idleAnnimation = new Animation(route.Content.Load<Texture2D>("Route/road"), 0.1f, false);
            layoutTaxi = Route.Content.Load<Texture2D>("Sprites/Cars/taxi_c-6");
            layoutGreen= Route.Content.Load<Texture2D>("Sprites/Cars/green_c");
            layoutRed = Route.Content.Load<Texture2D>("Sprites/Cars/red_c-6");
            layouts.Add(layoutTaxi); 
            layouts.Add(layoutGreen); 
            layouts.Add(layoutRed); 
        }
        public void update(GameTime gameTime)
        {

        }

        

        private void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(layoutMainCar, rectangleVoiture, Color.White);
        }
    }
}
