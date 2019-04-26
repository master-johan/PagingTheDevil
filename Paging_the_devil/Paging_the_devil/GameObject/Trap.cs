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
    class Trap : Ability
    {
        DateTime dateTime;
        public TimeSpan timePassed { get; set; }
        float originalSpeed;
        float calculatedSpeed;
        public Trap(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            Damage = ValueBank.TrapDmg;
            dateTime = DateTime.Now;
            coolDownTime = ValueBank.TrapCooldown;
            btnTexture = TextureManager.abilityButtonList[2];
        }
        public override void Update()
        {
            
            UpdateRect();

            if (HitCharacter != null)
            {
                if (Active)
                {
                    ApplyDamage();
                    Active = false;
                    originalSpeed = (HitCharacter as Enemy).MovementSpeed;
                    calculatedSpeed = originalSpeed / 2; 
                }
                timePassed = DateTime.Now - dateTime;
                SlowEffect();

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

        protected void SlowEffect()
        {

            (HitCharacter as Enemy).MovementSpeed = (int)calculatedSpeed;
            if (timePassed.Seconds >= ValueBank.TrapTimer  )
            {
                ToRemove = true;
                (HitCharacter as Enemy).MovementSpeed = (int)originalSpeed; 
            }
        }
    }
}
