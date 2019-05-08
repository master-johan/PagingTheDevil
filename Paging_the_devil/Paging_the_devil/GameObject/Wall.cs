using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil.GameObject
{
    public class Wall : StationaryObjects
    {
        Rectangle sourceRect;
        
        Rectangle hitboxLeft;
        Rectangle hitboxTop;
        Rectangle hitboxBot;
        Rectangle hitboxRight;

        public Rectangle HitboxLeft { get => hitboxLeft; set => hitboxLeft = value; }
        public Rectangle HitboxTop { get => hitboxTop; set => hitboxTop = value; }
        public Rectangle HitboxBot { get => hitboxBot; set => hitboxBot = value; }
        public Rectangle HitboxRight { get => hitboxRight; set => hitboxRight = value; }

        public Wall(Texture2D tex, Vector2 pos, Rectangle sourceRect) : base(tex, pos)
        {
            this.sourceRect = sourceRect;
            pos.X = sourceRect.X;
            pos.Y = sourceRect.Y;
            rect = sourceRect;
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, sourceRect, Color.White);
        }
    }
}
