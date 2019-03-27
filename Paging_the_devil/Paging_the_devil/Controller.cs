using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil
{
    public enum ControllerIndex { one, two, three, four}
    public  class Controller
    {
        Player player;
        ControllerIndex ctrlIdx;
        public GamePadCapabilities c1 = GamePad.GetCapabilities(PlayerIndex.One);
        public GamePadCapabilities c2 = GamePad.GetCapabilities(PlayerIndex.Two);
        public GamePadCapabilities c3 = GamePad.GetCapabilities(PlayerIndex.Three);
        public GamePadCapabilities c4 = GamePad.GetCapabilities(PlayerIndex.Four);
        GamePadState gamePadState;

        public Controller()
        {

            
        }

        public void UpdateCtrlOne()
        {

                ctrlIdx = ControllerIndex.one;
                GamePadState state = GamePad.GetState(PlayerIndex.One);



                if (c1.HasLeftXThumbStick)
                {
                    player.pos.X += state.ThumbSticks.Left.X * 10.0f;
                    player.pos.Y -= state.ThumbSticks.Left.Y * 10.0f;
                }

            
        }

        public void UpdateCtrlTwo()
        {
            if (c2.IsConnected)
            {
                ctrlIdx = ControllerIndex.two;
                GamePadState state = GamePad.GetState(PlayerIndex.One);



                if (c1.HasLeftXThumbStick)
                {
                    player.pos.X += state.ThumbSticks.Left.X * 10.0f;
                    player.pos.Y -= state.ThumbSticks.Left.Y * 10.0f;
                }

            }
        }
    }
}
