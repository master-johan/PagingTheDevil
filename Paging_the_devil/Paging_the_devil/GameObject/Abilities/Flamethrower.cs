using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Abilities
{
    class Flamethrower : Ability
    {
        double timePassed;

        public Flamethrower(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            Damage = ValueBank.FlamethrowerDmg;
        }

        public override void Update(GameTime gameTime)
        {

            if (Active)
            {
                ApplyDamage();

                timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timePassed >= ValueBank.WebRootTimer)
                {
                    ToRemove = true;
                    Active = false;
                    timePassed = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
