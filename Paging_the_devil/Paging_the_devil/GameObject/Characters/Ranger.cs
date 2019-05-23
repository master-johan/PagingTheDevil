using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.GameObject.Characters
{
    class Ranger : Player
    {
        public Ranger(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base(tex, pos, playerIndex, Controller)
        {
            Ability1 = new Trap(TextureBank.mageSpellList[2], pos, new Vector2(0, 0));
            Ability2 = new Arrow(TextureBank.mageSpellList[4], pos, LastDirection);
            Ability3 = new Dash(tex, pos, LastDirection, this, false);

            HealthPoints = ValueBank.RangerHealth;
            maxHealthPoints = HealthPoints;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
        /// <summary>
        /// Den här metoden sköter ability1
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility1()
        {
            Ability ability = new Trap(TextureBank.mageSpellList[2], pos, new Vector2(0, 0));
            Ability1CooldownTimer = ability.coolDownTime;
            SoundBank.SoundEffectList[9].Play();
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability2
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility2()
        {
            Ability ability = new Arrow(TextureBank.mageSpellList[4], pos, LastDirection);
            Ability2CooldownTimer = ability.coolDownTime;
            SoundBank.SoundEffectList[0].Play();
            return ability;
        }
        /// <summary>
        /// Den här metoden sköter ability3
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility3()
        {
            Ability ability = new Dash(tex, pos, LastDirection, this, true);
            Ability3CooldownTimer = ability.coolDownTime;
            SoundBank.SoundEffectList[8].Play();
            return ability;
        }
    }
}
