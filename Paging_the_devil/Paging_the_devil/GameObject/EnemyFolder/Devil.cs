using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class Devil : Enemy
    {
        Player[] playerArray;

        Player targetPlayer;

        Ability fireball;
        Ability cleave;

        int nrOfPlayer;
        int frame;
        int spriteCount;
        int spriteWidth;

        float radius;
        float[] shortestDistanceToPlayer;
        float scale;

        double timer;
        double interval;

        Rectangle drawRect;
        Rectangle up;
        Rectangle down;
        Rectangle left;
        Rectangle right;

        Vector2 temp;
        Vector2 tempNow;

        public Devil(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayer) : base(tex, pos)
        {
            this.playerArray = playerArray;
            this.nrOfPlayer = nrOfPlayer;

            interval = 200;
            radius = 1000;
            scale = 2f;
            spriteCount = 12;
            spriteWidth = tex.Width;
            HealthPoints = ValueBank.SlimeHealth;
            temp = Vector2.Zero;

            shortestDistanceToPlayer = new float[nrOfPlayer]; 

            up = new Rectangle(0, 315, 70, 100);
            down = new Rectangle(0, 0, 70, 100);
            left = new Rectangle(0, 945, 70, 100);
            right = new Rectangle(0, 630, 70, 100);
            drawRect = down;

            rect = new Rectangle((int)pos.X, (int)pos.Y, 70, 100);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GetTarget();
            Movement(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(tex, pos, drawRect, Color.White);
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
                direction = new Vector2(-1, 0);
                direction.Normalize();
            }

            float nowX = tempNow.X;
            float nowY = tempNow.Y;
            float lastX = temp.X;
            float lastY = temp.Y;

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

            pos += direction * ValueBank.DevilSpeed;
        }
        private void GetTarget()
        {
            float result = 0;
            for (int i = 0; i < nrOfPlayer; i++)
            {
                shortestDistanceToPlayer[i] = Vector2.Distance(playerArray[i].GetSetPos, pos);

                if (result < radius - shortestDistanceToPlayer[i])
                {
                    result = radius - shortestDistanceToPlayer[i];
                    targetPlayer = playerArray[i];
                }
            }
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
                shootTimer = ValueBank.SmallDevilShootTimer;
            }
        }
    }
}
