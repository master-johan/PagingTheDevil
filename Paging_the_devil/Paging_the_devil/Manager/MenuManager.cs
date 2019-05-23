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

        Pointer pointerMainMenu;
        Pointer pointerPause;

        MainMenuBackground mainMenuBackground;

        List<Button> mainMenuButtonList = new List<Button>();
        List<Button> pauseButtonList = new List<Button>();

        Controller[] controllerArray;

        int selectedBtn;
        int nrOfPlayers;
        int middleScreenY;
        int middleScreenX;
        float scrollSpeed;

        Vector2 pointerPosMainMenu;
        Vector2 pointerPosPause;
        Vector2 storyTextPos;
        Vector2 skipTextPos;

        StreamReader streamReader;

        string story;

        bool fromPause;
        public bool gamePaused;


        public bool StoryEnded { get; set; }

        public PlayerSelectManager PlayerSelectManager { get; set; }

        public MenuManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.game = game;

            middleScreenY = (ValueBank.WindowSizeY / 2);
            middleScreenX = (ValueBank.WindowSizeX / 2);

            CreatingButtons(graphicsDevice);

            pointerPosMainMenu = new Vector2(mainMenuButtonList[0].GetPos.X - 200, mainMenuButtonList[0].GetPos.Y + 10);
            pointerPosPause = new Vector2(pauseButtonList[0].GetPos.X - 200, pauseButtonList[0].GetPos.Y + 10);

            storyTextPos = new Vector2(70, ValueBank.WindowSizeY /*- 100*/);
            skipTextPos = new Vector2(ValueBank.WindowSizeX - 400, 50);

            pointerMainMenu = new Pointer(TextureBank.menuTextureList[5], pointerPosMainMenu, mainMenuButtonList);
            pointerPause = new Pointer(TextureBank.menuTextureList[5], pointerPosPause, pauseButtonList);

            selectedBtn = 0;
            mainMenuButtonList[0].activeButton = true;
            current = States.None;

            mainMenuBackground = new MainMenuBackground();
            PlayerSelectManager = new PlayerSelectManager();

            scrollSpeed = 0.5f;

            ReadStory();
        }

        public void Update(GameTime gameTime)
        {
            switch (GameManager.currentState)
            {
                case GameState.StoryScreen:

                    storyTextPos.Y -= scrollSpeed;

                    mainMenuBackground.Update(gameTime);

                    if (storyTextPos.Y < -1100)
                    {
                        GameManager.currentState = GameState.InGame;
                        MediaPlayer.Play(SoundBank.BgMusicList[0]);
                    }

                    break;
                case GameState.MainMenu:
                    mainMenuBackground.Update(gameTime);

                    foreach (var b in mainMenuButtonList)
                    {
                        b.Update();
                    }

                    pointerMainMenu.Update(gameTime);

                    previous = current;

                    MovingInMenu();

                    UpdateButtonChoise();

                    if (controllerArray[0].ButtonPressed(Buttons.A))
                    {
                        ButtonClickMainMenu();
                    }

                    break;
                case GameState.Controls:
                    GobackInControls();
                    break;
                case GameState.PlayerSelect:
                    PlayerSelectManager.GetController(controllerArray);
                    SendPlayerToPlayerSelect();
                    PlayerSelectManager.Update(gameTime);
                    break;
                case GameState.Win:
                    mainMenuBackground.Update(gameTime);
                    break;
                case GameState.GameOver:
                    mainMenuBackground.Update(gameTime);
                    break;
            }

            if (gamePaused)
            {
                foreach (var b in pauseButtonList)
                {
                    b.Update();
                }

                pointerPause.Update(gameTime);

                previous = current;

                MovingInMenu();

                UpdateButtonChoise();

                if (controllerArray[0].ButtonPressed(Buttons.A))
                {
                    ButtonClickPause();
                }
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
                    spriteBatch.Draw(TextureBank.menuTextureList[4], new Vector2(middleScreenX - TextureBank.menuTextureList[4].Width / 2, 100), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);

                    foreach (var b in mainMenuButtonList)
                    {
                        b.Draw(spriteBatch);
                    }

                    pointerMainMenu.Draw(spriteBatch);
                    break;
                case GameState.Controls:
                    spriteBatch.Draw(TextureBank.menuTextureList[16], Vector2.Zero, Color.White);
                    break;
                case GameState.PlayerSelect:
                    PlayerSelectManager.Draw(spriteBatch);
                    break;
                case GameState.InGame:
                    spriteBatch.Draw(TextureBank.menuTextureList[18], Vector2.Zero, Color.White);

                    foreach (var b in pauseButtonList)
                    {
                        b.Draw(spriteBatch);
                    }

                    pointerPause.Draw(spriteBatch);

                    break;
                case GameState.Win:
                    mainMenuBackground.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    mainMenuBackground.Draw(spriteBatch);
                    break;
            }
        }
        /// <summary>
        ///  Den här metoden skapar knapparna för MainMenu och Pause Menu
        /// </summary>
        /// <param name="graphicsDevice"></param>
        private void CreatingButtons(GraphicsDevice graphicsDevice)
        {
            mainMenuButtonList.Add(new Button(TextureBank.menuTextureList[0], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height)));
            mainMenuButtonList.Add(new Button(TextureBank.menuTextureList[1], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 150)));
            mainMenuButtonList.Add(new Button(TextureBank.menuTextureList[20], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 300)));
            mainMenuButtonList.Add(new Button(TextureBank.menuTextureList[2], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 450)));

            pauseButtonList.Add(new Button(TextureBank.menuTextureList[19], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height)));
            pauseButtonList.Add(new Button(TextureBank.menuTextureList[1], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 150)));
            pauseButtonList.Add(new Button(TextureBank.menuTextureList[20], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 300)));
            pauseButtonList.Add(new Button(TextureBank.menuTextureList[2], graphicsDevice, new Vector2(middleScreenX - TextureBank.menuTextureList[0].Width / 2, middleScreenY - TextureBank.menuTextureList[0].Height + 450)));
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
        /// Den här metoden uppdaterar vad som händer när man trycker på en knapp i main manu.
        /// </summary>
        public void ButtonClickMainMenu()
        {
            if (mainMenuButtonList[0].activeButton)
            {
                GameManager.currentState = GameState.PlayerSelect;
            }
            else if (mainMenuButtonList[1].activeButton)
            {
                GameManager.currentState = GameState.Controls;
            }
            else if (mainMenuButtonList[2].activeButton)
            {

            }
            else if (mainMenuButtonList[3].activeButton)
            {
                game.Exit();
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar vad som händer när man trycker på en knapp i paus menyn
        /// </summary>
        public void ButtonClickPause()
        {
            if (pauseButtonList[0].activeButton)
            {
                gamePaused = false;
            }
            else if (pauseButtonList[1].activeButton)
            {
                GameManager.currentState = GameState.Controls;
                fromPause = true;
            }
            else if (pauseButtonList[2].activeButton)
            {

            }
            else if (pauseButtonList[3].activeButton)
            {
                game.Exit();
            }

        }
        /// <summary>
        /// Denna metoden ändrar currentState till main menu.
        /// </summary>
        private void GobackInControls()
        {

            if (controllerArray[0].ButtonPressed(Buttons.B) && fromPause == false)
            {
                GameManager.currentState = GameState.MainMenu;
            }
            else if (controllerArray[0].ButtonPressed(Buttons.B) && fromPause == true)
            {
                GameManager.currentState = GameState.InGame;
                fromPause = false;
            }

        }
        /// <summary>
        /// Den här metoden uppdaterar vid byte av knappval.
        /// </summary>
        private void UpdateButtonChoise()
        {
            if (current == States.GoingDown && previous != States.GoingDown && selectedBtn < (mainMenuButtonList.Count - 1))
            {
                selectedBtn++;
            }

            else if (current == States.GoingUp && previous != States.GoingUp && selectedBtn > 0)
            {
                selectedBtn--;
            }

            for (int i = 0; i < mainMenuButtonList.Count; i++)
            {
                mainMenuButtonList[i].activeButton = false;
                mainMenuButtonList[selectedBtn].activeButton = true;
            }

            for (int i = 0; i < pauseButtonList.Count; i++)
            {
                pauseButtonList[i].activeButton = false;
                pauseButtonList[selectedBtn].activeButton = true;
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

            while (!streamReader.EndOfStream)
            {
                story += streamReader.ReadLine();
            }
            streamReader.Close();

            story = story.Replace("@", /*"@" +*/ System.Environment.NewLine);
        }

    }
}
