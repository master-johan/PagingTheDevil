using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Paging_the_devil.GameObject;
using Paging_the_devil.Manager;

namespace Paging_the_devil.Manager
{
    public enum GameState { MainMenu, PlayerSelect, InGame }


    class GameManager
    {
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;

        MenuManager menuManager;
        HUDManager HUD;
        Game1 game;

        int nrOfPlayers;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Enemy> enemyList;
        
        bool roomManagerCreated;

        bool[] connectedController;

        public static GameState currentState;

        RoomManager roomManager;
        LevelManager levelManager;

        public GameManager(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;
            this.graphics = graphics;
            this.game = game;
            SetWindowSize(graphics);

            menuManager = new MenuManager(graphicsDevice, game);
            levelManager = new LevelManager();
            

            enemyList = new List<Enemy>();

            currentState = GameState.MainMenu;

            CreatingThings();

            menuManager = new MenuManager(graphicsDevice, game);
            HUD = new HUDManager(graphicsDevice);

            ConnectController();

            SetWindowSize(graphics);

        }
        /// <summary>
        /// Den här metoden sätter fönstrets storlek
        /// </summary>
        /// <param name="graphics"></param>
        private static void SetWindowSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = TextureManager.WindowSizeY = 1080;
            graphics.PreferredBackBufferWidth = TextureManager.WindowSizeX = 1920;
            graphics.ApplyChanges();
        }
        /// <summary>
        /// Den här metoden anger värden till olika Arrays och skapar portaler.
        /// </summary>
        private void CreatingThings()
        {
            controllerArray = new Controller[4];
            connectedController = new bool[4];
            playerArray = new Player[4];
        }
        
        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.MainMenu:
                    ConnectController();
                    if (controllerArray[0] != null)
                    {
                        SendControllerToMenu();
                        controllerArray[0].Update();
                        menuManager.Update(gameTime);
                    }
                    DisconnectController();
                    break;
                case GameState.PlayerSelect:

                    ConnectController();
                    UpdateController();
                    SendPlayerToMenu();
                    playerArray = menuManager.GetAndSendPlayerArray();
                    menuManager.Update(gameTime);
                    DisconnectController();

                    break;
                case GameState.InGame:
                    HUD.GetNrOfPlayersToHud(nrOfPlayers);
                    HUD.Update();
                    if (roomManager == null)
                    {
                        CreateRoomManager();
                    }
                    UpdatePlayersDirection();
                    UpdateCharacters();
                    UpdateHealth();

                    roomManager.Update();

                    break;
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar spelare samt enemies samt tar bort enemies vid död.
        /// </summary>
        private void UpdateCharacters()
        {
            Enemy toRemoveEnemy = null;
            foreach (var e in enemyList)
            {
                e.Update();
                if (e.toRevome)
                {
                    toRemoveEnemy = e;
                }
            }
            for (int i = 0; i < nrOfPlayers; i++)
            {
                controllerArray[i].Update();
                playerArray[i].Update();

            }
            if (toRemoveEnemy != null)
            {
                enemyList.Remove(toRemoveEnemy);
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar spelarens riktning samt senaste riktning.
        /// </summary>
        private void UpdatePlayersDirection()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].GetDirection() != Vector2.Zero)
                {
                    playerArray[i].LastInputDirection(controllerArray[i].GetDirection());
                }
                playerArray[i].InputDirection(controllerArray[i].GetDirection());
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar enemies interaktion med abilities
        /// </summary>
        private void UpdateHealth()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var e in enemyList)
                {
                    foreach (var a in playerArray[i].abilityList)
                    {
                        if (e.GetRect.Intersects(a.GetRect))
                        {
                            if ((a is Slash))
                            {
                                if (!(a as Slash).Hit)
                                {
                                    e.HealthPoints -= a.Damage;
                                }
                            }

                            else
                            {
                                e.HealthPoints -= a.Damage;
                            }

                            if ((a is Slash))
                            {
                                (a as Slash).Hit = true;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                for (int j = 0; j < enemyList.Count; j++)
                {
                    foreach (var a in enemyList[j].enemyAbilityList)
                    {
                        if (playerArray[i].GetRect.Intersects(a.GetRect))
                        {
                            playerArray[i].HealthPoints -= a.Damage;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar kontrollerna.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void UpdateController()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                controllerArray[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case GameState.MainMenu:
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.PlayerSelect:

                    menuManager.Draw(spriteBatch);

                    break;
                case GameState.InGame:

                    if (roomManagerCreated)
                    {
                        roomManager.Draw(spriteBatch);
                    }
                    HUD.Draw(spriteBatch);
                    DrawCharacters(spriteBatch);

                    DrawCharacters(spriteBatch);
               
                    break;
            }
        }
        /// <summary>
        /// Den här metoden ritar ut spelare samt enemies.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawCharacters(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerArray[i].Draw(spriteBatch);
            }

            foreach (var e in enemyList)
            {
                e.Draw(spriteBatch);
            }
        }

        private void SpawnEnemy()
        {
            Random rand = new Random();
            int x = rand.Next((TextureManager.WindowSizeX / 2) + 20, TextureManager.WindowSizeX - TextureManager.enemyTextureList[0].Width - 20);
            int y = rand.Next(20, TextureManager.WindowSizeY - TextureManager.enemyTextureList[0].Height - 20);

            Enemy enemy = new Enemy(TextureManager.enemyTextureList[0], new Vector2(x, y));
            enemyList.Add(enemy);
        }
        /// <summary>
        /// Den här metoden ansluter en kontroll.
        /// </summary>
        private void ConnectController()
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                if (GamePad.GetState(i).IsConnected && !connectedController[i])
                {
                    PlayerIndex index = (PlayerIndex)i;

                    controllerArray[i] = new Controller(index);
                    nrOfPlayers++;
                    connectedController[i] = true;
                }
            }
        }

        private void DisconnectController()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (!GamePad.GetState(i).IsConnected && connectedController[i])
                {
                    controllerArray[i] = null;
                    nrOfPlayers--;
                    connectedController[i] = false;
                }
            }
        }
        /// <summary>
        /// Den här metoden skapar en roomManager
        /// </summary>
        public void CreateRoomManager()
        {
            roomManager = new RoomManager(playerArray, nrOfPlayers, enemyList, levelManager);
            roomManagerCreated = true;

        }
        /// <summary>
        /// Den här metoden uppdaterar menumanagerns controllerArray.
        /// </summary>
        private void SendControllerToMenu()
        {
            menuManager.GetController(controllerArray);
        }
        /// <summary>
        /// Den här metoden uppdaterar menumanagerns playerArray.
        /// </summary>
        private void SendPlayerToMenu()
        {
            menuManager.GetPlayer(nrOfPlayers);
        }
    }
}

