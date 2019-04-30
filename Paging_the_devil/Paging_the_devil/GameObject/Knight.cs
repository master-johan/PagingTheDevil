using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject
{
    class Knight : Player
    {
        public Knight(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base (tex, pos, playerIndex, Controller)
        {
            HealthPoints = ValueBank.KnightHealth;
            maxHealthPoints = HealthPoints;

            Ability1 = new Slash(TextureBank.mageSpellList[1], pos, LastDirection,this);
            Ability2 = new Fireball(TextureBank.mageSpellList[0], pos, LastDirection);
            Ability3 = new Trap(TextureBank.mageSpellList[2], pos, new Vector2(0, 0));
        }
        /// <summary>
        /// Den här metoden sköter ability1
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility1()
        {
            Ability ability = new Slash(TextureBank.mageSpellList[1], pos, LastDirection,this);
            Ability1CooldownTimer = ability.coolDownTime; 
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability2
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility2()
        {
            Ability ability = new Fireball(TextureBank.mageSpellList[0], pos, LastDirection);
            Ability2CooldownTimer = ability.coolDownTime;
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability3
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility3()
        {
            Ability ability = new Trap(TextureBank.mageSpellList[2], pos, new Vector2(0, 0));
            Ability3CooldownTimer = ability.coolDownTime;
            return ability;
        }
    }
}
