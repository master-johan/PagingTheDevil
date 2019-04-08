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
        public bool Hit { get; set; }

        public Slash(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            slashPos = pos;
            Hit = false;

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
            DecidingValues();
        }

        private void DecidingValues()
        {
            Active = true;
            Damage = 3;
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
            UpdateHitbox();
        }

        private void UpdateHitbox()
        {
            if (direction == down)
            {
                rect = new Rectangle((int)pos.X - (tex.Height * 2), (int)pos.Y, tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (direction == up)
            {
                rect = new Rectangle((int)pos.X - (tex.Height * 2), (int)pos.Y - tex.Width - (tex.Width/2), tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (direction == right)
            {
                rect = new Rectangle((int)pos.X, (int)pos.Y  - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
            else if (direction == left)
            {
                rect = new Rectangle((int)pos.X - tex.Width - (tex.Width/2), (int)pos.Y - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
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
