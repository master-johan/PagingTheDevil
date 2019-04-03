using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Slash : Ability
    {
        Rectangle sourceRect;
        float angle;
        
        public Slash(Texture2D tex, Vector2 pos, Player player, Vector2 direction, float angle) 
            : base (tex, pos, player, direction)
        {
            this.angle = angle;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
        }
        public override void Update()
        {
            angle += 0.3f;
            //if (player.slash)
            //{

            //}
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, sourceRect, Color.White, angle, new Vector2(0, tex.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
