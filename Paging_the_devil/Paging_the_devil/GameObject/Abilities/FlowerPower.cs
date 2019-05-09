using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using Paging_the_devil.GameObject.Characters;
namespace Paging_the_devil.GameObject.Abilities
{
    class FlowerPower : Ability
    {
        public List<Player> playerList;

        Player player;

        float timePassed;

        bool hit;
        public bool Active { get; private set; }

        Color tauntColor;

        Vector2 healPos;

        public FlowerPower(Texture2D tex, Vector2 pos, Vector2 direction, Player player) : base(tex, pos, direction)
        {
            this.player = player;

            Active = false;
            hit = false;

            playerList = new List<Player>();

            rect = new Rectangle((int)pos.X - tex.Width / 2, (int)pos.Y - tex.Height / 2, 400, 400);

            tauntColor = new Color(255, 255, 255, 255);
            btnTexture = TextureBank.abilityButtonList[7];
            coolDownTime = 600;
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)player.pos.X - tex.Width / 2;
            rect.Y = (int)player.pos.Y - tex.Height / 2;

            Vector2 temp = healPos - player.pos;
            healPos -= temp;

            if (HitCharacter != null)
            {
                bool hasHitBefore = false;
                foreach (var p in playerList)
                {
                    if (HitCharacter == p)
                    {
                        hasHitBefore = true;
                    }
                }
                if (!hasHitBefore)
                {
                    playerList.Add(HitCharacter as Player);
                }
            }

            hit = true;
            Active = true;
            if (hit)
            {
                FlowerPowerHeal(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, healPos, new Rectangle(0, 0, 400, 400), tauntColor, 0, new Vector2(200, 200), 1, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Den här metoden sköter healen (FlowerPower) 
        /// </summary>
        private void FlowerPowerHeal(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Active)
            {
                foreach (var p in playerList)
                {
                    p.HealthPoints += 0.2f;
                }
                tauntColor.R--;
                tauntColor.G--;
                tauntColor.B--;
                tauntColor.A--;
                //Rensar listan för att inte effekten ska hända varje update.
                playerList.Clear();
            }

            if (timePassed >= ValueBank.TauntTimer)
            {
                Active = false;
                ToRemove = true;
            }
        }
    }
}
