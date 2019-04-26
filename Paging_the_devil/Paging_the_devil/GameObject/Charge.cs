
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject
{
    class Charge : Ability
    {
        Player player;
        static DateTime StartTime;
        TimeSpan timePassed { get; set; }
        public bool Active { get; set; }
        public bool Hit { get; set; }

        public Charge(Texture2D tex, Vector2 pos, Vector2 direction, Player player, bool Active) : base(tex, pos, direction)
        {
            this.tex = null;
            this.player = player;
            this.Active = Active;
            Hit = false;
            Damage = ValueBank.ChargeDmg;           
            btnTexture = TextureManager.abilityButtonList[5];
            coolDownTime = ValueBank.ChargeCooldown;
            StartTime = DateTime.Now;
        }
        public override void Update()
        {
            rect = new Rectangle((int)player.GetSetPos.X - player.GetRect.Width/2, (int)player.GetSetPos.Y - player.GetRect.Height / 2, player.GetRect.Width, player.GetRect.Height);
            ChargeUpdate();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.mageSpellList[0], rect, Color.Black);
        }
        /// <summary>
        /// Denna metod updaterar charge abilityn 
        /// </summary>
        public void ChargeUpdate()
        {
            if (Active)
            {
                ChargeDirection(direction);
                player.movementSpeed = ValueBank.ChargeSpeed;

                TimeSpan timePassed = DateTime.Now - StartTime;
                if (timePassed.TotalSeconds >= ValueBank.ChargeTimer)
                {
                    player.movementSpeed = ValueBank.PlayerSpeed;
                    Active = false;
                }

            
            }
        }
        /// <summary>
        /// Denna metod hittar riktningen för dashen
        /// </summary>
        private Vector2 ChargeDirection(Vector2 direction)
        {
            Vector2 chargeDirection = player.LastDirection;

            return direction;
        }
    }
}



