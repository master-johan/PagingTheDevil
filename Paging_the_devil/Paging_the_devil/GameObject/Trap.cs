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
    class Trap : Ability
    {
        DateTime dateTime;
        public TimeSpan timePassed { get; set; }
        public Trap(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            Damage = ValueBank.TrapDmg;
            dateTime = DateTime.Now;
        }

        public override void Update()
        {
             timePassed = DateTime.Now - dateTime;
             rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
