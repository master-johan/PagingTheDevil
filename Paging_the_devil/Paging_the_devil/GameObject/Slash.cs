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

        Vector2 slashPos;
        Vector2 left, right, up, down;

        public bool Active { get; private set; }

        public Slash(Texture2D tex, Vector2 pos, Player player, Vector2 direction/* , float angle*/)
            : base(tex, pos, player, direction)
        {
            //this.angle = angle;
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            slashPos = player.pos;

            DirectionOfVectors();

            if (direction == up)
            {
                angle = MathHelper.ToRadians(-45f);
            }
            else if (direction == left)
            {
                angle = MathHelper.ToRadians(-135);
            }
            else if (direction == down)
            {
                angle = MathHelper.ToRadians(-225);
            }
            else if (direction == right)
            {
                angle = MathHelper.ToRadians(-315);
            }
            Active = true;

        }
        public override void Update()
        {
            angle -= 0.3f;

            if (angle < MathHelper.ToRadians(-135f) && direction == up ||
                angle < MathHelper.ToRadians(-225f) && direction == left ||
                angle < MathHelper.ToRadians(-315f) && direction == down ||
                angle < MathHelper.ToRadians(-405f) && direction == right)
            {
                Active = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, slashPos, sourceRect, Color.White, angle, new Vector2(-20, tex.Height / 2), 1, SpriteEffects.None, 1);
        }

        private void DirectionOfVectors()
        {
            right = new Vector2(1, 0);
            left = new Vector2(-1, 0);
            up = new Vector2(0, -1);
            down = new Vector2(0, 1);
        }
    }
}
