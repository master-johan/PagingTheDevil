using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject;

namespace Paging_the_devil
{
    public class Controller
    {
        PlayerIndex playerIndex;
        static DateTime StartVibrate;
        public bool Vibration { get; set; }
       
        public GamePadState gamePadState, oldPadState;

        public Controller(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            
        }

        public bool IsConnected()
        {
            gamePadState = GamePad.GetState(playerIndex);
            return gamePadState.IsConnected;
        }

        public void Update()
        {
            oldPadState = gamePadState;
            gamePadState = GamePad.GetState(playerIndex);
            GetVibration();
        }
        
        public bool ButtonPressed(Buttons button)
        {
            return gamePadState.IsButtonDown(button) && oldPadState.IsButtonUp(button);
        }

        public bool ButtonDown(Buttons button)
        {
            return gamePadState.IsButtonDown(button);
        }
        
        public Vector2 GetDirection()
        {
            return gamePadState.ThumbSticks.Left;
        }

        public GamePadState GetPadState()
        {
            return gamePadState;
        }

        public GamePadState GetOldPadState()
        {
            return oldPadState;
        }

        public void GetVibration()
        {
            if (Vibration)
            {
                GamePad.SetVibration(playerIndex, 1f, 1f);
                StartVibrate = DateTime.Now;
            }
            TimeSpan timePassed = DateTime.Now - StartVibrate;

            if (timePassed.TotalSeconds >= 0.2)
            {
                GamePad.SetVibration(playerIndex, 0f, 0f);
            }
            
        }
    }
}
