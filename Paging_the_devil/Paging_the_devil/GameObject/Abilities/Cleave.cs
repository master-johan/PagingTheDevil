using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Cleave : Ability
    {
        Rectangle sourceRect;

        float angle;
        float timePassed;

        bool hitBefore;

        Vector2 cleavePos;
        Vector2 left, right, up, down;
        Vector2 meleeDirection;

        Character character;

        public List<Enemy> enemiesHitList;

        public bool Active { get; private set; }
        bool hit;

        public Cleave(Texture2D tex, Vector2 pos, Vector2 direction, Character character)
            : base(tex, pos, direction)
        {
            this.character = character;
            cleavePos = pos;


            hitBefore = false;

            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);

            coolDownTime = ValueBank.CleaveCooldown;
            btnTexture = TextureBank.abilityButtonList[4];


            meleeDirection = DecideDirectionOfCleave(direction);

            enemiesHitList = new List<Enemy>();

            DirectionOfVectors();

            DecidingValues();
        }

        public override void Update(GameTime gameTime)
        {


            if (angle < MathHelper.ToRadians(-180f) && meleeDirection == up ||
                angle < MathHelper.ToRadians(-270f) && meleeDirection == left ||
                angle < MathHelper.ToRadians(-360f) && meleeDirection == down ||
                angle < MathHelper.ToRadians(-430f) && meleeDirection == right)
            {
                Active = false;
                ToRemove = true;
                enemiesHitList.Clear();
            }

            angle -= 0.15f;

            Vector2 temp = cleavePos - character.pos;
            cleavePos -= temp;

            if (HitCharacter != null)
            {
                bool hasHitBefore = false;

                foreach (var e in enemiesHitList)
                {
                    if (HitCharacter == e && hitBefore)
                    {
                        hasHitBefore = true;
                    }
                }

                if (!hasHitBefore)
                {
                    DoDamage();
                    hitBefore = true;
                }
            }
            UpdateHitbox();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, cleavePos, sourceRect, Color.White, angle, new Vector2(-40, tex.Height / 2), 1, SpriteEffects.None, 0.1f);
        }

        /// <summary>
        /// Den här metoden bestämmer riktningen av cleave
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Den här metoden bestämmer värden
        /// </summary>
        private void DecidingValues()
        {
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

            Active = true;
            Damage = ValueBank.CleaveDmg;
        }
        /// <summary>
        /// Den här metoden uppdaterar hitboxen
        /// </summary>
        private void UpdateHitbox()
        {
            if (meleeDirection == down)
            {
                rect = new Rectangle((int)cleavePos.X - (tex.Height * 2), (int)cleavePos.Y, tex.Height * 4, tex.Width + (tex.Width / 2));
            }

            else if (meleeDirection == up)
            {
                rect = new Rectangle((int)cleavePos.X - (tex.Height * 2), (int)cleavePos.Y - tex.Width - (tex.Width / 2), tex.Height * 4, tex.Width + (tex.Width / 2));
            }

            else if (meleeDirection == right)
            {
                rect = new Rectangle((int)cleavePos.X, (int)cleavePos.Y - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }

            else if (meleeDirection == left)
            {
                rect = new Rectangle((int)cleavePos.X - tex.Width - (tex.Width / 2), (int)cleavePos.Y - (tex.Height * 2), tex.Width + (tex.Width / 2), tex.Height * 4);
            }
        }
        /// <summary>
        /// Den här metoden bestämmer riktning på vektorerna
        /// </summary>
        private void DirectionOfVectors()
        {
            right = new Vector2(1, 0);
            left = new Vector2(-1, 0);
            up = new Vector2(0, -1);
            down = new Vector2(0, 1);
        }

        private void DoDamage()
        {
            foreach (var e in enemiesHitList)
            {
                e.HealthPoints -= Damage;
            }
        }
    }
}
