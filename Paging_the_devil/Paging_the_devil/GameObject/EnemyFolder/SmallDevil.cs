﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class SmallDevil : Enemy
    {
        Ability fireball;
        Player[] playerArray;
        Player targetPlayer;

        int nrOfPlayers;
        int frame;
        int spriteCount;
        int spriteWidth;
        int posX, posY;

        float radiusForChasing;
        float radiusForFleeing;
        float safetyRadiusOuter;
        float safetyRadiusInner;
        float distanceToPlayer;
       
        float scale;
        float randomPosTimer;

        double timer;
        double interval;

        bool fleeing;
        bool safeZone;
        bool chase;

        Vector2 direction;
        public SmallDevil(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayers) : base(tex, pos)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;

            HealthPoints = ValueBank.SmallDevilHealth;
            shootTimer = ValueBank.SmallDevilShootTimer;
            left = true;
            right = false;

            radiusForChasing = 400;

            safetyRadiusOuter = 250;
            safetyRadiusInner = 200;

            radiusForFleeing = 150;

            randomPosTimer = 0.2f;
            posX = 0;
            posY = 0;

            MovementSpeed = ValueBank.SmallDevilMoveSpeed;
            BaseMoveSpeed = MovementSpeed;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(tex, pos, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GetTarget();
            Movement(gameTime);
            ShootFireball();
        }

        protected override void Movement(GameTime gameTime)
        {
            if (targetPlayer != null && !targetPlayer.Dead && !fleeing && !safeZone)
            {
                direction = targetPlayer.GetSetPos - pos;
                direction.Normalize();
                pos += direction * ValueBank.SmallDevilMoveSpeed;
            }

            else if (targetPlayer != null && !targetPlayer.Dead && fleeing && !safeZone)
            {
                direction = targetPlayer.GetSetPos - pos;
                direction.Normalize();
                pos -= direction * ValueBank.SmallDevilMoveSpeed;
            }
            else if (safeZone && !fleeing)
            {
                randomPosTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (randomPosTimer <= 0)
                {
                    randomPosTimer = 0.2f;
                    posX = ValueBank.rand.Next(-1, 1);
                    posY = ValueBank.rand.Next(-1, 1);
                  
                    if (posX != 0 && posY != 0)
                    {                        
                        direction = new Vector2(posX, posY);
                        direction.Normalize(); 
                    }                    
                }
                pos += direction * ValueBank.SmallDevilIdleMoveSpeed;
                

            }
            //else 
            //{
            //    direction = new Vector2(-1, 0);
            //    direction.Normalize();
            //    pos += direction * ValueBank.SmallDevilIdleMoveSpeed;
            //}         
        }

        /// <summary>
        /// Den här metoden skjuter fireballs.
        /// </summary>
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

                fireball = new Fireball(TextureManager.mageSpellList[0], pos, dir);
                enemyAbilityList.Add(fireball);
                shootTimer = ValueBank.SmallDevilShootTimer;
            }
        }
        private void GetTarget()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                distanceToPlayer = Vector2.Distance(playerArray[i].GetSetPos, pos);


                if (distanceToPlayer <= radiusForChasing && distanceToPlayer >= safetyRadiusOuter)
                {
                    targetPlayer = playerArray[i];
                    fleeing = false;
                    safeZone = false;
                }

                else if (distanceToPlayer <= radiusForFleeing && distanceToPlayer <= safetyRadiusInner)
                {
                    targetPlayer = playerArray[i];
                    fleeing = true;
                    safeZone = false;
                }

                else if (distanceToPlayer >= safetyRadiusInner && distanceToPlayer <= safetyRadiusOuter)
                {
                    safeZone = true;
                    fleeing = false;
                }
            }
        }
    }
}
