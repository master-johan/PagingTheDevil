using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil
{
    public static class TextureManager
    {
      
        public static List<Texture2D> playerTextures = new List<Texture2D>();
        public static List<Texture2D> roomTextures = new List<Texture2D>();

        public static void LoadTextures(ContentManager Content)
        {
            //Player
            playerTextures.Add(Content.Load<Texture2D>(@"KnightSprite"));

            //Room
            roomTextures.Add(Content.Load<Texture2D>(@"Portal"));
            roomTextures.Add(Content.Load<Texture2D>(@"Horisontell Vägg"));
            roomTextures.Add(Content.Load<Texture2D>(@"Vertikal Vägg"));
        }
    }
}

