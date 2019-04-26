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

    

        public Barbarian(Texture2D tex, Vector2 pos, int playerIndex, Controller controller) : base(tex, pos, playerIndex, controller)
        {

            HealthPoints = ValueBank.BarbarianHealth;
            maxHealthPoints = HealthPoints;

            Ability1 = new Slash(TextureManager.mageSpellList[1], pos, LastDirection,this);
            Ability2 = new Cleave(TextureManager.mageSpellList[6], pos, LastDirection,this);
            Ability3 = new Charge(tex, pos, new Vector2(0, 0), this, false);
            
        }

        public override void Update()
        {
            base.Update();
        }

        protected override Ability CastAbility1()
        {
            Ability ability = new Cleave(TextureManager.mageSpellList[6], pos, LastDirection, this);
            Ability1CooldownTimer = ability.coolDownTime;
            return ability;
        }

        protected override Ability CastAbility2()
        {
            Ability ability = new Slash(TextureManager.mageSpellList[1], pos, LastDirection, this);
            Ability2CooldownTimer = ability.coolDownTime;
            return ability;
        }

        protected override Ability CastAbility3()
        {
            Ability ability = new Charge(tex, pos, new Vector2(0, 0), this, true);
            Ability3CooldownTimer = ability.coolDownTime;
            return ability;
        }

    }
}
