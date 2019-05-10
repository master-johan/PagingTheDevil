using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil.GameObject
{
    class Gateway : StationaryObjects
    {
        public bool IsVisible { get; set; }

        public Gateway(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
        public override void Update(GameTime gameTime)
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            pos = new Vector2(pos.X, pos.Y);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.White, 0, Vector2.Zero, 1,SpriteEffects.None, 0.3f);
        }
    }
}
