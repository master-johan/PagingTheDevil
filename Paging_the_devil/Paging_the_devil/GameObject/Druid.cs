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
            HealthPoints = ValueBank.DruidHealth;
            maxHealthPoints = HealthPoints;

            Ability1 = new Slash(TextureManager.mageSpellList[1], pos, lastInputDirection,this);
            Ability2 = new Healharm(TextureManager.mageSpellList[3], pos, lastInputDirection);
            Ability3 = new Trap(TextureManager.mageSpellList[2], pos, new Vector2(0, 0));
        }

        protected override Ability CastAbility1()
        {
            Ability ability = new Slash(TextureManager.mageSpellList[1], pos, lastInputDirection,this);
            Ability1CooldownTimer = ability.coolDownTime;
            return ability;
        }

        protected override Ability CastAbility2()
        {
            Ability ability = new Healharm(TextureManager.mageSpellList[3], pos, lastInputDirection);
            Ability2CooldownTimer = ability.coolDownTime;
            return ability;
        }

        protected override Ability CastAbility3()
        {
            Ability ability = new Trap(TextureManager.mageSpellList[2], pos, new Vector2(0, 0));
            Ability3CooldownTimer = ability.coolDownTime;
            return ability;

        }
        public override void Update(GameTime gameTime)
        {
            //if (Controller.ButtonPressed(Buttons.X))
            //{
            //    if (healHarmTimer == 0)
            //    {
            //        ShootHealHarm();
            //    }
            //}

            //if (healHarmTimer > 0)
            //{
            //    healHarmTimer--;
            //}

            base.Update(gameTime);

        }
        //private void ShootHealHarm()
        //{
        //    spellDirection = lastInputDirection;
        //    spellDirection.Normalize();
        //    spellDirection.Y = -spellDirection.Y;

        //    ability = new Healharm(TextureManager.mageSpellList[3], pos, spellDirection);
        //    abilityList.Add(ability);

        //    healHarmTimer = 60;
        //}
    }
}
