using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil
{

    public  class Controller
    {
           
        PlayerIndex playerIndex;
       
        GamePadState gamePadState;

        public Controller(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            
        }


        public void Update()
        {

            gamePadState = GamePad.GetState(playerIndex);

            

        }

       public Vector2 GetDirection()
        {
            return gamePadState.ThumbSticks.Left;
        }
       
    }
}
