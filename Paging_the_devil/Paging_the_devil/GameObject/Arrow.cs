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
    class Arrow : Ability
    {
        int speed;
        Vector2 origin;
        double rotation;
        Rectangle srsRect;
        Vector2 spellDirection;


        public Arrow(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            spellDirection = GetSpellDirection(direction);
            speed = ValueBank.ArrowSpeed;
            Damage = ValueBank.ArrowDmg;
            rotation = Math.Atan2(spellDirection.Y, spellDirection.X);
            rect = new Rectangle((int)pos.X, (int)pos.Y, TextureManager.mageSpellList[4].Width, TextureManager.mageSpellList[4].Height);
            origin = new Vector2(TextureManager.mageSpellList[4].Width / 2, TextureManager.mageSpellList[4].Height / 2);
            srsRect = new Rectangle(0, 0, TextureManager.mageSpellList[4].Width, TextureManager.mageSpellList[4].Height);
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
            spriteBatch.Draw(tex, pos, srsRect, Color.White, (float)rotation, origin, 1f, SpriteEffects.None, 1f);          
        }
    }
}
