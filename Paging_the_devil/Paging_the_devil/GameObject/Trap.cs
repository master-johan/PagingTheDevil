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
        float counter;
        float tickTime;
        float originalSpeed;
        float calculatedSpeed;
        float timePassed;
       
        public Trap(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            Damage = ValueBank.TrapDmg;
            coolDownTime = 40;
            btnTexture = TextureManager.hudTextureList[5];
            Active = true; 
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
                    SoundManager.SoundEffectList[10].Play();
                }
                timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                SlowEffect();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }

        protected void SlowEffect()
        {

            (HitCharacter as Enemy).MovementSpeed = 0;
            if (timePassed >= ValueBank.TrapTimer  )
            {
                (HitCharacter as Enemy).MovementSpeed = (int)originalSpeed;
                ToRemove = true;
            }
        }
    }
}
