using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.Manager
{
    class HUDManager
    {
        GraphicsDevice graphicsDevice;
        Texture2D tex;
        Rectangle rect;
        Vector2 pos;

        Rectangle[] rectArray;

        public HUDManager (GraphicsDevice graphicsDevice, Texture2D tex, Vector2 pos)
        {
            this.graphicsDevice = graphicsDevice;
            this.tex = tex;
            this.pos = pos;
            rectArray = new Rectangle[5];

            rect = new Rectangle()
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.menuTextureList[3], rectArray[0], Color.White);
        }
            

    }
}
