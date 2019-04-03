using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Fireball : Ability
    {
        int speed;

        public Fireball(Texture2D tex, Vector2 pos, Player player, Vector2 direction) 
            : base(tex, pos, player, direction)
        {
            speed = 5;
        }

        public override void Update()
        {
            if (player.shoot)
            {
                pos += direction * speed;
            }

            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
