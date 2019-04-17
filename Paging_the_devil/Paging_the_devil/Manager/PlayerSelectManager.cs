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

        HUDManager HUDManager;

        Controller[] controllerArray;
        Player[] playerArray;

        Rectangle[] playerRectArray;
        Rectangle[] pressYRectArray;
        Rectangle drawKnightRect;

        int nrOfPlayers;
        int[] characterChosen;

        bool[] playerConnected;
        bool[] characterSelected;
        bool[] connectedController;

        Texture2D barbarianTex;
        Texture2D knightTex;

        PlayerSelectBackground playerSelectBackground;

        public PlayerSelectManager()
        {
            characterChosen = new int[4];

            for (int i = 0; i < characterChosen.Length; i++)
            {
                characterChosen[i] = 0;
            }

            playerRectArray = new Rectangle[4];
            pressYRectArray = new Rectangle[4];

            characterSelected = new bool[4];
            connectedController = new bool[4];
            playerConnected = new bool[4];

            playerArray = new Player[4];

            drawKnightRect = new Rectangle(0, 0, 60, 70);

            DecidingRectangles();
            DecidingTextureArray();

            playerSelectBackground = new PlayerSelectBackground();
        }

        public void Update(GameTime gameTime)
        {
            playerSelectBackground.Update(gameTime);

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].ButtonPressed(Buttons.Y))
                {
                    characterSelected[i] = true;
                    connectedController[i] = false;
                    controllerArray[i] = new Controller((PlayerIndex)i);
                }
                else if (controllerArray[i].IsConnected())
                {
                    connectedController[i] = true;
                }
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (characterSelected[i])
                {
                    if (controllerArray[i].ButtonPressed(Buttons.DPadLeft))
                    {
                        if (characterChosen[i] == 0)
                        {
                            characterChosen[i] = 3;
                        }
                        else
                        {
                            characterChosen[i]--;
                        }
                    }
                    else if (controllerArray[i].ButtonPressed(Buttons.DPadRight))
                    {
                        if (characterChosen[i] == 3)
                        {
                            characterChosen[i] = 0;
                        }
                        else
                        {
                            characterChosen[i]++;
                        }
                    }
                }
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].ButtonPressed(Buttons.A))
                {
                    if (characterChosen[i] == 0)
                    {
                        playerArray[i] = new Knight(knightTex, new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }
                    else if (characterChosen[i] == 1)
                    {
                        playerArray[i] = new Barbarian(barbarianTex, new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }
                }
            }

            if (controllerArray[0] != null)
            {
                if (controllerArray[0].ButtonPressed(Buttons.Start))
                {

                    HUDManager = new HUDManager(playerArray,nrOfPlayers);
                    
                    GameManager.currentState = GameState.InGame;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.DarkGray);
            
            spriteBatch.Draw(TextureManager.menuTextureList[3], Vector2.Zero, Color.White);
            playerSelectBackground.Draw(spriteBatch);

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (characterSelected[i])
                {
                    if (characterChosen[i] == 0)
                    {
                        spriteBatch.Draw(knightTex, playerRectArray[i], drawKnightRect, Color.White);
                    }
                    else if (characterChosen[i] == 1)
                    {
                        spriteBatch.Draw(barbarianTex, playerRectArray[i], drawKnightRect, Color.White);
                    }
                    
                }
                else if (connectedController[i])
                {
                    spriteBatch.Draw(TextureManager.menuTextureList[7], pressYRectArray[i], Color.White);
                }
            }
        }

        private void DecidingRectangles()
        {
            playerRectArray[0] = new Rectangle(210, 726, 100, 110);
            playerRectArray[1] = new Rectangle(565, 775, 100, 110);
            playerRectArray[2] = new Rectangle(1268, 775, 100, 110);
            playerRectArray[3] = new Rectangle(1617, 726, 100, 110);

            pressYRectArray[0] = new Rectangle(160, 686, 200, 150);
            pressYRectArray[1] = new Rectangle(515, 735, 200, 150);
            pressYRectArray[2] = new Rectangle(1218, 735, 200, 150);
            pressYRectArray[3] = new Rectangle(1577, 686, 200, 150);
        }

        private void DecidingTextureArray()
        {
            knightTex = TextureManager.playerTextureList[0];
            barbarianTex = TextureManager.playerTextureList[1];
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
        public HUDManager GetHudManager()
        {
            return HUDManager;
        }
    }
}
