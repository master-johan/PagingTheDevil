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
      
        public static List<Texture2D> playerTextureList = new List<Texture2D>();
        public static List<Texture2D> mageSpellList = new List<Texture2D>();

        public TextureManager(ContentManager Content)
        {
            playerTextureList.Add(Content.Load<Texture2D>(@"KnightSprite"));
            mageSpellList.Add(Content.Load<Texture2D>(@"redSquare"));
        }
    }
}

