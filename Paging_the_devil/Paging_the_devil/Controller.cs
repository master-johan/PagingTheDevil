using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

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
        public void Update()
        {
            oldPadState = gamePadState;
            gamePadState = GamePad.GetState(playerIndex);
            GetVibration();
        }
        /// <summary>
        /// Den här metoden kollar ifall en kontroll är connectad
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            gamePadState = GamePad.GetState(playerIndex);
            return gamePadState.IsConnected;
        }
        /// <summary>
        /// Den här metoden returnerar ifall en knapp tryckt på
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonPressed(Buttons button)
        {
            return gamePadState.IsButtonDown(button) && oldPadState.IsButtonUp(button);
        }
        /// <summary>
        /// Den här metoden kollar ifall ifall en knapp är nedtryckt
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonDown(Buttons button)
        {
            return gamePadState.IsButtonDown(button);
        }
        /// <summary>
        /// Den här metoden returnerar direction av vänstra joysticken
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDirection()
        {
            return gamePadState.ThumbSticks.Left;
        }
        /// <summary>
        /// Den här metoden returnerar padstate
        /// </summary>
        /// <returns></returns>
        public GamePadState GetPadState()
        {
            return gamePadState;
        }
        /// <summary>
        /// Den här metoden returnerar den gamla padstate
        /// </summary>
        /// <returns></returns>
        public GamePadState GetOldPadState()
        {
            return oldPadState;
        }
        /// <summary>
        /// Den här metoden sköter vibration
        /// </summary>
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
