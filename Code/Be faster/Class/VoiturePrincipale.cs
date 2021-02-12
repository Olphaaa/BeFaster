using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Be_faster.Class


{
    class VoiturePrincipale
    {
        public Vector2 Position;
        public Texture2D Texture;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        // Rectangle permettant de définir la zone de l'image à afficher
        public Rectangle Source;
        // Durée depuis laquelle l'image est à l'écran
        public float time;
        // Durée de visibilité d'une image
        public float frameTime = 0.1f;
        // Indice de l'image en cours
        public int frameIndex;

    }
}