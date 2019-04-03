using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Paging_the_devil.GameObject;
using System.Threading;

namespace Paging_the_devil
{
    enum Roomba { One, Two, Three }
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int windowX, windowY;



        Roomba currentRoom;
        Gateway portal, portal2;
        Vector2 portalPos, portalRoom2, playerPos, playerPos2, portalRoom3, portalRoom4;
        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
        GamePadCapabilities[] connectedC;
        Controller[] controllerArray;
        Player[] playerArray;
        List<Controller> controllerList;
        List<Player> playerList;
        bool[] playerConnected;
        int noPlayers;
        Room room;
        


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

            portal = new Gateway(TextureManager.roomTextures[0], portalPos);
            portal2 = new Gateway(TextureManager.roomTextures[0], portalRoom3);

            playerList = new List<Player>();
            playerArray = new Player[4];

            connectedC = new GamePadCapabilities[4] { GamePad.GetCapabilities(PlayerIndex.One), GamePad.GetCapabilities(PlayerIndex.Two), GamePad.GetCapabilities(PlayerIndex.Three), GamePad.GetCapabilities(PlayerIndex.Four) };
            controllerArray = new Controller[4];
            controllerList = new List<Controller>();
            playerConnected = new bool[4];

            for (int i = 0; i < connectedC.Length; i++)
            {
                if (connectedC[i].IsConnected)
                {
                    PlayerIndex index = (PlayerIndex)i;

                    controllerArray[i] = new Controller(index);

                    noPlayers++;
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

            for (int i = 0; i < noPlayers; i++)
            {
                controllerArray[i].Update();
                playerArray[i].Update();
            }

            for (int i = 0; i < noPlayers; i++)
            {
                playerArray[i].InputDirection(controllerArray[i].GetDirection());
                playerArray[i].InputPadState(controllerArray[i].GetPadState());
            }

            for (int i = 0; i < room.GetWallList().Count; i++)
            {
                for (int j = 0; j < noPlayers; j++)
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
                    }
                    break;
                case Roomba.Three:
                    if (playerArray[0].GetRect.Intersects(portal.GetRect) && controllerArray[0].ButtonPressed(Buttons.Y))
                    {
                        currentRoom = Roomba.Two;
                    }
                    break;
                default:
                    break;
            }

            Collision();

            portal.Update();
            portal2.Update();
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

            room.Draw(spriteBatch);

            for (int i = 0; i < noPlayers; i++)
            {
                playerArray[i].Draw(spriteBatch);
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
            for (int i = 0; i < noPlayers; i++)
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

    }
}
