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


        public Arrow(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            speed = 7;
            Damage = 2;
            rotation = Math.Atan2(direction.Y, direction.X);
            rect = new Rectangle((int)pos.X, (int)pos.Y, TextureManager.mageSpellList[4].Width, TextureManager.mageSpellList[4].Height);
            origin = new Vector2(TextureManager.mageSpellList[4].Width / 2, TextureManager.mageSpellList[4].Height / 2);
        }

        public override void Update()
        {
            pos += direction * speed;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, rect, Color.White, (float)rotation, origin, 1f, SpriteEffects.None, 1f);          
        }
    }
}
