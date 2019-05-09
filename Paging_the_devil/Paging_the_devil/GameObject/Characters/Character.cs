using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil.GameObject.Characters
{
    public class Character : GameObject
    {
        public float HealthPoints { get; set; }
        public float HitTimer { get; set; }

        public bool Hit { get; set; }

        public Character(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
