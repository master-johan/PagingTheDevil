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
            btnTexture = TextureBank.hudTextureList[5];
            coolDownTime = 40;

        }

        public override void Update()
        {
            pos += spellDirection * speed;

            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            timePassed = DateTime.Now - dateTime;

            if (character != null)
            {
                if (character is Enemy)
                {
                    Active = true;
                }

                if (character is Player)
                {
                    Active = true;
                    HealOverTime();                    
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Active)
            {
                base.Draw(spriteBatch);
            }
        }

        public void DmgOverTime(Enemy enemy)
        {
            character = enemy;
            if (Active)
            {              
                dateTime = DateTime.Now;
            }
            TimeSpan timePassed = DateTime.Now - dateTime;

            if (timePassed.TotalSeconds <= ValueBank.HealHarmTimer)
            {
                enemy.HealthPoints -= ValueBank.HealHarmDmg;
                IsTicking = true;
            }
            else
            {
                IsTicking = false;
            }

        }

        private void HealOverTime()
        {
            if (Active)
            {
                dateTime = DateTime.Now;
            }
            TimeSpan timePassed = DateTime.Now - dateTime;

            if (timePassed.TotalSeconds >= ValueBank.HealHarmTimer)
            {
                character.HealthPoints += ValueBank.HealHarmHeal;
            }
        }

    }
}
