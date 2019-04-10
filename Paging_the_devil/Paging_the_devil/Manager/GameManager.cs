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

namespace Paging_the_devil
{
    public enum GameState { MainMenu, PlayerSelect, InGame }


    public class GameManager
    {
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;

        MenuManager menuManager;
        Game1 game;

        int nrOfPlayers;
        int windowX, windowY;

        Vector2 portalPos, portalRoom2, playerPos, playerPos2, portalRoom3, portalRoom4;

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;

        GamePadCapabilities[] connectedC;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Controller> controllerList;
        List<Enemy> enemyList;


        bool[] playerConnected;
        bool roomManagerCreated;

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
            controllerList = new List<Controller>();

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

        private void CreatingThings()
        {


            connectedC = new GamePadCapabilities[4] { GamePad.GetCapabilities(PlayerIndex.One), GamePad.GetCapabilities(PlayerIndex.Two), GamePad.GetCapabilities(PlayerIndex.Three), GamePad.GetCapabilities(PlayerIndex.Four) };
            controllerArray = new Controller[4];
            playerConnected = new bool[4];
            playerArray = new Player[4];
        }

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
                    if (nrOfPlayers >= 1)
                    {
                        menuManager.GetController(controllerArray[0].GetPadState());
                    }
                    if (controllerArray[0] != null)
                    {
                        controllerArray[0].Update();
                    }
                    menuManager.Update(gameTime);
                    ConnectPlayer();
                    break;
                case GameState.PlayerSelect:

                    
                    break;
                case GameState.InGame:
                    if (roomManager == null)
                    {
                        CreateRoomManager();
                    }

                    UpdatePlayersDirection();
                    UpdateCharacters();

                    roomManager.Update();

                    break;
            }
        }

        private void UpdateCharacters()
        {
            foreach (var e in enemyList)
            {
                e.Update();
            }
            for (int i = 0; i < nrOfPlayers; i++)
            {
                controllerArray[i].Update();
                playerArray[i].Update();
            }
        }

        private void UpdatePlayersDirection()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].GetDirection() != Vector2.Zero)
                {
                    playerArray[i].LastInputDirection(controllerArray[i].GetDirection());
                }
                playerArray[i].InputDirection(controllerArray[i].GetDirection());
                playerArray[i].InputPadState(controllerArray[i].GetPadState());
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
        private void ConnectController()
        {
            for (int i = 0; i < connectedC.Length; i++)
            {
                if (connectedC[i].IsConnected)
                {
                    PlayerIndex index = (PlayerIndex)i;

                    controllerArray[i] = new Controller(index);

                    nrOfPlayers++;
                }

                playerConnected[i] = false;

            }

        }
        private void ConnectPlayer()
        {
            for (int i = 0; i < controllerArray.Length; i++)
            {
                if (connectedC[i].IsConnected && playerConnected[i] == false)
                {
                    playerArray[i] = new Player(TextureManager.playerTextureList[0], new Vector2(100 * i + 50, 100), new Rectangle(0, 0, 10, 10), i, controllerArray[i]);
                    
                    playerConnected[i] = true;

                }
            }
        }


        public void CreateRoomManager()
        {
            roomManager = new RoomManager(playerArray, nrOfPlayers, enemyList);
            roomManagerCreated = true;

        }

    }
}

