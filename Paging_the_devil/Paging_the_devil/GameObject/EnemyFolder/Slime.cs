﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;
using System;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class Slime : Enemy
    {
        Rectangle srcRect;

        Player[] playerArray;

        Vector2 distanceDifference;

        int nrOfPlayer;
        int frame;
        int spriteCount;
        int spriteWidth;

        float scale;
        float oldDistance;
        float currentDistance;

        double timer;
        double interval;

        public Slime(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayer) : base(tex, pos)
        {
            this.playerArray = playerArray;
            this.nrOfPlayer = nrOfPlayer;

            //MovementSpeed = ValueBank.SlimeSpeed;
            interval = 200;
            scale = 2f;
            spriteCount = 5;
            spriteWidth = tex.Width;
            HealthPoints = ValueBank.SlimeHealth;
            oldDistance = int.MaxValue;

            srcRect = new Rectangle(0, 0, 32, 32);
            rect = new Rectangle((int)pos.X, (int)pos.Y, 32 * (int)scale, 32 * (int)scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
            Animation(gameTime);

            if (!Taunted)
            {
                GetTarget();
            }

            Movement(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcRect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Den här metoden animerar 
        /// </summary>
        /// <param name="gameTime"></param>
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
        /// <summary>
        /// Den här metoden sköter slimens rörelse
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Movement(GameTime gameTime)
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
        /// <summary>
        /// Den här metoden sköter slimens target
        /// </summary>
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
    }
}
