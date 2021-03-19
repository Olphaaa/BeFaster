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
        private AnimationCar sprite;
        private SpriteEffects flip = SpriteEffects.None;
        private float positionMilieu;


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

        private Vector2 rectangleVoiture;
        public MainCar(Vector2 position,Route route,Vector2 baseScreenSize)
        {
            this.route = route;
            this.baseScreenSize = baseScreenSize;
            positionMilieu = baseScreenSize.X / 2;
            LoadContent();
            Reset(position);
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
            rectangleVoiture = position;
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
        public void update(GameTime gametime,float x, float y, float z)
        {

            if (moyE.Count >= 15)
            {
                moyE.RemoveAt(0);
                moyE.Add(x);
            }
            else
            {
                moyE.Add(x);
            }
            float moy = moyE.Average();
            if (!(-(((baseScreenSize.X * moy) - positionMilieu) + 80) <= 200 || -(((baseScreenSize.X * moy) - positionMilieu) + 80) >= baseScreenSize.X - 260))
            {
                rectangleVoiture.X = -(((baseScreenSize.X * moy) - positionMilieu) + 80);
            }
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /*if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;*/

            //sprite.Draw(gameTime, spriteBatch, rectangleVoiture, flip);
            spriteBatch.Draw(layoutMainCar, rectangleVoiture, Color.White);
        }
    }
}
