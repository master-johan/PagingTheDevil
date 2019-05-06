using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class FireTurret : Enemy
    {
        public FireTurret(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
          
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(tex, pos, Color.White);
        }


    }
}
