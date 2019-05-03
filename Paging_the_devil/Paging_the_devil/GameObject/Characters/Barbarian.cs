using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.GameObject.Characters
{
    class Barbarian : Player
    {
        public Barbarian(Texture2D tex, Vector2 pos, int playerIndex, Controller controller) : base(tex, pos, playerIndex, controller)
        {
            HealthPoints = ValueBank.BarbarianHealth;
            maxHealthPoints = HealthPoints;

            Ability1 = new Slash(TextureBank.mageSpellList[1], pos, LastDirection,this);
            Ability2 = new Cleave(TextureBank.mageSpellList[6], pos, LastDirection,this);
            Ability3 = new Charge(tex, pos, new Vector2(0, 0), this, false);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Den här metoden sköter ability1
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility1()
        {
            Ability ability = new Cleave(TextureBank.mageSpellList[6], pos, LastDirection, this);
            Ability1CooldownTimer = ability.coolDownTime;
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability2
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility2()
        {
            Ability ability = new Slash(TextureBank.mageSpellList[1], pos, LastDirection, this);
            Ability2CooldownTimer = ability.coolDownTime;
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability3
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility3()
        {
            Ability ability = new Charge(tex, pos, new Vector2(0, 0), this, true);
            Ability3CooldownTimer = ability.coolDownTime;
            return ability;
        }
    }
}
