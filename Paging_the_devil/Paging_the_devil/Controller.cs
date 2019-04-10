﻿using Microsoft.Xna.Framework;
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
    }
}
