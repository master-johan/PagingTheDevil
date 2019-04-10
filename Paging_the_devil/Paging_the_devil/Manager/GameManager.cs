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
        Game1 game;

        int nrOfPlayers;
        int windowX, windowY;

        Vector2 portalPos, portalRoom2, playerPos, playerPos2, portalRoom3, portalRoom4;

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Enemy> enemyList;



        bool[] playerConnected;
        bool roomManagerCreated;

        bool[] connectedController;


        public static GameState currentState;

        RoomManager roomManager;

        


        public GameManager(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;
            this.graphics = graphics;
            this.game = game;
            GameWindow(graphics);


            menuManager = new MenuManager(graphicsDevice, game);
            

            enemyList = new List<Enemy>();

            currentState = GameState.MainMenu;

            CreatingThings();


            ConnectController();

            SetWindowSize(graphics);

        }

        private static void SetWindowSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = TextureManager.WindowSizeY = 1080;
            graphics.PreferredBackBufferWidth = TextureManager.WindowSizeX = 1920;
            graphics.ApplyChanges();
        }


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
        /// <summary>
        /// Den här metoden sätter storlekar.
        /// </summary>
        private void DecidingPosses()
        {
            WallTopPos = new Rectangle(0, 0, windowX, 20);
            WallBottomPos = new Rectangle(0, windowY - 20, windowX, 20);
            WallLeftPos = new Rectangle(0, 0, 20, windowY);
            WallRightPos = new Rectangle(windowX - 20, 0, 20, windowY);

            portalPos = new Vector2(300, 430);
            portalRoom2 = new Vector2(300, -10);
            portalRoom3 = new Vector2(1300, 350);
            portalRoom4 = new Vector2(-10, 350);

            playerPos = new Vector2(200, 400);
            playerPos2 = new Vector2(320, 100);
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

        private void GameWindow(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = windowY = TextureManager.WindowSizeY;
            graphics.PreferredBackBufferWidth = windowX = TextureManager.WindowSizeX;
            graphics.ApplyChanges();
        }

        private void SpawnEnemy()
        {
            Random rand = new Random();
            int x = rand.Next((windowX / 2) + 20, windowX - TextureManager.enemyTextureList[0].Width - 20);
            int y = rand.Next(20, windowY - TextureManager.enemyTextureList[0].Height - 20);

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



        public void CreateRoomManager()
        {
            roomManager = new RoomManager(playerArray, nrOfPlayers, enemyList);
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

