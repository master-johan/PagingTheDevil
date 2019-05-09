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
        bool block;

        public List<Player> playerList;
        public bool hasHit;

        public WebBall(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            speed = ValueBank.WebballSpeed;
            Damage = ValueBank.WebballDmg;

            Active = false;
            hasHit = false;
            block = false;


            playerList = new List<Player>();
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
                if (!block)
                {
                    spriteBatch.Draw(TextureBank.mageSpellList[12], (HitCharacter as Player).GetRect, Color.White);
                }
            }
        }
        private void WebRoot(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (HitCharacter != null)
            {
                Active = true;
                if (!damage)
                {
                    ApplyDamage();
                    timePassed = 2000;
                    damage = true;
                    hasHit = true;
                }

                playerList.Add(HitCharacter as Player);

                for (int i = 0; i < (HitCharacter as Player).abilityList.Count; i++)
                {
                    if (((HitCharacter as Player).abilityList[i]) is Block)
                    {
                        block = true;
                    }
                }

                if (!block)
                {
                    (HitCharacter as Player).movementSpeed = 0;
                }
            }

            if (timePassed >= ValueBank.WebRootTimer)
            {
                if (HitCharacter != null)
                {
                    (HitCharacter as Player).movementSpeed = ValueBank.PlayerSpeed;
                }
                Active = false;
                timePassed = 0;
                ToRemove = true;
            }
        }
    }
}
