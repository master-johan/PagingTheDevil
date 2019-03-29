using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Slash : Ability
    {
        public Slash(Texture2D tex, Vector2 pos, Player player, Vector2 direction) 
            : base (tex, pos, player, direction)
        {

        }
        public override void Update()
        {
            if (player.slash)
            {

            }
        }
    }
}
