using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Fireball : Ability
    {
        int speed;
        Vector2 spellDirection;

        public bool Active { get; set; }

        public Fireball(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {

            spellDirection = GetSpellDirection(direction);
            speed = ValueBank.FireballSpeed;
            Damage = ValueBank.FireballDmg;

            Active = true;
            coolDownTime = 60;

            btnTexture = TextureManager.hudTextureList[5];
        }

        public override void Update(GameTime gameTime)
        {
            pos += spellDirection * speed;

            UpdateRect();

            if (HitCharacter != null)
            {
                ApplyDamage();
                ToRemove = true; 
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
