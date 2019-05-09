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

        int width;
        int height;

        public Block(Texture2D tex, Vector2 pos, Vector2 direction, Player player) : base(tex, pos, direction)
        {
            this.player = player;

            Active = false;
            btnTexture = TextureBank.hudTextureList[7];

            width = 59;
            height = 61;

            rect.Width = width;
            rect.Height = height;

            first = false;

            coolDownTime = ValueBank.BlockCooldown;
        }
        public override void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Active = true;

            rect.X = (int)player.pos.X - width / 2;
            rect.Y = (int)player.pos.Y - height / 2;

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

        }
    }
}
