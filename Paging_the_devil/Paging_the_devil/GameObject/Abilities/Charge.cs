﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Charge : Ability
    {
        Player player;

        List<Enemy> enemiesHitList;

        float timePassed { get; set; }
        public bool Active { get; set; }
        public bool Hit { get; set; }

        public Charge(Texture2D tex, Vector2 pos, Vector2 direction, Player player, bool Active) : base(tex, pos, direction)
        {
            this.tex = null;
            this.player = player;
            this.Active = Active;

            Hit = false;

            Damage = ValueBank.ChargeDmg;           
            btnTexture = TextureBank.abilityButtonList[5];
            coolDownTime = ValueBank.ChargeCooldown;

            enemiesHitList = new List<Enemy>();
        }
        public override void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            rect = new Rectangle((int)player.GetSetPos.X - player.GetRect.Width/2, (int)player.GetSetPos.Y - player.GetRect.Height / 2, player.GetRect.Width, player.GetRect.Height);

            ChargeUpdate(gameTime);

            if (HitCharacter != null)
            {
                bool hasHitBefore = false;

                foreach (var e in enemiesHitList)
                {
                    if (HitCharacter == e)
                    {
                        hasHitBefore = true;
                    }
                }

                if (!hasHitBefore)
                {
                    ApplyDamage();

                    enemiesHitList.Add(HitCharacter as Enemy);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
        }
        /// <summary>
        /// Denna metod updaterar charge abilityn 
        /// </summary>
        public void ChargeUpdate(GameTime gameTime)
        {
            if (Active)
            {
                ChargeDirection(direction);
                player.movementSpeed = ValueBank.ChargeSpeed;

                if (timePassed >= ValueBank.ChargeTimer)
                {
                    player.movementSpeed = ValueBank.PlayerSpeed;
                    Active = false;
                    ToRemove = true;
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



