using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil
{
    class Player : Character
    {
        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, 59, 61);
        }

        public override void Update()
        {
            rect.X = (int)pos.X -30;
            rect.Y = (int)pos.Y- 30;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                pos.Y= pos.Y-5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                pos.X= pos.X +5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                pos.Y = pos.Y + 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                pos.X = pos.X - 5;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 60, 70), Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);
            spriteBatch.Draw(tex, rect, Color.Black);
        }
    }
}
