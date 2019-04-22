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

        public bool Active { get; set; }

        public Fireball(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            speed = ValueBank.FireballSpeed;
            Damage = ValueBank.FireballDmg;
            Active = true;
        }

        public override void Update()
        {
            pos += direction * speed;

            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
