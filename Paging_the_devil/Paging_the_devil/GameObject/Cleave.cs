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
    class Cleave : Ability
    {
        Rectangle sourceRect;

        float angle;

        Vector2 cleavePos;
        Vector2 left, right, up, down;
        Vector2 meleeDirection;
        Player player;

        public bool Active { get; private set; }
        public bool Hit { get; set; }

        public Cleave(Texture2D tex, Vector2 pos, Vector2 direction, Player player)
            : base(tex, pos, direction)
        {
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
            cleavePos = pos;
            Hit = false;
            coolDownTime = 20;
            meleeDirection = DecideDirectionOfCleave(direction);
            this.player = player;
            btnTexture = TextureManager.abilityButtonList[4];


            DirectionOfVectors();

            if (meleeDirection == up)
            {
                angle = MathHelper.ToRadians(-0f);
            }
            else if (meleeDirection == left)
            {
                angle = MathHelper.ToRadians(-90);
            }
            else if (meleeDirection == down)
            {
                angle = MathHelper.ToRadians(-180);
            }
            else if (meleeDirection == right)
            {
                angle = MathHelper.ToRadians(-270);
            }
            DecidingValues();
        }

        private Vector2 DecideDirectionOfCleave(Vector2 direction)
        {
            double cleaveDir = Math.Atan2(direction.Y, direction.X);

            float cleaveAngle = MathHelper.ToDegrees((float)cleaveDir);

            Vector2 meleeDirection = Vector2.Zero;

            if (cleaveAngle > 45 && cleaveAngle < 135) // up
            {
                meleeDirection = new Vector2(0, -1);
            }

            else if (cleaveAngle > 135 || cleaveAngle < -135) // left
            {
                meleeDirection = new Vector2(-1, 0);
            }

            else if (cleaveAngle > -135 && cleaveAngle < -45) // down
            {
                meleeDirection = new Vector2(0, 1);
            }

            else if (cleaveAngle > -45 && cleaveAngle < 45) // right
            {
                meleeDirection = new Vector2(1, 0);
            }
            return meleeDirection;

        }

        private void DecidingValues()
        {
            Active = true;
            Damage = ValueBank.CleaveDmg;
        }

        public override void Update()
        {
            angle -= 0.15f;

            if (angle < MathHelper.ToRadians(-180f) && meleeDirection == up ||
                angle < MathHelper.ToRadians(-270f) && meleeDirection == left ||
                angle < MathHelper.ToRadians(-360f) && meleeDirection == down ||
                angle < MathHelper.ToRadians(-430f) && meleeDirection == right)
            {
                Active = false;
            }
            Vector2 temp = cleavePos - player.pos;
            cleavePos -= temp;

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
                rect = new Rectangle((int)pos.X - (tex.Height * 2), (int)pos.Y - tex.Width - (tex.Width / 2), tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (meleeDirection == right)
            {
                rect = new Rectangle((int)pos.X, (int)pos.Y - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
            else if (meleeDirection == left)
            {
                rect = new Rectangle((int)pos.X - tex.Width - (tex.Width / 2), (int)pos.Y - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, cleavePos, sourceRect, Color.White, angle, new Vector2(-40, tex.Height / 2), 1, SpriteEffects.None, 1);
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
