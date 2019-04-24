using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil.GameObject
{
    class Barbarian : Player
    {
        int fireballTimer;
        
        public Barbarian(Texture2D tex, Vector2 pos, int playerIndex, Controller controller) : base(tex, pos, playerIndex, controller)
        {
            fireballTimer = 0;
            HealthPoints = ValueBank.BarbarianHealth;
            maxHealthPoints = HealthPoints;
        }

        public override void Update(GameTime gameTime)
        {
            if (Controller.ButtonPressed(Buttons.X))
            {
                if (fireballTimer == 0)
                {
                    ShootFireball();
                }
            }

            if (fireballTimer > 0)
            {
                fireballTimer--;
            }

            base.Update(gameTime);

        }
        private void ShootFireball()
        {
            spellDirection = lastInputDirection;
            spellDirection.Normalize();
            spellDirection.Y = -spellDirection.Y;

            ability = new Fireball(TextureManager.mageSpellList[0], pos, spellDirection);
            abilityList.Add(ability);

            fireballTimer = 60;
        }
    }
}
