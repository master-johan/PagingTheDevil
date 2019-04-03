using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Paging_the_devil.GameObject;
using System;

namespace Paging_the_devil
{
    enum Room { One, Two, Three}
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int windowX, windowY;
        int nrOfPlayers;

        Room currentRoom;

        Portal portal, portal2;

        Vector2 portalPos, portalRoom2, playerPos, playerPos2, portalRoom3, portalRoom4;

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;

        GamePadCapabilities[] connectedC;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Controller> controllerList;
        List<Enemy> enemyList;

        bool[] playerConnected;

        // test commit
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            TextureManager.LoadTextures(Content);
            GameWindow();
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            for (int i = 0; i < controllerArray.Length; i++)
            {
                if (connectedC[i].IsConnected && playerConnected[i] == false)
                {
                    playerArray[i] = new Player(TextureManager.playerTextureList[0], new Vector2(100*i+50,100), new Rectangle(0,0,10,10), i);
                    

                    playerConnected[i] = true;

                }
            }

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
            for (int i = 0; i < nrOfPlayers; i++)
            {
                switch (currentRoom)
                {
                    case Room.One:
                        if (playerArray[i].GetRect.Intersects(portal.GetRect) && controllerArray[i].GetPadState().IsButtonDown(Buttons.Y))
                        {
                            //portal.GetSetPos = portalRoom2;
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
                    default:
                        break;
                }
            }

            foreach (var e in enemyList)
            {
                e.Update();
            }

            Collision();

            portal.Update();
            portal2.Update();

            DeleteAbilities();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (currentRoom)
            {
                case Room.One:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    portal.Draw(spriteBatch);
                    break;
                case Room.Two:
                    GraphicsDevice.Clear(Color.IndianRed);
                    portal2.Draw(spriteBatch);
                    portal.Draw(spriteBatch);
                    break;
                case Room.Three:
                    GraphicsDevice.Clear(Color.ForestGreen);
                    portal.Draw(spriteBatch);
                    break;
                default:
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

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void GameWindow()
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
    }
}
