using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil.Manager
{
    class PlayerSelectManager
    {

        Controller[] controllerArray;
        Player[] playerArray;

        Rectangle[] playerRectArray;

        int nrOfPlayers;

        bool[] playerConnected;
        bool[] characterSelected;
        bool[] connectedController;

        public PlayerSelectManager()
        {
            playerRectArray = new Rectangle[4];

            characterSelected = new bool[4];
            connectedController = new bool[4];
            playerConnected = new bool[4];

            playerArray = new Player[4];

            DecidingRectangles();
        }

        public void Update()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].ButtonPressed(Buttons.Y))
                {
                    characterSelected[i] = true;
                    connectedController[i] = false;
                    controllerArray[i] = new Controller((PlayerIndex) i);
                    playerArray[i] = new Player(TextureManager.playerTextureList[0], new Vector2(100 * i + 50, 100), new Rectangle(0, 0, 10, 10), i, controllerArray[i]);

                }
                else if (controllerArray[i].IsConnected())
                {
                    connectedController[i] = true;
                }

            }


            if (controllerArray[0] != null)
            {
                if (controllerArray[0].ButtonPressed(Buttons.Start))
                {
                    GameManager.currentState = GameState.InGame;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.DarkGray);
            spriteBatch.Draw(TextureManager.menuTextureList[3], Vector2.Zero, Color.White);

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (characterSelected[i])
                {
                    spriteBatch.Draw(TextureManager.playerTextureList[0], playerRectArray[i], Color.White);
                }
                else if (connectedController[i])
                {
                    spriteBatch.Draw(TextureManager.menuTextureList[7], playerRectArray[i], Color.White);
                }
            }
        }

        private void DecidingRectangles()
        {
            playerRectArray[0] = new Rectangle(50, 200, 200, 100);
            playerRectArray[1] = new Rectangle(350, 200, 200, 100);
            playerRectArray[2] = new Rectangle(650, 200, 200, 100);
            playerRectArray[3] = new Rectangle(950, 200, 200, 100);

        }

        public void GetController(Controller[] controllerArray)
        {
            this.controllerArray = controllerArray;
        }

        public void GetNrOfPlayers(int nrOfPlayers)
        {
            this.nrOfPlayers = nrOfPlayers;
        }

        public Player[] GetPlayerArray()
        {
            return playerArray;
        }
    }
}
