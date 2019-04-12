using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.GameObject;

namespace Paging_the_devil.Manager
{
    enum States { GoingDown, GoingUp, None }
    class MenuManager
    {
        Game1 game;
        int selectedBtn;
        int nrOfPlayers;
        Pointer pointer;
        List<Button> buttonList = new List<Button>();
        Vector2 pointerPos;

        States current, previous;

        PlayerSelectManager playerSelectManager;

        Controller[] controllerArray;

        public MenuManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.game = game;

            buttonList.Add(new Button(TextureManager.menuTextureList[0], graphicsDevice, new Vector2(400, 300)));
            buttonList.Add(new Button(TextureManager.menuTextureList[2], graphicsDevice, new Vector2(400, 500)));

            pointerPos = new Vector2(buttonList[0].GetPos.X - 200, buttonList[0].GetPos.Y + 10);
            pointer = new Pointer(TextureManager.menuTextureList[5], pointerPos, buttonList);

            selectedBtn = 0;
            buttonList[0].activeButton = true;
            current = States.None;


            playerSelectManager = new PlayerSelectManager();
        }

        public void Update(GameTime gameTime)
        {
            switch (GameManager.currentState)
            {
                case GameState.MainMenu:

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
                    playerSelectManager.GetController(controllerArray);
                    SendPlayerToPlayerSelect();
                    playerSelectManager.Update();
                    break;
            }
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (GameManager.currentState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(TextureManager.menuTextureList[3], Vector2.Zero, Color.White);
                    spriteBatch.Draw(TextureManager.menuTextureList[4], new Vector2(100, 0), Color.White);

                    foreach (var b in buttonList)
                    {
                        b.Draw(spriteBatch);
                    }
                    pointer.Draw(spriteBatch);
                    break;
                case GameState.PlayerSelect:
                    playerSelectManager.Draw(spriteBatch);
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
            playerSelectManager.GetNrOfPlayers(nrOfPlayers);
        }

        public Player[] GetAndSendPlayerArray()
        {
            Player[] playerArray = playerSelectManager.GetPlayerArray();

            return playerArray;

        }
        public HUDManager HudManagerSend()
        {
            HUDManager hud = playerSelectManager.getHudmanager();
            return hud;
        }
    }
}
