using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.GameObject.Characters
{
    class Knight : Player
    {
        public Knight(Texture2D tex, Vector2 pos, int playerIndex, Controller Controller) : base (tex, pos, playerIndex, Controller)
        {
            HealthPoints = ValueBank.KnightHealth;
            maxHealthPoints = HealthPoints;

            Ability1 = new Block(TextureBank.roomTextureList[0], pos, LastDirection, this);
            Ability2 = new Slash(TextureBank.mageSpellList[1], pos, LastDirection, this); 
            Ability3 = new Taunt(TextureBank.mageSpellList[10], pos, new Vector2(0, 0),this);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (var a in abilityList)
            {
                if (a is Block && (a as Block).Active)
                {
                    spriteBatch.Draw(tex, pos, drawRect, Color.Gold, rotation, new Vector2(25, 30), 1, SpriteEffects.None, 1);
                }
            }
        }
        /// <summary>
        /// Den här metoden sköter ability1
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility1()
        {
            Ability ability = new Block(null, pos, LastDirection, this);
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
            SoundBank.SoundEffectList[4].Play();
            return ability;

        }
        /// <summary>
        /// Den här metoden sköter ability3
        /// </summary>
        /// <returns></returns>
        protected override Ability CastAbility3()
        {
            Ability ability = new Taunt(TextureBank.mageSpellList[9], pos, new Vector2(0, 0), this);
            Ability3CooldownTimer = ability.coolDownTime;
            SoundBank.SoundEffectList[7].Play();
            return ability;
        }
    }
}
