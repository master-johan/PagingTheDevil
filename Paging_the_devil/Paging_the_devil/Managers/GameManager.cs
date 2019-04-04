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

namespace Paging_the_devil
{
    public enum GameState { MainMenu, PlayerSelect, InGame }
    enum Room { One, Two, Three }

    public class GameManager
    {
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphics;

        MenuManager menuManager;
        Game1 game;
        Portal portal, portal2;
        Player player;

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

        public static GameState currentState;
        Room currentRoom;


        public GameManager(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;
            this.graphics = graphics;
            this.game = game;
            GameWindow(graphics);

            menuManager = new MenuManager(graphicsDevice, game);
            
            currentState = GameState.MainMenu;
            currentRoom = Room.One;

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

            portal = new Portal(TextureManager.rommTextureList[0], portalPos);
            portal2 = new Portal(TextureManager.rommTextureList[0], portalRoom3);

            enemyList = new List<Enemy>();
            controllerList = new List<Controller>();

            connectedC = new GamePadCapabilities[4] { GamePad.GetCapabilities(PlayerIndex.One), GamePad.GetCapabilities(PlayerIndex.Two), GamePad.GetCapabilities(PlayerIndex.Three), GamePad.GetCapabilities(PlayerIndex.Four) };
            controllerArray = new Controller[4];
            playerConnected = new bool[4];
            playerArray = new Player[4];

           ConnectController();
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.MainMenu:
                    if(nrOfPlayers >= 1)
                    {
                        menuManager.GetController(controllerArray[0].GetPadState());
                    }
                    controllerArray[0].Update();
                    menuManager.Update(gameTime);
                    ConnectPlayer();
                    break;
                case GameState.PlayerSelect:
                    break;
                case GameState.InGame:
            
                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        controllerArray[i].Update();
                        playerArray[i].Update();
                    }

                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        playerArray[i].InputDirection(controllerArray[i].GetDirection());
                        playerArray[i].InputPadState(controllerArray[i].GetPadState());
                    }

                    foreach (var e in enemyList)
                    {
                        e.Update();
                    }

                    Collision();

                    portal.Update();
                    portal2.Update();

                    DeleteAbilities();

                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        switch (currentRoom)
                        {
                            case Room.One:
                                if (playerArray[i].GetRect.Intersects(portal.GetRect) && controllerArray[i].GetPadState().IsButtonDown(Buttons.Y))
                                {
                                    currentRoom = Room.Two;
                                    SpawnEnemy();
                                }

                                break;
                            case Room.Two:
                                if (playerArray[i].GetRect.Intersects(portal.GetRect) && controllerArray[i].GetPadState().IsButtonDown(Buttons.Y))
                                {
                                    portal.GetSetPos = portalPos;
                                    currentRoom = Room.One;
                                    SpawnEnemy();
                                }
                                else if (playerArray[i].GetRect.Intersects(portal2.GetRect) && controllerArray[i].GetPadState().IsButtonDown(Buttons.Y))
                                {
                                    portal2.GetSetPos = portalRoom3;
                                    currentRoom = Room.Three;
                                    portal.GetSetPos = portalRoom4;
                                    SpawnEnemy();
                                }
                                break;
                            case Room.Three:
                                if (playerArray[i].GetRect.Intersects(portal.GetRect) && controllerArray[i].GetPadState().IsButtonDown(Buttons.Y))
                                {
                                    currentRoom = Room.Two;
                                    SpawnEnemy();
                                }

                                break;   
                        }
                    }
                    break;     
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

                    switch (currentRoom)
                    {
                        case Room.One:
                            graphicsDevice.Clear(Color.CornflowerBlue);
                            portal.Draw(spriteBatch);
                            break;
                        case Room.Two:
                            graphicsDevice.Clear(Color.IndianRed);
                            portal2.Draw(spriteBatch);
                            portal.Draw(spriteBatch);
                            break;
                        case Room.Three:
                            graphicsDevice.Clear(Color.ForestGreen);
                            portal.Draw(spriteBatch);
                            break;

                    }

                    spriteBatch.Draw(TextureManager.rommTextureList[1], WallTopPos, Color.White);
                    spriteBatch.Draw(TextureManager.rommTextureList[1], WallBottomPos, Color.White);
                    spriteBatch.Draw(TextureManager.rommTextureList[2], WallLeftPos, Color.White);
                    spriteBatch.Draw(TextureManager.rommTextureList[2], WallRightPos, Color.White);

                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        playerArray[i].Draw(spriteBatch);
                    }

                    foreach (var e in enemyList)
                    {
                        e.Draw(spriteBatch);
                    }

                    break;            
            }  
        }
        private void GameWindow(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = windowY = 700;
            graphics.PreferredBackBufferWidth = windowX = 1350;
            graphics.ApplyChanges();
        }
        private void Collision()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (playerArray[i].GetRect.Intersects(WallTopPos))
                {
                    Vector2 tempVector;
                    tempVector = playerArray[i].GetSetPos;
                    tempVector.Y = tempVector.Y + 5;
                    playerArray[i].GetSetPos = tempVector;
                }
                if (playerArray[i].GetRect.Intersects(WallBottomPos))
                {
                    Vector2 tempVector;
                    tempVector = playerArray[i].GetSetPos;
                    tempVector.Y = tempVector.Y - 5;
                    playerArray[i].GetSetPos = tempVector;
                }
                if (playerArray[i].GetRect.Intersects(WallLeftPos))
                {
                    Vector2 tempVector;
                    tempVector = playerArray[i].GetSetPos;
                    tempVector.X = tempVector.X + 5;
                    playerArray[i].GetSetPos = tempVector;
                }
                if (playerArray[i].GetRect.Intersects(WallRightPos))
                {
                    Vector2 tempVector;
                    tempVector = playerArray[i].GetSetPos;
                    tempVector.X = tempVector.X - 5;
                    playerArray[i].GetSetPos = tempVector;
                }
            }
        }
        private void DeleteAbilities()
        {
            Ability toRemoveAbility = null;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var a in playerArray[i].abilityList)
                {
                    if (a.GetRect.Intersects(WallBottomPos) || a.pos.Y > windowY)
                    {
                        toRemoveAbility = a;
                    }
                    else if (a.GetRect.Intersects(WallLeftPos) || a.pos.X < 0)
                    {
                        toRemoveAbility = a;
                    }
                    else if (a.GetRect.Intersects(WallRightPos) || a.pos.X > windowX)
                    {
                        toRemoveAbility = a;
                    }
                    else if (a.GetRect.Intersects(WallTopPos) || a.pos.Y < 0)
                    {
                        toRemoveAbility = a;
                    }
                }

                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var e in enemyList)
                    {
                        if (a.GetRect.Intersects(e.GetRect))
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
                           playerArray[i] = new Player(TextureManager.playerTextureList[0], new Vector2(100 * i + 50, 100), new Rectangle(0, 0, 10, 10), i);


                            playerConnected[i] = true;

                        }
                    }
        }

    }
}

