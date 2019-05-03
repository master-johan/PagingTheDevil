using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Abilities
{
    class Fireball : Ability
    {
        int speed;
        Vector2 spellDirection;

        public Fireball(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            spellDirection = GetSpellDirection(direction);
            speed = ValueBank.FireballSpeed;
            Damage = ValueBank.FireballDmg;

            Active = true;
            coolDownTime = 60;

            btnTexture = TextureBank.hudTextureList[5];
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
