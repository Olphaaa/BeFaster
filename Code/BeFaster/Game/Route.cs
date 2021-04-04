using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Game1.Game
{
    internal class Route
    {
        private Texture2D layerRoute1;
        private Texture2D layerRoute2;

        private Vector2 routeLane1 = new Vector2(300, -200);
        private Vector2 routeLane2 = new Vector2(490, -200);
        private Vector2 routeLane3 = new Vector2(680, -200);
        private Vector2 routeLane4 = new Vector2(870, -200);
        private bool isAccelerating;

        public ContentManager Content
        {
            get { return content; }
        }

        public MainCar MainCar
        {
            get { return mainCar; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        float speed;
        public List<OtherCar> Othercars
        {
            get { return othercars; }
        }
        List<OtherCar> othercars;

        //private List<OtherCar> othercars = new List<OtherCar>();

        MainCar mainCar;

        ContentManager content;
        private Rectangle test;
        private Vector2 baseScreenSize;

        private Rectangle bg1;
        private Rectangle bg2;

        public Route(IServiceProvider serviceProvider, ContentManager content, Vector2 baseScreenSize)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            this.content = content;
            this.baseScreenSize = baseScreenSize;
            upLimit = 50;
            downLimit = 5;
            layerRoute1 = Content.Load<Texture2D>("Route/road_big");
            layerRoute2 = Content.Load<Texture2D>("Route/road_big");
            speed = 25;
            othercars = new List<OtherCar>();
            LoadCar(10, 10);

        }

        public Vector2 positionAuDessus = new Vector2(0, -4035);
        private Vector2 routeLocation1 = Vector2.Zero;
        private Vector2 routeLocation2 = new Vector2(0, -4035);
        private int newY = -4035;
        private int tailleImage = 4041;
        private int rand;


        private int i1, i2;


        public void update(GameTime gameTime, float x, float y, float z, bool isAccelerating, bool firstTouch)
        {
            this.isAccelerating = isAccelerating;
            if (isAccelerating)
            {
                speed = (float)(speed + 0.5);
                //Console.WriteLine("Je fonce !!");
            }
            else
            {
                speed = (float)(speed - 0.7);
                //Console.WriteLine("Je ralenti !!");
            }
            checkSpeed();
            if (firstTouch)
            {
                updateOtherCar(gameTime);
            }
            updateRoute(gameTime, x, y, z);
            if (MainCar.IsDestroyed)
                DespawnAllOtherCar();
        }
        private int downLimit;
        private int upLimit;
        private void checkSpeed()
        {
            if (speed >= upLimit)
                speed = upLimit;
            else if (speed <= downLimit)
            {
                speed = downLimit;
            }
        }
        public void setDownLimit(int downLimit)
        {
            this.downLimit = downLimit;
        }
        private void updateRoute(GameTime gameTime, float x, float y, float z)
        {
            routeLocation1.Y = routeLocation1.Y + speed;
            routeLocation2.Y = routeLocation2.Y + speed;

            if (routeLocation2.Y + tailleImage >= baseScreenSize.Y && routeLocation2.Y + tailleImage <= baseScreenSize.Y + speed)
            {
                routeLocation1.Y = positionAuDessus.Y - 1780;
            }
            if (routeLocation1.Y + tailleImage >= baseScreenSize.Y && routeLocation1.Y + tailleImage <= baseScreenSize.Y + speed)
            {
                routeLocation2.Y = positionAuDessus.Y - 1780;
            }

            mainCar.update(gameTime, x, y, z, othercars);
        }
        private void updateOtherCar(GameTime gameTime)
        {
            randomSpawn();
            OtherCar asuppr = null;
            foreach (OtherCar oc in othercars)
            {
                oc.update(gameTime);
                oc.suivreUneVoiture();
                if (oc.Position.Y >= baseScreenSize.Y *4)
                {
                    asuppr = oc;
                }
            }
            if (asuppr != null)
            {
                othercars.Remove(asuppr);
            }
        }

        private void randomSpawn()
        {
            Random r = new Random();
            if (isAccelerating)
            {
                rand = r.Next(1, 15);
            }
            else
            {
                rand = r.Next(1, 50);
            }

            if (rand == 1)
            {
                OtherCar oc = new OtherCar(this, RandomRouteLane(), baseScreenSize);
                //LoadOtherCar(oc);
                foreach (OtherCar car in othercars)
                {
                    /*if ((oc.Position.X + oc.GetLayout.Width) > car.Position.X && (oc.Position.Y + oc.GetLayout.Height) > car.Position.Y && (oc.Position.Y + oc.GetLayout.Height) < (car.Position.Y + car.GetLayout.Height) && (oc.Position.X + oc.GetLayout.Width) < (car.Position.X + car.GetLayout.Width))
                    {
                        return;
                    }*/
                    /*if (oc.Position.X == car.Position.X && (car.Position.Y <= 0 && car.Position.Y <= -300) )
                    {
                        return;
                    }*/
                    if (oc.Collide(car))
                    {
                        return;
                    }
                }
                LoadOtherCar(oc);
            }
        }


        private void LoadOtherCar(OtherCar oc)
        {
            othercars.Add(oc);
        }

        private void DespawnAllOtherCar()
        {
            othercars.Clear();
        }

        private Vector2 RandomRouteLane()
        {
            Random r = new Random();
            int rand = r.Next(1, 5);
            switch (rand)
            {
                case 1:
                    return routeLane1;
                    break;
                case 2:
                    return routeLane2;
                    break;
                case 3:
                    return routeLane3;
                    break;
                case 4:
                    return routeLane4;
                    break;
                default:
                    return routeLane1;
                    break;
            }
        }

        public void LoadCar(int x, int y)
        {
            mainCar = new MainCar(new Vector2(x, y), this, baseScreenSize);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawRoute(gameTime, spriteBatch);
            MainCar.Draw(gameTime, spriteBatch);
        }

        private void DrawRoute(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(layerRoute1, routeLocation1, Color.White);
            spriteBatch.Draw(layerRoute2, routeLocation2, Color.White);
            foreach (OtherCar oc in othercars)
                oc.Draw(gameTime, spriteBatch);
        }
    }
}