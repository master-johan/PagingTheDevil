using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil
{
    class UtilityManager
    {
        public static List<Texture2D> playerTextures = new List<Texture2D>();

        public UtilityManager(ContentManager Content)
        {
            playerTextures.Add(Content.Load<Texture2D>(@"KnightSprite"));
        }
    }
}
