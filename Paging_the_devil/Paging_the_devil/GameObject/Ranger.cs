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
        int arrowTimer;

        public Ranger(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base(tex, pos, playerIndex, Controller)
        {
            arrowTimer = 0;
            HealthPoints = ValueBank.RangerHealth;
            maxHealthPoints = HealthPoints;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Controller.ButtonPressed(Buttons.X))
            {
                ability = new Trap(TextureManager.mageSpellList[2], pos, new Vector2(0, 0));
                abilityList.Add(ability);
            }
            if (Controller.ButtonPressed(Buttons.A))
           
             {
                 if (arrowTimer == 0)
                 {
                        ShootArrow();
                 }
             }

             if (arrowTimer > 0)
             {
                    arrowTimer--;
             }
              
            base.Update(gameTime);
        }

        private void ShootArrow()
        {
            spellDirection = lastInputDirection;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;

            ability = new Arrow(TextureManager.mageSpellList[4], pos, spellDirection);
            abilityList.Add(ability);

            arrowTimer = 60;
        }
    }
}
