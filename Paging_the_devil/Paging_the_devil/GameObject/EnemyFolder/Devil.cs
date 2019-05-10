using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;
using Paging_the_devil.GameObject.Abilities;
using Paging_the_devil.GameObject;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class Devil : Enemy
    {
        Player[] playerArray;

        Ability fireball;
        Ability cleave;

        int nrOfPlayer;
        int frame;
        int spriteCount;
        int spriteWidth;
        int cleaveTimer;
        
        float scale;

        float oldDistance;
        float currentDistance;

        double timer;
        double interval;

        Rectangle drawRect;
        Rectangle up;
        Rectangle down;
        Rectangle left;
        Rectangle right;

        Vector2 temp;
        Vector2 tempNow;
        Vector2 distanceDifference;

        public Devil(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayer) : base(tex, pos)
        {
            this.playerArray = playerArray;
            this.nrOfPlayer = nrOfPlayer;

            interval = 200;
            scale = 2f;
            spriteCount = 12;
            spriteWidth = tex.Width;
            HealthPoints = ValueBank.DevilHealth;
            temp = Vector2.Zero;
            oldDistance = int.MaxValue;

            up = new Rectangle(0, 315, 70, 100);
            down = new Rectangle(0, 0, 70, 100);
            left = new Rectangle(0, 945, 70, 100);
            right = new Rectangle(0, 630, 70, 100);
            drawRect = down;

            rect = new Rectangle((int)pos.X - 35, (int)pos.Y - 200, 70, 100);
            shootTimer = ValueBank.SmallDevilShootTimer - ValueBank.SmallDevilShootTimer / 2;

            cleaveTimer = ValueBank.DevilCleaveCooldown;

            MovementSpeed = ValueBank.DevilSpeed;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            rect.X = (int)pos.X - 35;
            rect.Y = (int)pos.Y - 50;
            if (!Taunted)
            {
                GetTarget();
            }
            Movement(gameTime);
            ShootFireball();
            StartLaugh();

            if (cleaveTimer <= 0)
            {
                DevilCleave();
                cleaveTimer = 360;
            }
            cleaveTimer--;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(tex, pos, drawRect, Color.White, 0, new Vector2(drawRect.Width / 2, drawRect.Height / 2), 1, SpriteEffects.None, 0);

            if (Hit)
            {
                spriteBatch.Draw(tex, pos, drawRect, Color.Red, 0, new Vector2(drawRect.Width / 2, drawRect.Height / 2), 1, SpriteEffects.None, 0);
            }
        }
        protected override void Movement(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            temp = direction;

            if (targetPlayer != null && !targetPlayer.Dead)
            {
                direction = targetPlayer.GetSetPos - pos;
                tempNow = direction;
                direction.Normalize();
            }
            else
            {
                if (pos.X <= 50)
                {
                    direction.X = 1;
                    direction.Normalize();
                }
                else if (pos.X >= 1850)
                {
                    direction.X = -1;
                    direction.Normalize();
                }
                if (pos.Y >= 980)
                {
                    direction.Y = -1;
                }
                else if (pos.Y <= ValueBank.GameWindowStartY + 80)
                {
                    direction.Y = 1;
                }
            }

            float nowX = tempNow.X;
            float nowY = tempNow.Y;
            float lastX = temp.X;
            float lastY = temp.Y;

            pos += direction * MovementSpeed;

            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                if (nowX < lastX)
                {
                    PlayerAnimation(left);
                }
                else if (nowX > lastX)
                {
                    PlayerAnimation(right);
                }
            }
            else
            {
                if (nowY < lastY)
                {
                    PlayerAnimation(up);
                }
                else if (nowY > lastY)
                {
                    PlayerAnimation(down);
                }
            }


        }
        private void GetTarget()
        {
            for (int i = 0; i < nrOfPlayer; i++)
            {
                distanceDifference = playerArray[i].pos - pos;

                currentDistance = distanceDifference.LengthSquared();

                if (currentDistance < oldDistance)
                {
                    targetPlayer = playerArray[i];
                    oldDistance = currentDistance;
                }
            }

            oldDistance = int.MaxValue;
        }
        private void PlayerAnimation(Rectangle rect)
        {
            if (timer <= 0)
            {
                timer = interval; frame++;
                drawRect.Y = rect.Y + (frame % 3) * 100 + (5 * (frame % 3));
            }
        }

        private void ShootFireball()
        {
            if (shootTimer == 0)
            {
                double x = ValueBank.rand.NextDouble();
                double y = ValueBank.rand.NextDouble();

                int minusOrNotX = ValueBank.rand.Next(0, 2);
                int minusOrNotY = ValueBank.rand.Next(0, 2);

                if (minusOrNotX == 0)
                {
                    x = -x;
                }
                if (minusOrNotY == 0)
                {
                    y = -y;
                }

                Vector2 dir = new Vector2((float)x, (float)y);
                dir.Normalize();

                fireball = new Fireball(TextureBank.mageSpellList[0], pos, dir);
                enemyAbilityList.Add(fireball);
                shootTimer = ValueBank.SmallDevilShootTimer - ValueBank.SmallDevilShootTimer / 2;
            }
        }
        private void DevilCleave()
        {
            Ability ability = new DevilCleave(TextureBank.mageSpellList[8], pos, direction, this);
            enemyAbilityList.Add(ability);
        }

        public void StartLaugh()
        {
            if (cleaveTimer == ValueBank.DevilCleaveCooldown)
            {
                SoundBank.SoundEffectList[13].Play();
            }
        }
    }
}
