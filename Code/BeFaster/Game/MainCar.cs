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
    public class MainCar : Car
    {
        private List<float> moyE = new List<float>();
        private float moy;


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
        public MainCar(Route route ,Vector2 baseScreenSize)
        {
            base.Route = route;
            baseScreenSize = baseScreenSize;
            LoadContent();
            Reset(base.Position);
            ResetMilieuBas();
        }

        private void Reset(Vector2 position)
        {
            //Position = position;
            base.Position = new Vector2(base.Position.X,base.Position.Y);
            
            isDestroyed = false;
           //sprite.PlayAnimation(layoutMainCar);
        }
        private void ResetMilieuBas()
        {
            float tailleW = base.baseScreenSize.X /2;
            float tailleH = base.baseScreenSize.Y-500;

            Position = new Vector2(tailleW, tailleH);
            rectangleVoiture = base.Position;
            isDestroyed = false;
           // sprite.PlayAnimation(layoutMainCar);
        }
        private void LoadContent()
        {
            //idleAnnimation = new Animation(Route.Content.Load<Texture2D>("Sprites/Cars/red"), 0.1f, false);
            //idleAnnimation = new Animation(route.Content.Load<Texture2D>("Route/road"), 0.1f, false);
            base.layoutCar = Route.Content.Load<Texture2D>("Sprites/Cars/red");
        }
        public void update(GameTime gametime,float x, float y, float z,List<OtherCar> Cars)
        {
            //othercars = Cars;
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
            rectangleVoiture.X = -(((baseScreenSize.X * moy) - positionMilieu) + 80);

            /*if (CanMove())
            {*/

            //}
        }
           

        
        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(layoutCar, rectangleVoiture, Color.White);
        }
    }
}
