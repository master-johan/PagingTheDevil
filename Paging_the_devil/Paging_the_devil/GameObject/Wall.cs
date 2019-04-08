using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Paging_the_devil.GameObject
{
    public class Wall : StationaryObjects
    {
        Rectangle sourceRect;
        Rectangle top;

        public Wall(Texture2D tex, Vector2 pos, Rectangle sourceRect) : base(tex, pos)
        {
            this.sourceRect = sourceRect;
            pos.X = sourceRect.X;
            pos.Y = sourceRect.Y;
            top = new Rectangle((int)pos.X, (int)pos.Y, sourceRect.Width, 2);

            rect = sourceRect;
        }
        public override void Update()
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, sourceRect, Color.White);
        }
        
    }
}
