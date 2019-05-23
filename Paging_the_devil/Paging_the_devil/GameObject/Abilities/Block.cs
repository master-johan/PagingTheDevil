using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.GameObject.Abilities
{
    class Block : Ability
    {
        float health;
        float timePassed;

        string blockTimerText;

        bool first;

        Player player;

        Vector2 blockPos;
        Vector2 blockTimerTextPos;

        public bool Active { get; private set; }

        int width;
        int height;

        public Block(Texture2D tex, Vector2 pos, Vector2 direction, Player player) : base(tex, pos, direction)
        {
            this.player = player;

            Active = false;
            btnTexture = TextureBank.abilityButtonList[10];

            blockTimerTextPos = new Vector2(player.pos.X, player.pos.Y);

            width = 59;
            height = 61;

            rect.Width = width;
            rect.Height = height;

            first = false;

            coolDownTime = ValueBank.BlockCooldown;

            timePassed = (int)ValueBank.BlockTimer;
        }
        public override void Update(GameTime gameTime)
        {

            timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            blockTimerText = timePassed.ToString("0");

            Active = true;

            rect.X = (int)player.pos.X - width / 2;
            rect.Y = (int)player.pos.Y - height / 2;

            blockTimerTextPos.X = player.pos.X - 15;
            blockTimerTextPos.Y = player.pos.Y + 20;

            Vector2 temp = blockPos - player.pos;
            blockPos -= temp;

            if (HitCharacter != null)
            {
                if (!first)
                {
                    health = HitCharacter.HealthPoints;
                    first = true;
                }
            }

            if (HitCharacter != null)
            {                
                HitCharacter.HealthPoints = health;
            }

            if (timePassed <= 0)
            {
                Active = false;
                ToRemove = true;
                timePassed = (int)ValueBank.BlockTimer;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureBank.spriteFont, blockTimerText, blockTimerTextPos, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);
        }
    }
}
