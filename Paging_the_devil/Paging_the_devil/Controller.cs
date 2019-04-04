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
    //public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
    //public static MouseState mouseState, oldMouseState = Mouse.GetState();
    //public static bool KeyPressed(Keys key)
    //{
    //    return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
    //}

    public class Controller
    {
        PlayerIndex playerIndex;
       
        GamePadState gamePadState, oldPadState;
        

        public Controller(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;

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
