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
    class SmallDevil : Enemy
    {

        Ability fireball;
        public SmallDevil(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            HealthPoints = ValueBank.SmallDevilHealth;
            shootTimer = ValueBank.SmallDevilShootTimer;
            left = true;
            right = false;

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
            Movement();
            ShootFireball();
            base.Update(gameTime);
        }

        protected override void Movement()
        {
            if (pos.X < 100)
            {
                right = true;
                left = false;
            }
            else if (pos.X > 1000)
            {
                right = false;
                left = true;
            }
            if (right)
            {
                pos.X += MovementSpeed;
            }
            else if (left)
            {
                pos.X -= MovementSpeed;
            }
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
    }
}
