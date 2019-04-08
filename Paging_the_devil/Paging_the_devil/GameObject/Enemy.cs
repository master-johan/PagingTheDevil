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
        public bool toRevome;
        public Enemy(Texture2D tex, Vector2 pos) : base (tex, pos)
        {
            HealthPoints = 10;
        }
        public override void Update()
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            if (HealthPoints <= 0)
            {
                toRevome = true;
            }
            else
            {
                toRevome = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
