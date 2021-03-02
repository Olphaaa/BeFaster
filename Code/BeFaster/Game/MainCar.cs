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

        public MainCar(Vector2 position,Route route,Vector2 baseScreenSize)
        {
            this.route = route;
            this.baseScreenSize = baseScreenSize;
            LoadContent();
            Reset(position);
            ResetMilieuBas();
        }

        private void Reset(Vector2 position)
        {
            //Position = position;
            Position = new Vector2(position.X,position.Y);
            isDestroyed = false;
            sprite.PlayAnimation(idleAnnimation);
        }
        private void ResetMilieuBas()
        {
            float tailleW = baseScreenSize.X /2;
            float tailleH = baseScreenSize.Y-500;

            Position = new Vector2(tailleW, tailleH);
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
