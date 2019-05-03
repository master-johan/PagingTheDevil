using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.GameObject.Characters
{
    class Druid : Player
    {
        public Druid(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base(tex, pos, playerIndex, Controller)
        {
            HealthPoints = ValueBank.DruidHealth;
            maxHealthPoints = HealthPoints;

            Ability1 = new Slash(TextureBank.mageSpellList[1], pos, LastDirection,this);
            Ability2 = new Healharm(TextureBank.mageSpellList[3], pos, LastDirection);
            Ability3 = new FlowerPower(TextureBank.mageSpellList[11], pos, new Vector2(0, 0), this);
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
            Ability ability = new Healharm(TextureBank.mageSpellList[3], pos, LastDirection);
            Ability2CooldownTimer = ability.coolDownTime;
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability3
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility3()
        {
            Ability ability = new FlowerPower(TextureBank.mageSpellList[11], pos, new Vector2(0, 0), this);
            Ability3CooldownTimer = ability.coolDownTime;
            SoundBank.SoundEffectList[11].Play();
            return ability;

        }
    }
}
