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

            Ability1 = new Dash(tex, pos, LastDirection,this,false);
            Ability2 = new Arrow(TextureManager.mageSpellList[4], pos, LastDirection);
            Ability3 = new Trap(TextureManager.mageSpellList[2], pos, new Vector2(0, 0));

            HealthPoints = ValueBank.RangerHealth;
            maxHealthPoints = HealthPoints;

        }

        protected override Ability CastAbility1()
        {
            Ability ability = new Dash(tex, pos, LastDirection,this,true);
            Ability1CooldownTimer = ability.coolDownTime;
            return ability;
        }

        protected override Ability CastAbility2()
        {
            Ability ability = new Arrow(TextureManager.mageSpellList[4], pos, LastDirection);
            Ability2CooldownTimer = ability.coolDownTime;
            return ability;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }

        protected override Ability CastAbility3()
        {

            Ability ability = new Trap(TextureManager.mageSpellList[2], pos, new Vector2(0, 0));
            Ability3CooldownTimer = ability.coolDownTime;
            return ability;

        }
    }
}
