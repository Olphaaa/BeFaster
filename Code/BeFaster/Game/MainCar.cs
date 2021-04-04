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
        private List<float> moyE = new List<float>();


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

        public MainCar(Vector2 position, Route route, Vector2 baseScreenSize)
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
            Position = new Vector2(position.X, position.Y);
            isDestroyed = false;
            //sprite.PlayAnimation(layoutMainCar);
        }
        private void ResetMilieuBas()
        {
            float tailleW = baseScreenSize.X / 2;
            float tailleH = baseScreenSize.Y - 500;

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
        public void update(GameTime gametime, float x, List<OtherCar> Cars)
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

        /// <summary>
        /// Permet de controller la limite à ne pas franchir de la voiture pour ne pas aller trop à droite ni a gauche
        /// Control si la voiture principale n'entre pas en collision avec une des voitures existante sur la route
        /// </summary>
        /// <returns>true s'il peut bouger, false sinon</returns>
        public bool CanMove()
        {
            OtherCar test = null;
            if ((-(((baseScreenSize.X * moy) - positionMilieu) + 80) <= 200 || -(((baseScreenSize.X * moy) - positionMilieu) + 80) >= baseScreenSize.X - 260))
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
                    if (this.Collide(car))
                    {
                        isDestroyed = true;
                        return false;
                    }
                }
            }
            if (test != null)
            {
                othercars.Remove(test);
            }
            //Console.WriteLine("La j'suis bien");
            return true;
        }
        /// <summary>
        /// Avoir les coordonnées d'une position
        /// </summary>
        /// <returns>la position actuelle soir la valeur en haut a gauche du sujet</returns>
        public Vector2 getTopLeft()
        {
            return position;
        }

        /// <summary>
        /// Avoir les coordonnées d'une position
        /// </summary>
        /// <returns>La valeur en haut à droite</returns>
        public Vector2 getTopRight()
        {
            return new Vector2(position.X + GetLayout.Width, position.Y);
        }
        /// <summary>
        /// Avoir les coordonnées d'une position
        /// </summary>
        /// <returns>La valeur en bas à gauche</returns>
        public Vector2 getBottomLeft()
        {
            return new Vector2(position.X, position.Y + GetLayout.Height);
        }
        /// <summary>
        /// Avoir les coordonnées d'une position
        /// </summary>
        /// <returns>La valeur en bas à droite</returns>
        public Vector2 getBottomRight()
        {
            return new Vector2(position.X + GetLayout.Width, position.Y + GetLayout.Height);
        }

        /// <summary>
        /// Dessine la voiture sur la route
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(layoutMainCar, rectangleCar, Color.White);
        }

        /// <summary>
        /// méthode qui permet de détecter une collision
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Collide(OtherCar other)
        {
            Rectangle BoundingRectangle = new Rectangle((int)other.Position.X, (int)other.Position.Y, other.GetLayout.Width, other.GetLayout.Height);
            if (BoundingRectangle.Intersects(new Rectangle((int)rectangleCar.X + 10, (int)rectangleCar.Y, GetLayout.Width - 20, GetLayout.Height)))
            {
                return true;
            }
            return false;
        }
    }
}
