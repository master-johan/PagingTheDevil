using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil.Manager
{
    enum States { GoingDown, GoingUp, None }

    class MenuManager
    {
        States current, previous;

        Game1 game;

        Pointer pointer;

        MainMenuBackground mainMenuBackground;

        List<Button> buttonList = new List<Button>();

        Controller[] controllerArray;

        int selectedBtn;
        int nrOfPlayers;
        int middleScreenY;
        int middleScreenX;
        int scrollSpeed;

        Vector2 pointerPos;
        Vector2 storyTextPos;

        StreamReader streamReader;

        string story;

        public PlayerSelectManager PlayerSelectManager { get; set; }

        public MenuManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.game = game;

            middleScreenY = (ValueBank.WindowSizeY / 2);
            middleScreenX = (ValueBank.WindowSizeX/2);

            buttonList.Add(new Button(TextureBank.menuTextureList[0], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width/ 2, middleScreenY - TextureBank.menuTextureList[0].Height)));
            buttonList.Add(new Button(TextureBank.menuTextureList[2], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width/ 2, middleScreenY - TextureBank.menuTextureList[0].Height + 200)));

            pointerPos = new Vector2(buttonList[0].GetPos.X - 200, buttonList[0].GetPos.Y + 10);
            storyTextPos = new Vector2(70, ValueBank.WindowSizeY - 100);
            pointer = new Pointer(TextureBank.menuTextureList[5], pointerPos, buttonList);

            selectedBtn = 0;
            buttonList[0].activeButton = true;
            current = States.None;

            mainMenuBackground = new MainMenuBackground();
            PlayerSelectManager = new PlayerSelectManager();

            scrollSpeed = 3;


            ReadStory();
        }

        public void Update(GameTime gameTime)
        {
            switch (GameManager.currentState)
            {
                case GameState.StoryScreen:

                    storyTextPos.Y -= scrollSpeed;

                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        if (controllerArray[i].ButtonDown(Buttons.A))
                        {
                            scrollSpeed = 6;

                        }
                        else
                        {
                            scrollSpeed = 3;
                        }
                    
                    }
                    mainMenuBackground.Update(gameTime);

                    break;
                case GameState.MainMenu:
                    mainMenuBackground.Update(gameTime);

                    foreach (var b in buttonList)
                    {
                        b.Update();
                    }

                    pointer.Update(gameTime);

                    previous = current;

                    MovingInMenu();

                    UpdateButtonChoise();

                    if (controllerArray[0].ButtonPressed(Buttons.A))
                    {
                        ButtonClick();
                    }

                    break;
                case GameState.PlayerSelect:
                    PlayerSelectManager.GetController(controllerArray);
                    SendPlayerToPlayerSelect();
                    PlayerSelectManager.Update(gameTime);
                    break;
            }
        }      
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (GameManager.currentState)
            {
                case GameState.StoryScreen:
                    mainMenuBackground.Draw(spriteBatch);
                    spriteBatch.DrawString(TextureBank.spriteFont, story, storyTextPos, Color.Green);
                    break;
                case GameState.MainMenu:

                    mainMenuBackground.Draw(spriteBatch);                   
                    spriteBatch.Draw(TextureBank.menuTextureList[4], new Vector2(middleScreenX - TextureBank.menuTextureList[4].Width/2, 100), Color.White);

                    foreach (var b in buttonList)
                    {
                        b.Draw(spriteBatch);
                    }

                    pointer.Draw(spriteBatch);
                    break;
                case GameState.PlayerSelect:

                    PlayerSelectManager.Draw(spriteBatch);
                    spriteBatch.Draw(TextureBank.menuTextureList[8], new Vector2(middleScreenX - TextureBank.menuTextureList[8].Width / 2, 100), Color.White);
                    break;
            }
        }
        /// <summary>
        /// Den här metoden gör att man kan röra sig i menyn
        /// </summary>
        private void MovingInMenu()
        {
            if (controllerArray[0].gamePadState.ThumbSticks.Left.Y > 0.5f)
            {
                current = States.GoingUp;
            }

            else if (controllerArray[0].gamePadState.ThumbSticks.Left.Y < -0.5f)
            {
                current = States.GoingDown;
            }

            else
            {
                current = States.None;
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar vad som händer när man trycker på en knapp.
        /// </summary>
        public void ButtonClick()
        {
            if (buttonList[0].activeButton)
            {
                GameManager.currentState = GameState.PlayerSelect;
            }

            else if (buttonList[1].activeButton)
            {
                game.Exit();
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar vid byte av knappval.
        /// </summary>
        private void UpdateButtonChoise()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (current == States.GoingDown && previous != States.GoingDown && selectedBtn < (buttonList.Count - 1))
                {
                    selectedBtn++;
                }

                else if (current == States.GoingUp && previous != States.GoingUp && selectedBtn > 0)
                {
                    selectedBtn--;
                }

                buttonList[i].activeButton = false;
                buttonList[selectedBtn].activeButton = true;
            }
        }
        /// <summary>
        /// Den här metoden får kontroller av GameManager
        /// </summary>
        /// <param name="controllerArray"></param>
        public void GetController(Controller[] controllerArray)
        {
            this.controllerArray = controllerArray;
        }
        /// <summary>
        /// Den här metoden får player av GameManager
        /// </summary>
        /// <param name="playerArray"></param>
        public void GetPlayer(int nrOfPlayers)
        {
            this.nrOfPlayers = nrOfPlayers;
        }
        /// <summary>
        /// Den här metoden skickar playerArray till PlayerSelect
        /// </summary>
        private void SendPlayerToPlayerSelect()
        {
            PlayerSelectManager.GetNrOfPlayers(nrOfPlayers);
        }
        /// <summary>
        /// Läser av textfilen med story. @ används som radbrytare.
        /// </summary>
        private void ReadStory()
        {
            streamReader = new StreamReader(@"story.txt");

            while(!streamReader.EndOfStream)
            {
                story += streamReader.ReadLine();
            }
            streamReader.Close();

            story = story.Replace("@", /*"@" +*/ System.Environment.NewLine);
        }
    }
}
