using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class Enemy : Character
    {
        public bool toRevome;

        protected bool left;
        protected bool right;

        public bool hitBySlowTrap { get; set; }

        protected int shootTimer;
        public int MovementSpeed { get; set; }
        public int BaseMoveSpeed { get; set; }
        public double TrapTimer { get; set; }



        public List<Ability> enemyAbilityList;


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

            if (hitBySlowTrap)
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


    }
}
