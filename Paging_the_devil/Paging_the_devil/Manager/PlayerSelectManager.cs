using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.Manager
{
    class PlayerSelectManager
    {
        Controller[] controllerArray;

        Rectangle[] playerRectArray;
        Rectangle[] pressYRectArray;
        Rectangle[] playerArrowArray;
        Rectangle[] characterInfoArray;

        Rectangle drawKnightRect;
        Rectangle startGameRect;

        int nrOfPlayers;
        int readyPlayers;

        int[] currentCharacter;

        bool[] playerConnected;
        bool[] selectingCharacter;
        bool[] connectedController;
        bool[] characterChosen;

        Texture2D barbarianTex;
        Texture2D knightTex;
        Texture2D druidTex;
        Texture2D rangerTex;

        PlayerSelectBackground playerSelectBackground;

        public Player[] PlayerArray { get; set; }
        public HUDManager HUDManager { get; set; }

        public PlayerSelectManager()
        {
            currentCharacter = new int[4];

            for (int i = 0; i < currentCharacter.Length; i++)
            {
                currentCharacter[i] = 0;
            }

            playerRectArray = new Rectangle[4];
            pressYRectArray = new Rectangle[4];
            playerArrowArray = new Rectangle[4];
            characterInfoArray = new Rectangle[4];

            selectingCharacter = new bool[4];
            connectedController = new bool[4];
            playerConnected = new bool[4];
            characterChosen = new bool[4];

            PlayerArray = new Player[4];

            drawKnightRect = new Rectangle(0, 0, 50, 60);
            startGameRect = new Rectangle(ValueBank.WindowSizeX / 2 - TextureBank.menuTextureList[11].Width /2, ValueBank.WindowSizeY / 3, TextureBank.menuTextureList[11].Width, TextureBank.menuTextureList[11].Height);

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
                    selectingCharacter[i] = true;
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
                if (selectingCharacter[i] && !characterChosen[i])
                {
                    if (controllerArray[i].ButtonPressed(Buttons.DPadLeft))
                    {
                        if (currentCharacter[i] == 0)
                        {
                            currentCharacter[i] = 3;
                        }

                        else
                        {
                            currentCharacter[i]--;
                        }
                    }
                    else if (controllerArray[i].ButtonPressed(Buttons.DPadRight))
                    {
                        if (currentCharacter[i] == 3)
                        {
                            currentCharacter[i] = 0;
                        }

                        else
                        {
                            currentCharacter[i]++;
                        }
                    }
                }
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].ButtonPressed(Buttons.A) && selectingCharacter[i] && !characterChosen[i])
                {
                    if (currentCharacter[i] == 0)
                    {
                        PlayerArray[i] = new Knight(knightTex, new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    else if (currentCharacter[i] == 1)
                    {
                        PlayerArray[i] = new Barbarian(barbarianTex, new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    else if (currentCharacter[i] == 2)
                    {
                        PlayerArray[i] = new Druid(druidTex, new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    else if (currentCharacter[i] == 3)
                    {
                        PlayerArray[i] = new Ranger(rangerTex, new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    characterChosen[i] = true;
                    readyPlayers++;
                }         
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (readyPlayers == nrOfPlayers)
                {
                    if (controllerArray[i].ButtonPressed(Buttons.Start))
                    {
                        HUDManager = new HUDManager(PlayerArray, nrOfPlayers);
                        MediaPlayer.Play(SoundBank.BgMusicList[2]);
                        GameManager.currentState = GameState.InGame;
                    }
                }
            }


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Draw(TextureBank.menuTextureList[3], Vector2.Zero, Color.White);
            playerSelectBackground.Draw(spriteBatch);

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (selectingCharacter[i])
                {
                    if(!characterChosen[i])
                    {
                        spriteBatch.Draw(TextureBank.menuTextureList[9], playerArrowArray[i], Color.White);
                    }

                    else
                    {
                        spriteBatch.Draw(TextureBank.menuTextureList[10], pressYRectArray[i], Color.White);
                    }

                    if (currentCharacter[i] == 0)
                    {
                        spriteBatch.Draw(knightTex, playerRectArray[i], drawKnightRect, Color.White);
                        spriteBatch.Draw(TextureBank.menuTextureList[12], characterInfoArray[i], Color.White);
                    }

                    else if (currentCharacter[i] == 1)
                    {
                        spriteBatch.Draw(barbarianTex, playerRectArray[i], drawKnightRect, Color.White);
                        spriteBatch.Draw(TextureBank.menuTextureList[14], characterInfoArray[i], Color.White);
                    }

                    else if (currentCharacter[i] == 2)
                    {
                        spriteBatch.Draw(druidTex, playerRectArray[i], drawKnightRect, Color.White);
                        spriteBatch.Draw(TextureBank.menuTextureList[15], characterInfoArray[i], Color.White);
                    }

                    else if (currentCharacter[i] == 3)
                    {
                        spriteBatch.Draw(rangerTex, playerRectArray[i], drawKnightRect, Color.White);
                        spriteBatch.Draw(TextureBank.menuTextureList[13], characterInfoArray[i], Color.White);
                    }
                }
                else if (connectedController[i])
                {
                    spriteBatch.Draw(TextureBank.menuTextureList[7], pressYRectArray[i], Color.White);
                }
            }
            if (nrOfPlayers == readyPlayers && nrOfPlayers > 0)
            {
                spriteBatch.Draw(TextureBank.menuTextureList[11], startGameRect, Color.White);
            }
        }
        /// <summary>
        /// Den här metoden sätter värderna för rektanglarna
        /// </summary>
        private void DecidingRectangles()
        {
            playerRectArray[0] = new Rectangle(210, 726, 100, 110);
            playerRectArray[1] = new Rectangle(565, 775, 100, 110);
            playerRectArray[2] = new Rectangle(1268, 775, 100, 110);
            playerRectArray[3] = new Rectangle(1617, 726, 100, 110);

            playerArrowArray[0] = new Rectangle(80, 550, 350, 300); 
            playerArrowArray[1] = new Rectangle(435, 600, 350, 300);
            playerArrowArray[2] = new Rectangle(1138, 600, 350, 300);
            playerArrowArray[3] = new Rectangle(1487, 550, 350, 300);

            pressYRectArray[0] = new Rectangle(80, 550, 350, 140);
            pressYRectArray[1] = new Rectangle(435, 600, 350, 140);
            pressYRectArray[2] = new Rectangle(1138, 600, 350, 140);
            pressYRectArray[3] = new Rectangle(1487, 550, 350, 140);

            characterInfoArray[0] = new Rectangle(180, 855, 161, 170);
            characterInfoArray[1] = new Rectangle(535, 905, 161, 170);
            characterInfoArray[2] = new Rectangle(1238, 905, 161, 170);
            characterInfoArray[3] = new Rectangle(1587, 855, 161, 170);
        }
        /// <summary>
        /// Den här metoden sätter värdena för textur-arrayerna
        /// </summary>
        private void DecidingTextureArray()
        {
            knightTex = TextureBank.playerTextureList[0];
            barbarianTex = TextureBank.playerTextureList[1];
            druidTex = TextureBank.playerTextureList[2];
            rangerTex = TextureBank.playerTextureList[3];
        }
        /// <summary>
        /// Den här metoden hämtar controller array.
        /// </summary>
        /// <param name="controllerArray"></param>
        public void GetController(Controller[] controllerArray)
        {
            this.controllerArray = controllerArray;
        }
        /// <summary>
        /// Den här metoden hämtar nrOfPlayers.
        /// </summary>
        /// <param name="nrOfPlayers"></param>
        public void GetNrOfPlayers(int nrOfPlayers)
        {
            this.nrOfPlayers = nrOfPlayers;
        }
    }
}
