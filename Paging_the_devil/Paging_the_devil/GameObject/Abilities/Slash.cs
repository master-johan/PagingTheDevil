using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using System;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Slash : Ability
    {
        Rectangle sourceRect;

        float angle;

        Vector2 slashPos;
        Vector2 left, right, up, down;
        Vector2 meleeDirection;

        Player player;

        bool hit;

        public Slash(Texture2D tex, Vector2 pos, Vector2 direction, Player player)
            : base(tex, pos, direction)
        {
            this.player = player;
            slashPos = pos;
            hit = false;

            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);

            coolDownTime = ValueBank.SlashCooldown; 
            btnTexture = TextureBank.abilityButtonList[1];

            meleeDirection = DecideDirectionOfSlash(direction);

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

        public override void Update(GameTime gameTime)
        {
            angle -= 0.3f;

            if (angle < MathHelper.ToRadians(-135f) && meleeDirection == up ||
                angle < MathHelper.ToRadians(-225f) && meleeDirection == left ||
                angle < MathHelper.ToRadians(-315f) && meleeDirection == down ||
                angle < MathHelper.ToRadians(-405f) && meleeDirection == right)
            {
                Active = false;
                ToRemove = true;
            }

            if (HitCharacter != null)
            {
                if (!hit)
                {
                    ApplyDamage();
                    hit = true;
                }
            }

            UpdateHitbox();

            Vector2 temp = slashPos - player.pos;
            slashPos -= temp;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, slashPos, sourceRect, Color.White, angle, new Vector2(-20, tex.Height / 2), 1, SpriteEffects.None, 0.1f);

        }

        /// <summary>
        /// Den här metoden sköter rikting på slash
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Den här metoden bestämmer värde
        /// </summary>
        private void DecidingValues()
        {
            Active = true;
            Damage = ValueBank.SlashDmg;
        }
        /// <summary>
        /// Den här metoden uppdaterar hitboxen
        /// </summary>
        private void UpdateHitbox()
        {
            if (meleeDirection == down)
            {
                rect = new Rectangle((int)slashPos.X - (tex.Height * 2), (int)slashPos.Y, tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (meleeDirection == up)
            {
                rect = new Rectangle((int)slashPos.X - (tex.Height * 2), (int)slashPos.Y - tex.Width - (tex.Width/2), tex.Height * 4, tex.Width + (tex.Width / 2));
            }
            else if (meleeDirection == right)
            {
                rect = new Rectangle((int)slashPos.X, (int)slashPos.Y  - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
            else if (meleeDirection == left)
            {
                rect = new Rectangle((int)slashPos.X - tex.Width - (tex.Width/2), (int)slashPos.Y - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
        }
        /// <summary>
        /// Den här metoden bestämmer riktning på vektorer
        /// </summary>
        private void DirectionOfVectors()
        {
            right = new Vector2(1, 0);
            left = new Vector2(-1, 0);
            up = new Vector2(0, -1);
            down = new Vector2(0, 1);
        }
    }
}
