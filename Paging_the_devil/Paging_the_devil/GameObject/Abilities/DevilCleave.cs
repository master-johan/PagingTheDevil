using System.Collections.Generic;
using Paging_the_devil.GameObject.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Abilities
{
    class DevilCleave : Ability
    {
        public List<Player> playerList;
        Vector2 cleavePos;
        Texture2D tex;
        Rectangle sourceRect;
        float angle;

        float timePassed;

        Character character;

        public bool Active { get; private set; }
        bool hit;

        public DevilCleave(Texture2D tex, Vector2 pos, Vector2 direction, Character character) : base(tex, pos, direction)
        {
            this.character = character;
            this.tex = tex;

            Active = false;
            hit = false;
            timePassed = 0;

            playerList = new List<Player>();
            rect = new Rectangle((int)pos.X - tex.Width, (int)pos.Y - tex.Width, 300, 300);
            sourceRect = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)character.pos.X - tex.Width;
            rect.Y = (int)character.pos.Y - tex.Width;

            Vector2 temp = cleavePos - character.pos;
            cleavePos -= temp;

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
                CleaveDamage(gameTime);
            }
            UpdatingAngle();
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(tex, cleavePos, sourceRect, Color.White, angle, new Vector2(-40, tex.Height / 2), 1, SpriteEffects.None, 0.1f);
                //spriteBatch.Draw(tex, rect, Color.Black);
            }     
        }

        public void CleaveDamage(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Active == true)
            {
                foreach (var p in playerList)
                {
                    p.HealthPoints -= 0.5f;
                }
                playerList.Clear();
            }

            if (timePassed >= ValueBank.DevilCleaveTimer)
            {
                Active = false;
                ToRemove = true;
            }
        }

        public void UpdatingAngle()
        {
            if (Active)
            {
                angle -= 0.15f;
            }
            
        }



    }
}
