using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Enemy : Character
    {
        public bool toRevome;
        bool left;
        bool right;

        int shootTimer;

        Ability fireball;

        public List<Ability> enemyAbilityList;

        Random rand;

        public Enemy(Texture2D tex, Vector2 pos) : base (tex, pos)
        {
            HealthPoints = 10;
            left = true;
            right = false;

            shootTimer = 60;

            enemyAbilityList = new List<Ability>();

            rand = new Random();
        }
        public override void Update()
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            Dead();
            Movement();
            ShootFireball();

            foreach (var e in enemyAbilityList)
            {
                e.Update();
            }

            if (shootTimer > 0)
            {
                shootTimer--;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
            foreach (var e in enemyAbilityList)
            {
                e.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Den här metoden sköter vad som händer vid död.
        /// </summary>
        private void Dead()
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
        private void Movement()
        {
            if (pos.X < 100)
            {
                right = true;
                left = false;
            }
            else if(pos.X > 1000)
            {
                right = false;
                left = true;
            }
            if (right)
            {
                pos.X += 4;
            }
            else if(left)
            {
                pos.X -= 4;
            }
        }
        /// <summary>
        /// Den här metoden skjuter fireballs.
        /// </summary>
        private void ShootFireball()
        {
            if (shootTimer == 0)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                int minusOrNotX = rand.Next(0, 2);
                int minusOrNotY = rand.Next(0, 2);

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
                shootTimer = 60;
            }
        }
    }
}
