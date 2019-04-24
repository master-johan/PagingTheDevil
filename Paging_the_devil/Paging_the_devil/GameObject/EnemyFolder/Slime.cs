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
    class Slime : Enemy
    {
        Rectangle srcRect;
        Player[] playerArray;
        Player targetPlayer;
        int nrOfPlayer;
        int frame;
        int spriteCount;
        int spriteWidth;
        int posX, posY;
        float radius;
        float shortestDistanceToPlayer;
        float scale;
        double timer;
        double interval;
        Vector2 direction;


        public Slime(Texture2D tex, Vector2 pos, Player[] player, int nrOfPlayer) : base(tex, pos)
        {
            this.playerArray = player;
            this.nrOfPlayer = nrOfPlayer;

            interval = 200;
            radius = 500;
            scale = 2f;
            spriteCount = 5;
            spriteWidth = tex.Width;
            HealthPoints = ValueBank.SlimeHealth;
            srcRect = new Rectangle(0, 0, 32, 32);
            rect = new Rectangle((int)pos.X, (int)pos.Y, 32 * (int)scale, 32 * (int)scale);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcRect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Animation(gameTime);
            GetTarget();
            Movement();
        }
        private void Animation(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer <= 0)
            {
                timer = interval;
                frame++;
                srcRect.Y = (frame % spriteCount) * spriteWidth;
            }
        }
        protected override void Movement()
        {
            if (targetPlayer != null && !targetPlayer.Dead)
            {
                direction = targetPlayer.GetSetPos - pos;
                direction.Normalize();

            }
            
            else
            {
                direction = new Vector2(-1, 0);
                direction.Normalize();      
            }

            pos += direction * ValueBank.SlimeSpeed;


        }
        private void GetTarget()
        {
            for (int i = 0; i < nrOfPlayer; i++)
            {
                shortestDistanceToPlayer = Vector2.Distance(playerArray[i].GetSetPos, pos);

                if (shortestDistanceToPlayer < radius)
                {
                    targetPlayer = playerArray[i];
                }
            }
        }
    }
}
