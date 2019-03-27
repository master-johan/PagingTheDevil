using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil
{
    class Portal : GameObject
    {
        
        public Portal(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.Blue);
            //spriteBatch.Draw(tex, rect, Color.Black);
        }

        public override void Update()
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            pos = new Vector2(pos.X, pos.Y);

        }
    }
}
