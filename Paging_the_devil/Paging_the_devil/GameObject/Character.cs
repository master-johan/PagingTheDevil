using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil.GameObject
{
    public class Character : MovingObject
    {
        public float HealthPoints { get; set; }
        

        public Character(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
