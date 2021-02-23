using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game1.Game
{
    class MainCar
    {
        private Animation idleAnnimation;
        private AnimationCar sprite;
        private SpriteEffects flip = SpriteEffects.None;


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

        public MainCar(Vector2 position,Route route)
        {
            this.route = route;
            LoadContent();
            Reset(position);
        }

        private void Reset(Vector2 position)
        {
            //Position = position;
            Position = new Vector2(100, 100);
            isDestroyed = false;
            sprite.PlayAnimation(idleAnnimation);
        }

        private void LoadContent()
        {
            idleAnnimation = new Animation(Route.Content.Load<Texture2D>("Sprites/Cars/red"), 0.1f, false);
            //idleAnnimation = new Animation(route.Content.Load<Texture2D>("Route/road"), 0.1f, false);
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;

            sprite.Draw(gameTime, spriteBatch, Position, flip);
        }
    }
}
