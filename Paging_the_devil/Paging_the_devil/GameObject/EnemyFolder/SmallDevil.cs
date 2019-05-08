using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Abilities;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class SmallDevil : Enemy
    {
        Ability fireball;

        Player[] playerArray;

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

        Vector2 temp;

        public SmallDevil(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayers) : base(tex, pos)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;

            HealthPoints = ValueBank.SmallDevilHealth;
            shootTimer = ValueBank.SmallDevilShootTimer;
            left = true;
            right = false;

            radiusForChasing = 400;
            safetyRadiusOuter = 350;
            safetyRadiusInner = 300;
            radiusForFleeing = 250;
            randomPosTimer = 0.2f;

            MovementSpeed = (int)ValueBank.SmallDevilMoveSpeed;
            BaseMoveSpeed = MovementSpeed;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            temp = Vector2.Zero;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            ChasingOrFleeingOrSafe();
            Movement(gameTime);
            ShootFireball();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(tex, pos, Color.White);
            spriteBatch.Draw(tex, rect, Color.Black);
        }  
        /// <summary>
        /// Den här metoden sköter devilens rörelse
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Movement(GameTime gameTime)
        {
            //if(!DownMovementBlocked && !UpMovementBlocked && !LeftMovementBlocked && !RightMovementBlocked)
            //{
            bool chasing = false;
            chasing = CheckIfAllowedMovement();

            if (chasing)
            {
                fleeing = false;
                safeZone = false;
            }

            if (targetPlayer != null && !targetPlayer.Dead && !fleeing && !safeZone)
            {
                direction = targetPlayer.GetSetPos - pos;
                direction.Normalize();
                temp = direction;
            }

            else if (targetPlayer != null && !targetPlayer.Dead && fleeing && !safeZone)
            {
                direction = targetPlayer.GetSetPos - pos;
                direction.Normalize();
                temp = -direction;
            }
            else if (safeZone && !fleeing)
            {
                randomPosTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (randomPosTimer <= 0)
                {
                    randomPosTimer = 0.2f;
                    posX = ValueBank.rand.Next(-1, 2);
                    posY = ValueBank.rand.Next(-1, 2);

                    if (posX != 0 && posY != 0)
                    {
                        direction = new Vector2(posX, posY);
                        direction.Normalize();
                    }
                }
                temp = direction;
            }

            pos += temp * MovementSpeed;
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

                fireball = new Fireball(TextureBank.mageSpellList[0], pos, dir);
                enemyAbilityList.Add(fireball);
                shootTimer = ValueBank.SmallDevilShootTimer;
            }
        }
        /// <summary>
        /// Den här metoden sköter ifall smallDevil ska jaga/fly eller vara säker
        /// </summary>
        private void ChasingOrFleeingOrSafe()
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

                else if (distanceToPlayer <= safetyRadiusOuter && distanceToPlayer >= safetyRadiusInner)
                {
                    safeZone = true;
                    fleeing = false;
                }
            }
        }
    }
}
