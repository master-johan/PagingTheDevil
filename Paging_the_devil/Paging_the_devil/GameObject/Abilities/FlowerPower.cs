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
        List<Player> playerList;

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
            playerList.Add(player);
            btnTexture = TextureBank.hudTextureList[5];
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)pos.X - tex.Width / 2;
            rect.Y = (int)pos.Y - tex.Height / 2;

            Vector2 temp = healPos - player.pos;
            healPos -= temp;

            if (HitCharacter != null)
            {
                bool hasHitBefore = false;
                foreach (var p in playerList)
                {
                    if (HitCharacter is Player)
                    {
                        if (HitCharacter == p)
                        {
                            hasHitBefore = true;
                        }
                    }
                }
                if (!hasHitBefore)
                {
                    if (HitCharacter is Player)
                    {
                        playerList.Add(HitCharacter as Player);
                    }
                }
            }

            hit = true;
            Active = true;

            if (hit)
            {
                Heal(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, healPos, new Rectangle(0, 0, 400, 400), tauntColor, 0, new Vector2(200, 200), 1, SpriteEffects.None, 1);
        }

        private void Heal(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Active == true)
            {
                foreach (var p in playerList)
                {
                    p.HealthPoints += 0.2f;
                }
                tauntColor.R--;
                tauntColor.G--;
                tauntColor.B--;
                tauntColor.A--;
            }

            if (timePassed >= ValueBank.TauntTimer)
            {
                Active = false;
                ToRemove = true;
            }
        }
    }
}
