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
    class TargetDummy : Slime
    {
        bool up;
        bool down;

        int maxY;
        int minY;
        public TargetDummy(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayer) : base(tex, pos, playerArray, nrOfPlayer)
        {
            spriteCount = 1;

            Damage = 0.1f;
            HealthPoints = 50000000;

            maxY = 900;
            minY = 250;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ChooseDirection();
            Movement(gameTime);
        }
        private void ChooseDirection()
        {
            if ( pos.Y <= minY) // Nedåt
            {
                down = true;
                up = false;
            }
            if (pos.Y >= maxY) //Uppåt
            {
                up = true;
                down = false;
            }
        }
        protected override void Movement(GameTime gameTime)
        {
            if(up)
            {
                pos.Y -= MovementSpeed;
            }
            if(down)
            {
                pos.Y += MovementSpeed;
            }
        }

    }
}
