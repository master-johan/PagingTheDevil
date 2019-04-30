using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using System;

namespace Paging_the_devil.GameObject
{
    class Arrow : Ability
    {
        int speed;

        double rotation;

        Rectangle srsRect;

        Vector2 spellDirection;
        Vector2 origin;

        public Arrow(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            spellDirection = GetSpellDirection(direction);

            speed = ValueBank.ArrowSpeed;
            Damage = ValueBank.ArrowDmg;

            rotation = Math.Atan2(spellDirection.Y, spellDirection.X);

            rect = new Rectangle((int)pos.X, (int)pos.Y, TextureBank.mageSpellList[4].Width, TextureBank.mageSpellList[4].Height);
            srsRect = new Rectangle(0, 0, TextureBank.mageSpellList[4].Width, TextureBank.mageSpellList[4].Height);

            origin = new Vector2(TextureBank.mageSpellList[4].Width / 2, TextureBank.mageSpellList[4].Height / 2);

            btnTexture = TextureBank.abilityButtonList[0];
            coolDownTime = ValueBank.ArrowCooldown;
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
            spriteBatch.Draw(tex, pos, srsRect, Color.White, (float)rotation, origin, 1f, SpriteEffects.None, 1f);          
        }
    }
}
