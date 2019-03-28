using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil
{
    class TextureManager
    {
      
        public static List<Texture2D> playerTextures = new List<Texture2D>();

        public TextureManager(ContentManager Content)
        {
            playerTextures.Add(Content.Load<Texture2D>(@"KnightSprite"));
            playerTextures.Add(Content.Load<Texture2D>(@"Portal"));
            playerTextures.Add(Content.Load<Texture2D>(@"Horisontell Vägg"));
            playerTextures.Add(Content.Load<Texture2D>(@"Vertikal Vägg"));
        }
    }
}

