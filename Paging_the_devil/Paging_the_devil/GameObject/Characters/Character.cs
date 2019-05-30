using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Characters
{
    public class Character : GameObject
    {
        public float HealthPoints { get; set; }
        protected float MaxHealthPoints { get; set; }
        public float HitTimer { get; set; }
        protected Rectangle HealthBarRectangle;
        protected Rectangle HealthBarBackgroundRect;
        protected int healthbarWidth;
        protected int healthbarWidthMax;
        protected int healthbarYOffset;
        protected int healthbarXOffset;

        public bool Hit { get; set; }
        protected bool ShowHealthbar { get; set; }

        public Character(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            healthbarYOffset = -5;
            healthbarWidth = tex.Width;
            healthbarWidthMax = healthbarWidth;
            HealthBarRectangle = new Rectangle((int)GetSetPos.X + healthbarXOffset, (int)GetSetPos.Y + healthbarYOffset, healthbarWidth, 15);
            HealthBarBackgroundRect = HealthBarRectangle;

            Hit = false;
            HitTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (Hit)
            {
                HitTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (HitTimer >= ValueBank.HitTimerMax)
            {
                Hit = false;
                HitTimer = 0;
            }

            if (HealthPoints <= 0.5 * MaxHealthPoints)
            {
                ShowHealthbar = true; ;
            }
            else
            {
                ShowHealthbar = false;
            }

            UpdateHealthBar();




        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        protected void UpdateHealthBar()
        {
            HealthBarRectangle.X = (int)GetSetPos.X + healthbarXOffset;
            HealthBarRectangle.Y = (int)GetSetPos.Y + healthbarYOffset;

            HealthBarBackgroundRect.X = (int)GetSetPos.X + healthbarXOffset;
            HealthBarBackgroundRect.Y = (int)GetSetPos.Y + healthbarYOffset;

            float healthbarWidthProcent = HealthPoints / MaxHealthPoints;

            healthbarWidth = (int)(healthbarWidthProcent * healthbarWidthMax);

            HealthBarRectangle.Width = healthbarWidth;
        }

        protected void DrawHealthBar(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(TextureBank.hudTextureList[18], HealthBarRectangle, Color.White);
            spriteBatch.Draw(TextureBank.hudTextureList[18], null, HealthBarBackgroundRect, null, null, 0, Vector2.One, Color.Black, SpriteEffects.None, 0.9f);

            spriteBatch.Draw(TextureBank.hudTextureList[18], null, HealthBarRectangle, null, null, 0, Vector2.One, Color.White, SpriteEffects.None, 1);
        }
    }
}
