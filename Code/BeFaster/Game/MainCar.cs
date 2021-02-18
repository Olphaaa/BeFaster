using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game1.Game
{
    class MainCar
    {
        private Animation idleAnnimatino;
        private Vector2 position;
        public Route Route
        {
            get { return route; }
        }
        Route route;

        public MainCar(Vector2 position,Route route)
        {
            this.route = route;
            LoadContent();
            //Reset(position);
        }

        private void Reset(Vector2 position)
        {
            throw new NotImplementedException();
        }

        private void LoadContent()
        {
            //idleAnnimatino = new Animation(route.Content.Load<Texture2D>("red"), 0.1f, true);
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //todo
        }
    }
}
