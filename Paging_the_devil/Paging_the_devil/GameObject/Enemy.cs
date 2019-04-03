using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Enemy : Character
    {
        public Enemy(Texture2D tex, Vector2 pos) : base (tex, pos)
        {
              
        }
        public override void Update()
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
