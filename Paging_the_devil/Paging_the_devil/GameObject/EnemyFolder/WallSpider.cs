using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;
using Paging_the_devil.GameObject.Characters;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.GameObject.EnemyFolder
{
    class WallSpider : Enemy
    {
        Player[] playerArray;

        Ability fireball;

        Rectangle srcRect;

        bool left;
        bool right;
        bool up;
        bool down;

        int nrOfPlayers;
        int maxX;
        int maxY;
        int minX;
        int minY;
        int spriteWidth;
        int spriteHeight;
        int frame;
        int spriteCount;

        double timer;
        double interval;

        Vector2 origin;

        float scale;
        float rotation;
        public WallSpider(Texture2D tex, Vector2 pos, Player[] playerArray, int nrOfPlayers) : base(tex, pos)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;

            spriteWidth = 65;
            spriteHeight = 42;

            srcRect = new Rectangle(0, 0, 50, 32);
            rect = new Rectangle((int)pos.X, (int)pos.Y, spriteWidth, spriteHeight);

            HealthPoints = ValueBank.WallSpiderHealth;

            spriteCount = 6;
            interval = 200;

            scale = 2f;
            origin = new Vector2(TextureBank.enemyTextureList[2].Width / 2, 16);

            maxX = 1875;
            maxY = 1030;
            minX = 35;
            minY = 175;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Animation(gameTime);

            rect.X = (int)pos.X - 25;
            rect.Y = (int)pos.Y - 25;

            ShootingWeb();
            ChoosingDirection();
            Movement(gameTime);
            if (down)
            {
                rotation = MathHelper.ToRadians(90);
            }
            if (up)
            {
                rotation = MathHelper.ToRadians(270);
            }
            if (left)
            {
                rotation = MathHelper.ToRadians(180);
            }
            if (right)
            {
                rotation = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(TextureBank.hudTextureList[0], rect, Color.Black);
            spriteBatch.Draw(tex, pos, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 1f);

            base.Draw(spriteBatch);
        }
        protected override void Movement(GameTime gameTime)
        {
            if (pos.X >= maxX && pos.Y <= minY) // Nedåt
            {
                down = true;
                up = false;
                right = false;
                left = false;
                UpdateHitbox();
            }

            if (pos.X <= minX && pos.Y >= maxY) //Uppåt
            {
                up = true;
                down = false;
                right = false;
                left = false;
                UpdateHitbox();
            }

            if (pos.X <= minX && pos.Y <= minY) //Höger
            {
                right = true;
                left = false;
                up = false;
                down = false;
                UpdateHitbox();
            }

            if (pos.X >= maxX && pos.Y >= maxY) //Vänster
            {
                left = true;
                right = false;
                up = false;
                down = false;
                UpdateHitbox();
            }

        }
        private void ChoosingDirection()
        {
            if (up)
            {
                pos.Y -= ValueBank.SpiderMoveSpeed; //Uppåt
            }
            if (down)
            {
                pos.Y += ValueBank.SpiderMoveSpeed; //Nedåt
            }
            if (left)
            {
                pos.X -= ValueBank.SpiderMoveSpeed; //Vänster
            }
            if (right)
            {
                pos.X += ValueBank.SpiderMoveSpeed; //Höger
            }
        }
        private void UpdateHitbox()
        {

            if (down || up)
            {
                rect.Width = 52;
                rect.Height = 65;
            }
            else if (left || right)
            {
                rect.Width = 70;
                rect.Height = 42;
            }
        }
        private void ShootingWeb()
        {
            if (shootTimer == 0)
            {
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    Vector2 tempVector = pos;
                    targetPlayer = playerArray[i];

                    Vector2 dir = targetPlayer.GetSetPos - tempVector;
                    dir.Normalize();

                    dir.Y = -dir.Y;

                    ValueBank.FireballSpeed = ValueBank.WebballSpeed;

                    fireball = new Fireball(TextureBank.mageSpellList[7], tempVector, dir);
                    enemyAbilityList.Add(fireball);
                   

                    shootTimer = (int)ValueBank.WebbballTimer;
                }
            }
        }
        private void Animation(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer <= 0)
            {
                timer = interval;
                frame++;
                srcRect.Y = 32 * (frame % spriteCount);
            }
        }
    }
}
