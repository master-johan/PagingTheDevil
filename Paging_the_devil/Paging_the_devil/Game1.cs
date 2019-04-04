using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Paging_the_devil.GameObject;
using System;


namespace Paging_the_devil
{
    enum Roomba { One, Two, Three }
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;




        Roomba currentRoom;
        Gateway portal, portal2;

        int nrOfPlayers;



      


        Vector2 portalPos, portalRoom2, playerPos, playerPos2, portalRoom3, portalRoom4;

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;


        Room room;
        


        GamePadCapabilities[] connectedC;

        Controller[] controllerArray;


        Player[] playerArray;
        
        List<Enemy> enemyList;

        bool[] playerConnected;
        
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

            room = new Room(graphics);
            




            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentRoom = Roomba.One;

            //WallTopPos = new Rectangle(0, 0, windowX, 20);
            //WallBottomPos = new Rectangle(0, windowY - 20, windowX, 20);
            //WallLeftPos = new Rectangle(0, 0, 20, windowY);
            //WallRightPos = new Rectangle(windowX - 20, 0, 20, windowY);

            portalPos = new Vector2(300, 430);
            portalRoom2 = new Vector2(300, -10);
            portalRoom3 = new Vector2(1300, 350);
            portalRoom4 = new Vector2(-10, 350);

            playerPos = new Vector2(200, 400);
            playerPos2 = new Vector2(320, 100);


            portal = new Gateway(TextureManager.roomTextureList[0], portalPos);
            portal2 = new Gateway(TextureManager.roomTextureList[0], portalRoom3);

            


            enemyList = new List<Enemy>();

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

        protected override void UnloadContent()
        {

        }




        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            for (int i = 0; i < controllerArray.Length; i++)
            {
                if (connectedC[i].IsConnected && playerConnected[i] == false)
                {
                    playerArray[i] = new Player(TextureManager.playerTextureList[0], new Vector2(100 * i + 50, 100), new Rectangle(0, 0, 10, 10), i);


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
                if (controllerArray[i].GetDirection() != Vector2.Zero)
                {
                    playerArray[i].LastInputDirection(controllerArray[i].GetDirection());
                }
                playerArray[i].InputDirection(controllerArray[i].GetDirection());
                playerArray[i].InputPadState(controllerArray[i].GetPadState());
            }


            for (int i = 0; i < room.GetWallList().Count; i++)
            {
                for (int j = 0; j < nrOfPlayers; j++)
                {
                    if (playerArray[j].GetBotHitbox.Intersects(room.GetWallList()[i].GetRect))
                    {

                    }
                    else if (playerArray[j].GetTopHitbox.Intersects(room.GetWallList()[i].GetRect))
                    {

                    }
                    else if (playerArray[j].GetLeftHitbox.Intersects(room.GetWallList()[i].GetRect))
                    {

                    }
                    else if (playerArray[j].GetRightHitbox.Intersects(room.GetWallList()[i].GetRect))
                    {

                    }
                }
            }

            switch (currentRoom)
            {
                case Roomba.One:

                    if (playerArray[0].GetRect.Intersects(portal.GetRect) && controllerArray[0].ButtonPressed(Buttons.Y))
                    {

                        //portal.GetSetPos = portalRoom2;
                        currentRoom = Roomba.Two;
                        SpawnEnemy();
                    }

                    break;
                case Roomba.Two:
                    if (playerArray[0].GetRect.Intersects(portal.GetRect) && controllerArray[0].ButtonPressed(Buttons.Y))
                    {
                        portal.GetSetPos = portalPos;
                        currentRoom = Roomba.One;
                    }
                    else if (playerArray[0].GetRect.Intersects(portal2.GetRect) && controllerArray[0].ButtonPressed(Buttons.Y))
                    {
                        portal2.GetSetPos = portalRoom3;
                        currentRoom = Roomba.Three;
                        portal.GetSetPos = portalRoom4;
                        SpawnEnemy();
                    }
                    break;
                case Roomba.Three:
                    if (playerArray[0].GetRect.Intersects(portal.GetRect) && controllerArray[0].ButtonPressed(Buttons.Y))
                    {
                        currentRoom = Roomba.Two;
                        SpawnEnemy();
                    }
                    break;
                default:
                    break;
            }

                    foreach (var e in enemyList)
                    {
                        e.Update();

                    }



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
                case Roomba.One:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    portal.Draw(spriteBatch);
                    break;
                case Roomba.Two:
                    GraphicsDevice.Clear(Color.IndianRed);
                    portal2.Draw(spriteBatch);
                    portal.Draw(spriteBatch);
                    break;
                case Roomba.Three:
                    GraphicsDevice.Clear(Color.ForestGreen);
                    portal.Draw(spriteBatch);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerArray[i].Draw(spriteBatch);
            }

            room.Draw(spriteBatch);

            foreach (var e in enemyList)
            {
                e.Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

       
        private void DeleteAbilities()
        {
            Ability toRemoveAbility = null;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var w in room.GetWallList())
                    {
                        if (a.GetRect.Intersects(w.GetRect) || a.pos.Y > room.WindowY || a.pos.X < 0 || a.pos.X > room.WindowX || a.pos.Y < 0)
                        {
                            toRemoveAbility = a;
                        }
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
            int x = rand.Next((room.WindowX / 2) + 20, room.WindowX - TextureManager.enemyTextureList[0].Width - 20);
            int y = rand.Next(20, room.WindowY - TextureManager.enemyTextureList[0].Height - 20);

            Enemy enemy = new Enemy(TextureManager.enemyTextureList[0], new Vector2(x, y));
            enemyList.Add(enemy);
        }

    }
}
