using BeFaster.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        
        public Route(IServiceProvider serviceProvider, ContentManager content)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            this.content = content;
            layerRoute = Content.Load<Texture2D>("Route/road");
            //layerRoute = Content.Load<Texture2D>("Sprites/Cars/red");
            LoadCar(10, 10);
        }
        public void LoadCar(int x, int y)
        { 
            mainCar = new MainCar(new Vector2(x,y),this);
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