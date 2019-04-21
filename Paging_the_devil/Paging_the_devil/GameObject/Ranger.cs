using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject
{
    class Ranger : Player
    {
        public Ranger(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base(tex, pos, playerIndex, Controller)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Controller.ButtonPressed(Buttons.X))
            {
                ability = new Trap(TextureManager.mageSpellList[2], pos, new Vector2(0, 0));
                abilityList.Add(ability);
            }
            base.Update(gameTime);
        }
    }
}
