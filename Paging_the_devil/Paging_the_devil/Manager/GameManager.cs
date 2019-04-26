﻿using System;
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
using Microsoft.Xna.Framework.Media;
using Paging_the_devil.GameObject.EnemyFolder;

namespace Paging_the_devil.Manager
{
    public enum GameState { MainMenu, PlayerSelect, InGame }


    class GameManager
    {
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;

        MenuManager menuManager;
        public HUDManager HUDManager { get; set; }
        Game1 game;

        int nrOfPlayers;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Enemy> enemyList;

        bool roomManagerCreated;
        bool hudManagerCreated;

        bool[] connectedController;

        public static GameState currentState;

        RoomManager roomManager;
        LevelManager levelManager;

        Room currentRoom;

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
            MediaPlayer.Play(SoundManager.BgMusicList[0]);

            CreatingThings();

            menuManager = new MenuManager(graphicsDevice, game);


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
            TextureManager.GameWindowStartY = 135;
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
                    playerArray = menuManager.PlayerSelectManager.GetPlayerArray();
                    menuManager.Update(gameTime);
                    DisconnectController();

                    break;
                case GameState.InGame:

                    if (HUDManager == null)
                    {
                        HUDManager = menuManager.PlayerSelectManager.GetHudManager();
                        hudManagerCreated = true;

                    }
                    if (roomManager == null)
                    {
                        CreateRoomManager();
                        currentRoom = roomManager.CurrentRoom;
                    }


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

                        if (e is Slime)
                        {
                            CheckSlimeCollision(e as Slime);
                        }
                    }


                    HUDManager.Update(gameTime);
                    UpdatePlayersDirection();
                    UpdateCharacters(gameTime);

                    //UpdateHealth();
                    //DeleteAbilities();


                    roomManager.Update();


                    break;
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar spelare samt enemies samt tar bort enemies vid död.
        /// </summary>
        private void UpdateCharacters(GameTime gameTime)
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
                    e.hitBySlowTrap = false;
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
                    if (hudManagerCreated)
                    {
                        HUDManager.Draw(spriteBatch);
                    }
                    spriteBatch.Draw(TextureManager.mageSpellList[4], new Rectangle(100, 100, TextureManager.mageSpellList[4].Width, TextureManager.mageSpellList[4].Height), Color.White);


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

        /// <summary>
        /// Den här metoden tar bort abilities vid interaktion med enemies.
        /// </summary>
        private void DeleteAbilities()
        {
            Ability toRemoveAbility = null;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var w in currentRoom.GetWallList())
                    {
                        if (a.GetRect.Intersects(w.GetRect))
                        {
                            toRemoveAbility = a;
                        }
                    }
                }

                //foreach (var a in playerArray[i].abilityList)
                //{
                //    foreach (var e in enemyList)
                //    {
                //        if (a.GetRect.Intersects(e.GetRect))
                //        {
                //            toRemoveAbility = a;
                //        }
                //    }
                //}

                foreach (var e in enemyList)
                {
                    foreach (var a in e.enemyAbilityList)
                    {
                        if (a.GetRect.Intersects(playerArray[i].GetRect))
                        {
                            toRemoveAbility = a;
                        }
                    }
                }
                if (toRemoveAbility != null)
                {
                    playerArray[i].abilityList.Remove(toRemoveAbility);
                }
            }
        }
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
                        if (a is Slash) // arbeta mer för att fixa slashen
                        {

                            (a as Slash).Hit = true;
                        }

                        //if ((a is Slash))
                        //{
                        //    if (!(a as Slash).Hit)
                        //    {
                        //        e.HealthPoints -= a.Damage;
                        //    }
                        //    (a as Slash).Hit = true;
                        //}
                        //else
                        //{
                        //    e.HealthPoints -= a.Damage;
                        //    toRemove = a;
                        //}

                        //if (a is Trap)
                        //{
                        //    e.hitBySlowTrap = true;
                        //    e.MovementSpeed -= 2;
                        //    //ValueBank.SlimeSpeed -= 0.5f;
                        //}

                        //if (a is Healharm)
                        //{
                        //    (a as Healharm).Active = true;
                        //    (a as Healharm).DmgOverTime(e);
                        //}

                    }
                }
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    if (a.GetRect.Intersects(playerArray[i].GetRect))
                    {
                        if (a is Healharm)
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

                


                //foreach (var w in currentRoom.GetWallList())
                //{
                //    if (a.GetRect.Intersects(w.GetRect))
                //    {
                //        toRemove = a;
                //    }
                //}
            }
            if (toRemove != null)
            {
                abilityList.Remove(toRemove);
            }
        }

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
                        toRemove = a;
                    }
                }

                foreach (var w in currentRoom.GetWallList())
                {
                    if (a.GetRect.Intersects(w.GetRect))
                    {

                        toRemove = a;

                    }
                }
            }

            if (toRemove != null)
            {
                abilityList.Remove(toRemove);
            }


        }
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

