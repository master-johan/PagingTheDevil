using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Ability : GameObject
    {
        Player player;
        public Ability(Texture2D tex, Vector2 pos, Rectangle rect, Player player) : base(tex, pos, rect)
        {
            this.player = player;
        }
        public override void Update()
        {
            if (player.shoot)
            {
                pos.X = pos.X + 5;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
