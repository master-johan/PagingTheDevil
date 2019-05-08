using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;
using System;
using System.Collections.Generic;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Taunt : Ability
    {
        List<Enemy> enemyList;

        Player player;

        float timePassed;

        bool hit;
        public bool Active { get; private set; }

        Color tauntColor;

        public Taunt(Texture2D tex, Vector2 pos, Vector2 direction, Player player) : base(tex, pos, direction)
        {
            this.player = player;

            Active = false;
            hit = false;

            enemyList = new List<Enemy>();

            rect = new Rectangle((int)pos.X - tex.Width / 2, (int)pos.Y - tex.Height / 2, 400, 400);

            tauntColor = new Color(255, 255, 255, 255);
            btnTexture = TextureBank.hudTextureList[7];
            coolDownTime = 600;
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)pos.X - tex.Width / 2;
            rect.Y = (int)pos.Y - tex.Height / 2;

            if (HitCharacter != null)
            {
                enemyList.Add(HitCharacter as Enemy);
            }

            hit = true;
            Active = true;

            if (hit)
            {
                Taunted(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 400, 400), tauntColor, 0, new Vector2(200, 200), 1, SpriteEffects.None, 1);
        }

        private void Taunted(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Active == true)
            {
                foreach (var e in enemyList)
                {
                    e.targetPlayer = player;
                    e.Taunted = true;
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
                foreach (var e in enemyList)
                {
                    e.Taunted = false;
                }
            }
        }
    }
}
