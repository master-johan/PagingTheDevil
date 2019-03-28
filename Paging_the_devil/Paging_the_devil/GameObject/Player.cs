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
        Vector2 direction;
        int playerIndex;
   

       

        public Player(Texture2D tex, Vector2 pos, Rectangle rect, int playerIndex) 
            : base(tex, pos, rect)
        {
            this.playerIndex = playerIndex;
            //controller = new Controller();
        }

        public override void Update()
        {

            pos.X += direction.X * 10.0f;
            pos.Y -= direction.Y * 10.0f;


               
        




        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(c1.IsConnected)
            spriteBatch.Draw(tex, pos, new Rectangle(0, 0, 60, 70), Color.White, 0, new Vector2(30, 35), 1, SpriteEffects.None, 1);
        }

        public void InputDirection(Vector2 newDirection)
        {
            direction = newDirection;
        }
    }
}
