using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.Characters;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Abilities
{
    class WebBall : Fireball
    {
        float timePassed;
        bool damage;

        public WebBall(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            speed = ValueBank.WebballSpeed;
            Damage = ValueBank.WebballDmg;

            Active = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                pos += spellDirection * speed;

            }

            else
            {
                rect.Width = 0;
                rect.Height = 0;
            }
            UpdateRect();
            WebRoot(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Active)
            {
                base.Draw(spriteBatch);
            }

            else
            {
                spriteBatch.Draw(TextureBank.mageSpellList[12], (HitCharacter as Player).GetRect, Color.White);
            }
        }
        private void WebRoot(GameTime gameTime)
        {
            if (HitCharacter != null)
            {
                Active = true;

                if (!damage)
                {
                    ApplyDamage();
                    damage = true;
                }

                timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                (HitCharacter as Player).movementSpeed = 0;
            }
           
            if (timePassed >= ValueBank.WebRootTimer)
            {
                (HitCharacter as Player).movementSpeed = ValueBank.PlayerSpeed;
                ToRemove = true;
                Active = false;
                timePassed = 0;
            }
        }
    }
}
