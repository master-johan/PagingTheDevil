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
        Controller controller;

        public Player(Texture2D tex, Vector2 pos, Rectangle rect, Controller controller) : base(tex, pos, rect)
        {
            this.controller = controller;

        }

        public override void Update()
        {

            if (controller.HasLeftXThumbStick)
            {
                Pos.X += state.ThumbSticks.Left.X * 10.0f;
                Pos.Y += state.ThumbSticks.Left.Y * 10.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                pos.Y--;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                pos.X++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                pos.Y++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                pos.X--;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 60, 70), Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);
        }
    }
}
