using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Abilities
{
    class Root : Ability
    {
        public List<Enemy> enemyList;

        float timePassed;
        Color rootColor;
        
        public bool Active { get; private set; }

        public Root(Texture2D tex, Vector2 pos, Vector2 direction) : base(tex, pos, direction)
        {
            Active = false;

            enemyList = new List<Enemy>();

            rect = new Rectangle((int)pos.X - tex.Width / 2, (int)pos.Y - tex.Height / 2, 400, 400);

            rootColor = new Color(255, 255, 255, 255);
            btnTexture = TextureBank.abilityButtonList[9];
            coolDownTime = 600;
        }

        public override void Update(GameTime gameTime)
        {
            rect.X = (int)pos.X - tex.Width / 2;
            rect.Y = (int)pos.Y - tex.Height / 2;

            if (HitCharacter != null)
            {
                bool hasHitBefore = false;

                foreach (var e in enemyList)
                {
                    if (HitCharacter == e)
                    {
                        hasHitBefore = true;
                    }
                }
                if (!hasHitBefore)
                {
                    enemyList.Add(HitCharacter as Enemy);
                    ApplyDamage();
                }
            }

            Active = true;
            WebRoot(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, tex.Width, tex.Height), rootColor, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0.1f);
        }

        private void WebRoot(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Active)
            {
                foreach (var e in enemyList)
                {
                    e.MovementSpeed = 0;
                }
                rootColor.R--;
                rootColor.G--;
                rootColor.B--;
                rootColor.A--;
            }

            if (timePassed >= ValueBank.RootTimer)
            {

                foreach (var e in enemyList)
                {
                    if (e is Slime)
                    {
                        (e as Slime).MovementSpeed = ValueBank.SlimeSpeed;
                    }
                    else if (e is Devil)
                    {
                        (e as Devil).MovementSpeed = ValueBank.DevilSpeed;
                    }
                    else if (e is SmallDevil)
                    {
                        (e as SmallDevil).MovementSpeed = ValueBank.SmallDevilMoveSpeed;
                    }
                    else if (e is WallSpider)
                    {
                        (e as WallSpider).MovementSpeed = ValueBank.SpiderMoveSpeed;
                    }

                }

                ToRemove = true;
                Active = false;
                timePassed = 0;
            }
        }
    }
}
