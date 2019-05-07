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

        bool first;

        Player player;

        Vector2 blockPos;

        public bool Active { get; private set; }

        public Block(Texture2D tex, Vector2 pos, Vector2 direction, Player player) : base(tex, pos, direction)
        {
            this.player = player;

            Active = false;
            btnTexture = TextureBank.hudTextureList[7];

            rect.Width = tex.Width;
            rect.Height = tex.Height;

            first = false;
        }
        public override void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Active = true;

            rect.X = (int)player.pos.X - tex.Width / 2;
            rect.Y = (int)player.pos.Y - tex.Height / 2;

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

            if (timePassed >= ValueBank.BlockTimer)
            {
                Active = false;
                ToRemove = true;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, blockPos, new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0, new Vector2(tex.Width/2, tex.Height/2), 1, SpriteEffects.None, 1);
        }
    }
}
