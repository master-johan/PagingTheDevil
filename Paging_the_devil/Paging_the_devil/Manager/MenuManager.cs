using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        float scrollSpeed;

        Vector2 pointerPos;
        Vector2 storyTextPos;
        Vector2 skipTextPos;

        StreamReader streamReader;

        string story;
        

        public bool StoryEnded { get; set; }

        public PlayerSelectManager PlayerSelectManager { get; set; }

        public MenuManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.game = game;

            middleScreenY = (ValueBank.WindowSizeY / 2);
            middleScreenX = (ValueBank.WindowSizeX/2);

            buttonList.Add(new Button(TextureBank.menuTextureList[0], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width/ 2, middleScreenY - TextureBank.menuTextureList[0].Height)));
            buttonList.Add(new Button(TextureBank.menuTextureList[1], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width/ 2, middleScreenY - TextureBank.menuTextureList[0].Height + 150)));
            buttonList.Add(new Button(TextureBank.menuTextureList[2], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 300)));

            pointerPos = new Vector2(buttonList[0].GetPos.X - 200, buttonList[0].GetPos.Y + 10);
            storyTextPos = new Vector2(70, ValueBank.WindowSizeY - 100);
            skipTextPos = new Vector2(ValueBank.WindowSizeX - 400, 50);

            pointer = new Pointer(TextureBank.menuTextureList[5], pointerPos, buttonList);

            selectedBtn = 0;
            buttonList[0].activeButton = true;
            current = States.None;

            mainMenuBackground = new MainMenuBackground();
            PlayerSelectManager = new PlayerSelectManager();

            scrollSpeed = 0.6f;

            ReadStory();
        }

        public void Update(GameTime gameTime)
        {
            switch (GameManager.currentState)
            {
                case GameState.StoryScreen:

                    storyTextPos.Y -= scrollSpeed;

                    mainMenuBackground.Update(gameTime);

                    if (storyTextPos.Y < -1600)
                    {
                        GameManager.currentState = GameState.InGame;
                        MediaPlayer.Play(SoundBank.BgMusicList[0]);
                    }

                    break;
                case GameState.MainMenu:
                    mainMenuBackground.Update(gameTime);

                    BackToMainMenu();

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
                case GameState.Controls:
                    BackToMainMenu();
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
                    spriteBatch.DrawString(TextureBank.spriteFont, story, storyTextPos, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                    spriteBatch.Draw(TextureBank.menuTextureList[17], skipTextPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                    break;
                case GameState.MainMenu:

                    mainMenuBackground.Draw(spriteBatch);                   
                    spriteBatch.Draw(TextureBank.menuTextureList[4], new Vector2(middleScreenX - TextureBank.menuTextureList[4].Width / 2, 100), null, Color.White, 0, Vector2.Zero, 1,SpriteEffects.None, 0.3f);


                    foreach (var b in buttonList)
                    {
                        b.Draw(spriteBatch);
                    }

                    pointer.Draw(spriteBatch);
                    break;
                case GameState.Controls:
                    spriteBatch.Draw(TextureBank.menuTextureList[16], Vector2.Zero, Color.White);
                    break;
                case GameState.PlayerSelect:

                    PlayerSelectManager.Draw(spriteBatch);
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
                GameManager.currentState = GameState.Controls;
            }
            else if (buttonList[2].activeButton)
            {
                game.Exit();
            }
        }
        /// <summary>
        /// Denna metoden ändrar currentState till main menu.
        /// </summary>
        private void BackToMainMenu()
        {
            if (controllerArray[0].ButtonPressed(Buttons.B))
            {
                GameManager.currentState = GameState.MainMenu;
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar vid byte av knappval.
        /// </summary>
        private void UpdateButtonChoise()
        {
            if (current == States.GoingDown && previous != States.GoingDown && selectedBtn < (buttonList.Count - 1))
            {
                selectedBtn++;
            }

            else if (current == States.GoingUp && previous != States.GoingUp && selectedBtn > 0)
            {
                selectedBtn--;
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
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
