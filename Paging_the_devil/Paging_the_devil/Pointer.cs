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
        List<Button> buttonList;

        public Pointer(Texture2D tex, Vector2 pos, List<Button> buttonList)
        {
            this.tex = tex;
            this.pos = pos;
            this.buttonList = buttonList;
        }
            
        public void Update(GameTime gameTime)
        {
            foreach (var b in buttonList)
	        {
                if (b.activeButton)
	            {
                    pos.Y = b.GetPos.Y;
	            }
	        }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

  
    }
}
