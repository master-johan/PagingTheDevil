using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Trap : Ability
    {
        float originalSpeed;
        float calculatedSpeed;
        float timePassed;
        float noHitTimer;
       
        public Trap(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            Damage = ValueBank.TrapDmg;

            coolDownTime = ValueBank.TrapCooldown;
            Active = true;

            btnTexture = TextureBank.abilityButtonList[2];
        }

        public override void Update(GameTime gameTime)
        {
            UpdateRect();

            if (HitCharacter != null)
            {
                rect.Height = 0;
                rect.Width = 0;

                if (Active)
                {
                    ApplyDamage();

                    Active = false;

                    originalSpeed = (HitCharacter as Enemy).MovementSpeed;

                    calculatedSpeed = originalSpeed / 2;

                    SoundBank.SoundEffectList[10].Play();
                }
                timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                SlowEffect();
            }
            else if (HitCharacter == null)
            {
                noHitTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                RemoveTrap();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        /// <summary>
        /// Den här metoden sköter slow
        /// </summary>
        protected void SlowEffect()
        {
            (HitCharacter as Enemy).MovementSpeed = 0;

            if (timePassed >= ValueBank.TrapTimer)
            {
                (HitCharacter as Enemy).MovementSpeed = (int)originalSpeed;
                ToRemove = true;
            }
        }
        /// <summary>
        /// Den här metoden tar bort trappen om den inte träffar en fiende
        /// </summary>
        private void RemoveTrap()
        {
            if (noHitTimer >=ValueBank.TrapNoHitTimer)
            {
                ToRemove = true;
            }
        }
    }
}
