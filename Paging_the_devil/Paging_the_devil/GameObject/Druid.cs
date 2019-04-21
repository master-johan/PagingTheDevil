using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Druid : Player
    {
        int healHarmTimer;

        public Druid(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base(tex, pos, playerIndex, Controller)
        {

        }
        public override void Update(GameTime gameTime)
        {
            if (Controller.ButtonPressed(Buttons.X))
            {
                if (healHarmTimer == 0)
                {
                    ShootHealHarm();
                }
            }

            if (healHarmTimer > 0)
            {
                healHarmTimer--;
            }

            base.Update(gameTime);

        }
        private void ShootHealHarm()
        {
            spellDirection = lastInputDirection;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;

            ability = new Healharm(TextureManager.mageSpellList[3], pos, spellDirection);
            abilityList.Add(ability);

            healHarmTimer = 60;
        }
    }
}
