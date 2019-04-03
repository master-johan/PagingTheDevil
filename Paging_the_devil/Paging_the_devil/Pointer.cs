using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject;

namespace Paging_the_devil
{
    public class Pointer
    {
        Texture2D tex;
        Vector2 pos;

        public Pointer(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
        }
            
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

  
    }
}
