using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Healharm : Ability
    {
        int speed;

        float tickTime;
        float counter;

        Vector2 spellDirection; 

        public float timePassed { get; set; }

        public Character character { get; set; }

        //public bool Active { get; set; }

        public bool IsTicking { get; set; }

        public Healharm(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            spellDirection = GetSpellDirection(direction);

            speed = ValueBank.HealHarmSpeed;
            Damage = ValueBank.HealHarmDmg;
            Heal = ValueBank.HealHarmHeal;

            Active = false;
            IsTicking = false;

            btnTexture = TextureBank.hudTextureList[5];

            coolDownTime = 40;
            counter = 1000;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                pos += spellDirection * speed;
               
            }

            else
            {
                rect.Height = 0;
                rect.Width = 0;
            }
            
            UpdateRect();

            if (HitCharacter != null)
            {
                Active = true;

                timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                tickTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (HitCharacter is Enemy)
                {
                    DmgOverTime();
                }

                else if (HitCharacter is Player)
                {
                    HealOverTime();                    
                }
            }

            if (timePassed >= ValueBank.HealHarmTimer)
            {
                Active = false;
                ToRemove = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Active)
            {
                base.Draw(spriteBatch);
                spriteBatch.Draw(TextureBank.hudTextureList[1], rect, Color.White);
            }
        }
        /// <summary>
        /// Den här metoden gör skada över tid
        /// </summary>
        public void DmgOverTime()
        {
            if (Active)
            {
                if (tickTime > counter)
                {
                    ApplyDamage();
                    tickTime = 0; 
                }
            }
        }
        /// <summary>
        /// Den här metoden healar över tid
        /// </summary>
        private void HealOverTime()
        {
            if (Active)
            {
                if (tickTime > counter)
                {
                    ApplyHeal();
                    tickTime = 0; 
                }
            }
        }
    }
}
