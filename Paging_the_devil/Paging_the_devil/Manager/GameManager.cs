using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework.Media;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.GameObject.Characters;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.Manager
{
    public enum GameState { StoryScreen ,MainMenu, PlayerSelect, InGame }


    class GameManager
    {
        public static GameState currentState;

        GraphicsDevice graphicsDevice;

        GraphicsDeviceManager graphics;

        MenuManager menuManager;

        Game1 game;

        RoomManager roomManager;

        LevelManager levelManager;

        Room currentRoom;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Enemy> enemyList;


        int nrOfPlayers; 

        bool roomManagerCreated;
        bool hudManagerCreated;

        bool[] connectedController;

        public HUDManager HUDManager { get; set; }

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

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(SoundBank.BgMusicList[0]);

            ArrayGenerator();

            menuManager = new MenuManager(graphicsDevice, game);

            ConnectController();

            SetWindowSize(graphics);
        }
        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.StoryScreen:
                    ConnectController();
                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        if (controllerArray[i].ButtonPressed(Buttons.X))
                        {
                            currentState = GameState.InGame;
                        }
                    }
                    if (controllerArray[0] != null)
                    {
                        SendControllerToMenu();
                        controllerArray[0].Update();
                        menuManager.Update(gameTime);
                    }
                    break;
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
                    playerArray = menuManager.PlayerSelectManager.PlayerArray;
                    menuManager.Update(gameTime);
                    DisconnectController();

                    break;
                case GameState.InGame:
                    if (HUDManager == null)
                    {
                        HUDManager = menuManager.PlayerSelectManager.HUDManager;
                        hudManagerCreated = true;
                    }

                    if (roomManager == null)
                    {
                        CreateRoomManager();
                        currentRoom = roomManager.CurrentRoom;
                    }
                    //Ta bort i slutprodukten 
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        for (int i = 0; i < nrOfPlayers; i++)
                        {
                            playerArray[i].HealthPoints -= 0.5f;
                        }
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        for (int i = 0; i < nrOfPlayers; i++)
                        {
                            playerArray[i].HealthPoints += 0.5f;
                        }
                    }

                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        CheckPlayerAbilites(playerArray[i].abilityList, playerArray[i]);
                    }

                    foreach (var e in enemyList)
                    {
                        if (e is SmallDevil)
                        {
                            CheckEnemiesAbilites((e as SmallDevil).enemyAbilityList);
                        }
                        if(e is WallSpider)
                        {
                            CheckEnemiesAbilites((e as WallSpider).enemyAbilityList);
                        }
                        if (e is Slime)
                        {
                            CheckSlimeCollision(e as Slime);
                        }
                        if (e is Devil)
                        {
                            CheckEnemiesAbilites((e as Devil).enemyAbilityList);
                        }
                    }

                    HUDManager.Update(gameTime);
                    UpdatePlayersDirection();
                    CharacterÚpdate(gameTime);
                    DeleteAbilities();
                    roomManager.Update();

                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case GameState.StoryScreen:
                    menuManager.Draw(spriteBatch);
                    break;
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

                    if (hudManagerCreated)
                    {
                        HUDManager.Draw(spriteBatch);
                    }

                    break;
            }
        }
        /// <summary>
        /// Den här metoden sätter fönstrets storlek
        /// </summary>
        /// <param name="graphics"></param>
        private static void SetWindowSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = ValueBank.WindowSizeY = 1080;
            graphics.PreferredBackBufferWidth = ValueBank.WindowSizeX = 1920;
            graphics.ApplyChanges();
            ValueBank.GameWindowStartY = 135;
        }
        /// <summary>
        /// Den här metoden anger värden till olika Arrays och skapar portaler.
        /// </summary>
        private void ArrayGenerator()
        {
            controllerArray = new Controller[4];
            connectedController = new bool[4];
            playerArray = new Player[4];
        }

        /// <summary>
        /// Den här metoden uppdaterar spelare samt enemies samt tar bort enemies vid död.
        /// </summary>
        private void CharacterÚpdate(GameTime gameTime)
        {
            Enemy toRemoveEnemy = null;

            foreach (var e in enemyList)
            {
                e.Update(gameTime);

                if (e.toRevome)
                {
                    toRemoveEnemy = e;
                }

                if (e.TrapTimer < 0)
                {
                    e.HitBySlowTrap = false;
                    e.MovementSpeed = e.BaseMoveSpeed;
                }
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                controllerArray[i].Update();
                playerArray[i].Update(gameTime);

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
        /// <summary>
        /// Denna metoden kopplar från kontrollerna
        /// </summary>
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
        /// <summary>
        /// Den här metoden tar bort abilities vid interaktion med enemies.
        /// </summary>
        private void DeleteAbilities()
        {
            Ability toRemove = null;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var w in currentRoom.GetWallRectList())
                    {
                        if (a.GetRect.Intersects(w))
                        {
                            if (a.ToRemove == true)
                            {
                                toRemove = a;
                            }

                            if (a is Taunt || a is FlowerPower)
                            {
                                a.ToRemove = false;
                            }
                            else
                            {
                                a.ToRemove = true;
                            }
                        }

                        if (a.ToRemove == true)
                        {
                            toRemove = a;
                        }
                    }
                }

                foreach (var e in enemyList)
                {
                    foreach (var a in e.enemyAbilityList)
                    {
                        if (a.GetRect.Intersects(playerArray[i].GetRect))
                        {
                            a.ToRemove = true;
                        }
                        if (a.ToRemove == true)
                        {
                            toRemove = a;
                        }
                    }
                }
                if (toRemove != null)
                {
                    playerArray[i].abilityList.Remove(toRemove);
                }
            }
        }
        /// <summary>
        /// Den här metoden kollar interaktion mellan player-abilities och enemies
        /// </summary>
        /// <param name="abilityList"></param>
        /// <param name="player"></param>
        private void CheckPlayerAbilites(List<Ability> abilityList, Player player)
        {
            Ability toRemove = null;

            foreach (var a in abilityList)
            {
                foreach (var e in enemyList)
                {
                    if (a.GetRect.Intersects(e.GetRect))
                    {
                        a.HitCharacter = e;

                        if (a is Slash ) // arbeta mer för att fixa slashen
                        {
                            (a as Slash).Hit = true;
                        }
                    }
                }
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    if (a.GetRect.Intersects(playerArray[i].GetRect))
                    {
                        if (a is Healharm || a is FlowerPower)
                        {
                            if (player == playerArray[i])
                            {
                                continue;
                            }
                            a.HitCharacter = playerArray[i];
                            
                        }
                    }
                }

                if (a.ToRemove)
                {
                    toRemove = a;
                }
            }

            if (toRemove != null)
            {
                abilityList.Remove(toRemove);
            }
        }
        /// <summary>
        /// Den här metoden kollar interaktion mellan enemy-abilities och players
        /// </summary>
        /// <param name="abilityList"></param>
        private void CheckEnemiesAbilites(List<Ability> abilityList)
        {
            Ability toRemove = null;

            foreach (var a in abilityList)
            {
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    if (a.GetRect.Intersects(playerArray[i].GetRect))
                    {
                        playerArray[i].HealthPoints -= a.Damage;
                        a.ToRemove = true;
                    }
                }

                foreach (var w in currentRoom.GetWallRectList())
                {
                    if (a.GetRect.Intersects(w))
                    {
                        a.ToRemove = true;
                    }
                }

                if (a.ToRemove == true)
                {
                    toRemove = a;
                }
            }

            if (toRemove != null)
            {
                abilityList.Remove(toRemove);
            }
        }
        /// <summary>
        /// Kollar kollisionen för slime och spelare.
        /// </summary>
        /// <param name="slime"></param>
        private void CheckSlimeCollision(Slime slime)
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (slime.GetRect.Intersects(playerArray[i].GetRect))
                {
                    playerArray[i].HealthPoints = 0;
                }
            }
        }
    }
}

