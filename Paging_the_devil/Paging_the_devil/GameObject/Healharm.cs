using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.GameObject
{
    class Healharm : Ability
    {
        int speed;
        DateTime dateTime;
        double tickTime;
        double counter;

        Vector2 spellDirection; 
        public TimeSpan timePassed { get; set; }
        public Character character { get; set; }
        public bool Active { get; set; }
        public bool IsTicking { get; set; }

        public Healharm(Texture2D tex, Vector2 pos, Vector2 direction)
            : base(tex, pos, direction)
        {
            spellDirection = GetSpellDirection(direction);
            speed = ValueBank.HealHarmSpeed;
            Active = false;
            IsTicking = false;
            dateTime = DateTime.Now;

            btnTexture = TextureManager.abilityButtonList[3];
            coolDownTime = ValueBank.HealHarmCooldown;



            coolDownTime = 40;
            Damage = ValueBank.HealHarmDmg;
            Heal = ValueBank.HealHarmHeal;
            counter = 1500;

        }

        public override void Update()
        {
            if (!Active)
            {
                pos += spellDirection * speed;
            }
            UpdateRect();
            if (HitCharacter != null)
            {
                if (!Active)
                {
                    dateTime = DateTime.Now;
                }
                
                timePassed = DateTime.Now - dateTime;
                Active = true;
                tickTime += timePassed.TotalMilliseconds; 

                if (HitCharacter is Enemy)
                {
                    DmgOverTime();
                }

                if (HitCharacter is Player)
                {
                    HealOverTime();                    
                }
            }
            if (timePassed.TotalSeconds >= ValueBank.HealHarmTimer)
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
                spriteBatch.Draw(TextureManager.hudTextureList[1], rect, Color.White);
            }
        }

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
            //character = enemy;
            //if (Active)
            //{              
            //    dateTime = DateTime.Now;
            //}
            //TimeSpan timePassed = DateTime.Now - dateTime;

            //if (timePassed.TotalSeconds <= ValueBank.HealHarmTimer)
            //{
            //    enemy.HealthPoints -= ValueBank.HealHarmDmg;
            //    IsTicking = true;
            //}
            //else
            //{
            //    IsTicking = false;
            //}
        }

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
