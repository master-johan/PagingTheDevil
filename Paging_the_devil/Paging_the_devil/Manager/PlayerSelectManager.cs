using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        Rectangle drawCharRect;
        Rectangle startGameRect;

        Vector2 goBackTextPos;

        int readyPlayers;
        int middleScreenX;

        int[] currentCharacter;

        bool[] playerConnected;
        bool[] selectingCharacter;
        bool[] connectedController;
        bool[] characterChosen;

        bool justPressed;

        PlayerSelectBackground playerSelectBackground;

        public int nrOfPlayers { get; set; }

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

            goBackTextPos = new Vector2(15, 15);

            middleScreenX = (ValueBank.WindowSizeX / 2);

            drawCharRect = new Rectangle(0, 0, 50, 60);
            startGameRect = new Rectangle(ValueBank.WindowSizeX / 2 - TextureBank.menuTextureList[11].Width / 2, ValueBank.WindowSizeY / 3, TextureBank.menuTextureList[11].Width, TextureBank.menuTextureList[11].Height);

            DecidingRectangles();

            playerSelectBackground = new PlayerSelectBackground();
        }

        public void Update(GameTime gameTime)
        {
            playerSelectBackground.Update(gameTime);

            ConnectingToPlayerSelect();
            BrowsingCharacter();
            SelectingCharacter();
            StartGameWhenCharactersAreChosen();

            justPressed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.DarkGray);

            playerSelectBackground.Draw(spriteBatch);
            spriteBatch.Draw(TextureBank.menuTextureList[8], new Vector2(middleScreenX - TextureBank.menuTextureList[8].Width / 2, 100), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.6f);
            spriteBatch.Draw(TextureBank.menuTextureList[24], goBackTextPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (selectingCharacter[i])
                {
                    if (!characterChosen[i])
                    {
                        spriteBatch.Draw(TextureBank.menuTextureList[9], playerArrowArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                    }

                    else
                    {
                        spriteBatch.Draw(TextureBank.menuTextureList[10], pressYRectArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                    }

                    if (currentCharacter[i] == 0)
                    {
                        spriteBatch.Draw(TextureBank.playerTextureList[0], playerRectArray[i], drawCharRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                        spriteBatch.Draw(TextureBank.menuTextureList[12], characterInfoArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    }

                    else if (currentCharacter[i] == 1)
                    {
                        spriteBatch.Draw(TextureBank.playerTextureList[1], playerRectArray[i], drawCharRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                        spriteBatch.Draw(TextureBank.menuTextureList[14], characterInfoArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    }

                    else if (currentCharacter[i] == 2)
                    {
                        spriteBatch.Draw(TextureBank.playerTextureList[2], playerRectArray[i], drawCharRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                        spriteBatch.Draw(TextureBank.menuTextureList[15], characterInfoArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    }

                    else if (currentCharacter[i] == 3)
                    {
                        spriteBatch.Draw(TextureBank.playerTextureList[3], playerRectArray[i], drawCharRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                        spriteBatch.Draw(TextureBank.menuTextureList[13], characterInfoArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    }
                }
                else if (connectedController[i])
                {
                    spriteBatch.Draw(TextureBank.menuTextureList[7], pressYRectArray[i], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
                }
            }
            if (nrOfPlayers == readyPlayers && nrOfPlayers > 0)
            {
                spriteBatch.Draw(TextureBank.menuTextureList[11], startGameRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.6f);
            }
        }

        /// <summary>
        /// När en person trycker Y i playerselectmenyn så kan personen börja välja karaktärer.
        /// </summary>
        private void ConnectingToPlayerSelect()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].ButtonPressed(Buttons.Y) && !characterChosen[i])
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
        }

        /// <summary>
        /// När alla spelare har valt karaktärer så kan man trycka start för att starta spelet.
        /// </summary>
        private void StartGameWhenCharactersAreChosen()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (readyPlayers == nrOfPlayers)
                {
                    if (controllerArray[i].ButtonPressed(Buttons.Start))
                    {

                        HUDManager = new HUDManager(PlayerArray, nrOfPlayers);
                        GameManager.currentState = GameState.StoryScreen;
                        MediaPlayer.Play(SoundBank.BgMusicList[3]);


                    }
                }
            }
        }
        /// <summary>
        /// När man trycker A på Xbox-kontrollen så väljer man den nuvarande gubben man bläddrat till, trycker man B så ångrar man sitt val.
        /// Om den användaren som har kontroll nummer 1 trycker på B så går spelet tillbaka till huvudmenyn.
        /// </summary>
        private void SelectingCharacter()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].ButtonPressed(Buttons.A) && selectingCharacter[i] && !characterChosen[i])
                {
                    if (currentCharacter[i] == 0)
                    {
                        PlayerArray[i] = new Knight(TextureBank.playerTextureList[0], new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    else if (currentCharacter[i] == 1)
                    {
                        PlayerArray[i] = new Barbarian(TextureBank.playerTextureList[1], new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    else if (currentCharacter[i] == 2)
                    {
                        PlayerArray[i] = new Druid(TextureBank.playerTextureList[2], new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    else if (currentCharacter[i] == 3)
                    {
                        PlayerArray[i] = new Ranger(TextureBank.playerTextureList[3], new Vector2(100 * i + 100, 200), i, controllerArray[i]);
                    }

                    characterChosen[i] = true;
                    readyPlayers++;
                }

                if (controllerArray[i].ButtonPressed(Buttons.B) && characterChosen[i])
                {
                    characterChosen[i] = false;
                    readyPlayers--;
                    justPressed = true;
                }
                else if (controllerArray[0].ButtonPressed(Buttons.B) && selectingCharacter[0] && !justPressed)
                {
                    for (int j = 0; j < nrOfPlayers; j++)
                    {
                        characterChosen[j] = false;
                        currentCharacter[j] = 0;
                        readyPlayers = 0;
                    }
                    GameManager.currentState = GameState.MainMenu;
                }
            }
        }
        /// <summary>
        /// Om man trycker på D-paden ,höger eller vänster, så bläddrar man mellan valbara karaktärer.
        /// </summary>
        private void BrowsingCharacter()
        {
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

        /// <summary>
        /// Den här metoden sätter om värdena till ursprungs värden när spelet ska restarta.
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                selectingCharacter[i] = false;
                connectedController[i] = true;
                currentCharacter[i] = 0;
                PlayerArray[i] = null;
                characterChosen[i] = false;
                readyPlayers = 0;
                HUDManager = null;
            }
        }
    }
}
