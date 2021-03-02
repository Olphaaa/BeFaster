using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

namespace Game1.Game
{
    internal class Route
    {
        private Texture2D layerRoute;
        public ContentManager Content
        {
            get { return content; }
        }

        public MainCar MainCar
        {
            get { return mainCar; }
        }
        MainCar mainCar;

        ContentManager content;
        private Rectangle test;
        private Vector2 baseScreenSize;
        public Route(IServiceProvider serviceProvider, ContentManager content, Vector2 baseScreenSize)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            this.content = content;
            this.baseScreenSize = baseScreenSize;
            layerRoute = Content.Load<Texture2D>("Route/road_big");
            LoadCar(10, 10);
        }
        

        public void LoadCar(int x, int y)
        { 
            mainCar = new MainCar(new Vector2(x,y),this, baseScreenSize);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawRoute(spriteBatch);
            MainCar.Draw(gameTime, spriteBatch);
        }

        private void DrawRoute(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(layerRoute, Vector2.Zero, Color.White);
        }
    }
}