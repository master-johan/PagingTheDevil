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
    class Dash : Ability
    {
        Player player;
        static DateTime StartTime;
        TimeSpan timePassed { get; set; }
        public bool Active { get; set; }

        public Dash(Texture2D tex, Vector2 pos, Vector2 direction,Player player, bool Active) : base(tex, pos, direction)
        {
            this.tex = null;
            this.player = player;
            this.Active = Active;
            btnTexture = TextureManager.abilityButtonList[5];
            coolDownTime = ValueBank.DashCooldown;
            StartTime = DateTime.Now;
        }
        public override void Update()
        {
            DashUpdate();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {         
        }
        /// <summary>
        /// Denna metod updaterar dash abilityn 
        /// </summary>
        public void DashUpdate()
        {
            
            if (Active)
            {                
                DashDirection(direction);
                player.movementSpeed = ValueBank.DashSpeed;

                TimeSpan timePassed = DateTime.Now - StartTime;
                if (timePassed.TotalSeconds >= ValueBank.DashTimer)
                {
                    player.movementSpeed = ValueBank.PlayerSpeed;
                    Active = false;
                }
            }
        }

        /// <summary>
        /// Denna metod hittar riktningen för dashen
        /// </summary>
        private Vector2 DashDirection(Vector2 direction)
        {
            Vector2 dashDirection = player.LastDirection;

            return direction;
        }
    }
}
