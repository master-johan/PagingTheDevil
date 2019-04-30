using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;


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
            GenerateRectangles(pos);
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, sourceRect, Color.White);
            //spriteBatch.Draw(tex, hitboxBot, Color.Black);
            //spriteBatch.Draw(tex, hitboxTop, Color.White);
            //spriteBatch.Draw(tex, hitboxLeft, Color.Red);
            //spriteBatch.Draw(tex, hitboxRight, Color.Blue);
           
        }

        private void GenerateRectangles(Vector2 pos)
        {
            //hitboxLeft = new Rectangle((int)pos.X, (int)pos.Y,5,GetRect.Height);
            //hitboxTop = new Rectangle((int)pos.X, (int)pos.Y , GetRect.Width, 5);
            //hitboxBot = new Rectangle((int)pos.X, (int)pos.Y + GetRect.Height - 5,GetRect.Width, 5);
            //hitboxRight = new Rectangle((int)pos.X + GetRect.Width -5, (int)pos.Y, 5, GetRect.Height);
        }

    }
}
