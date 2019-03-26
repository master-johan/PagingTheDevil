using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil
{
    public class Controller
    {
       
        GamePadCapabilities c = GamePad.GetCapabilities(PlayerIndex.One);
        GamePadState gamePadState;

        public Controller(GamePadCapabilities c, GamePadState gamePadState)
        {
            this.c = c;
            this.gamePadState = gamePadState;
            
        }

        public void Update(GameTime gameTime)
        {

            
            if (c.IsConnected)
            {
                gamePadState = GamePad.GetState(PlayerIndex.One);


                if (c.GamePadType == GamePadType.GamePad)
                {

                }
                


            }
        }
    }
}
