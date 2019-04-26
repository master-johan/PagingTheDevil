using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
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
        Vector2 meleeDirection;

        public bool Active { get; private set; }
        public bool Hit { get; set; }

        public Slash(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            slashPos = pos;
            Hit = false;
            coolDownTime = 20; 
            meleeDirection = DecideDirectionOfSlash(direction);

            btnTexture = TextureBank.hudTextureList[5];


            DirectionOfVectors();

            if (meleeDirection == up)
            {
                angle = MathHelper.ToRadians(-45f);
            }
            else if (meleeDirection == left)
            {
                angle = MathHelper.ToRadians(-135);
            }
            else if (meleeDirection == down)
            {
                angle = MathHelper.ToRadians(-225);
            }
            else if (meleeDirection == right)
            {
                angle = MathHelper.ToRadians(-315);
            }
            DecidingValues();
        }

        private Vector2 DecideDirectionOfSlash(Vector2 direction)
        {
            double slashDir = Math.Atan2(direction.Y, direction.X);

            float slashAngle = MathHelper.ToDegrees((float)slashDir);

            Vector2 meleeDirection = Vector2.Zero;

            if (slashAngle > 45 && slashAngle < 135) // up
            {
                meleeDirection = new Vector2(0, -1);
            }

            else if (slashAngle > 135 || slashAngle < -135) // left
            {
                meleeDirection = new Vector2(-1, 0);
            }

            else if (slashAngle > -135 && slashAngle < -45) // down
            {
                meleeDirection = new Vector2(0, 1);
            }

            else if (slashAngle > -45 && slashAngle < 45) // right
            {
                meleeDirection = new Vector2(1, 0);
            }
            return meleeDirection;

        }

        private void DecidingValues()
        {
            Active = true;
            Damage = 3;
        }

        public override void Update()
        {
            angle -= 0.3f;

            if (angle < MathHelper.ToRadians(-135f) && meleeDirection == up ||
                angle < MathHelper.ToRadians(-225f) && meleeDirection == left ||
                angle < MathHelper.ToRadians(-315f) && meleeDirection == down ||
                angle < MathHelper.ToRadians(-405f) && meleeDirection == right)
            {
                Active = false;
            }
            UpdateHitbox();
        }

        private void UpdateHitbox()
        {
            if (meleeDirection == down)
            {
                rect = new Rectangle((int)pos.X - (tex.Height * 2), (int)pos.Y, tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (meleeDirection == up)
            {
                rect = new Rectangle((int)pos.X - (tex.Height * 2), (int)pos.Y - tex.Width - (tex.Width/2), tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (meleeDirection == right)
            {
                rect = new Rectangle((int)pos.X, (int)pos.Y  - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
            else if (meleeDirection == left)
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
