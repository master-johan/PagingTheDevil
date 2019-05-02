using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class Enemy : Character
    {
        public bool toRevome;

        protected bool left;
        protected bool right;

        protected int shootTimer;

        protected Vector2 direction;

        public List<Ability> enemyAbilityList;

        public bool HitBySlowTrap { get; set; }
        public int MovementSpeed { get; set; }
        public int BaseMoveSpeed { get; set; }
        public double TrapTimer { get; set; }

        public bool UpMovementBlocked { get; set; }
        public bool DownMovementBlocked { get; set; }
        public bool LeftMovementBlocked { get; set; }
        public bool RightMovementBlocked { get; set; }

        public Vector2 GetDirection { get { return direction; } }

        public Enemy(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            enemyAbilityList = new List<Ability>();
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            Dead();

            foreach (var e in enemyAbilityList)
            {
                e.Update(gameTime);
            }

            if (shootTimer > 0)
            {
                shootTimer--;
            }

            if (HitBySlowTrap)
            {
                TrapTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                TrapTimer = ValueBank.TrapTimer;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var e in enemyAbilityList)
            {
                e.Draw(spriteBatch);
                spriteBatch.Draw(TextureBank.hudTextureList[0], rect, Color.Black);
            }
            
        }

        /// <summary>
        /// Den här metoden sköter vad som händer vid död.
        /// </summary>
        protected void Dead()
        {
            if (HealthPoints <= 0)
            {
                toRevome = true;
            }
            else
            {
                toRevome = false;
            }
        }
        /// <summary>
        /// Den här metoden sköter rörelsen.
        /// </summary>
        protected virtual void Movement(GameTime gameTime)
        {

        }
        /// <summary>
        /// Den här metoden kollar ifall ändrar movement är tilgängligt
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        protected Vector2 CheckIfAllowedMovement (Vector2 direction)
        {
            if (UpMovementBlocked && direction.Y > 0)
            {
                direction.Y = -1;

            }
            if (DownMovementBlocked && direction.Y < 0)
            {
                direction.Y = 1;
            }

            if (RightMovementBlocked && direction.X > 0)
            {
                direction.X = -1;
            }
            if (LeftMovementBlocked && direction.X < 0)
            {
                direction.X = 1;
            }

            return direction;
        }
    }
}
