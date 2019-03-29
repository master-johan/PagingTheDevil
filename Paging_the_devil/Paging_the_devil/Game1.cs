using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Paging_the_devil
{
    enum Room { One, Two, Three}
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int windowX, windowY;

        KeyboardState keyboardState, oldKeyboardState;

        Room currentRoom;
        Player player;
        Portal portal, portal2;
        Vector2 portalPos, portalRoom2, playerPos, playerPos2, portalRoom3, portalRoom4;
        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
        Rectangle portalHitbox;
        GamePadCapabilities[] connectedC;
        Controller[] controllerArray;
        Player[] playerArray;
        List<Controller> controllerList;
        List<Player> playerList;
        bool[] playerConnected;
        int noPlayers;



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


            player = new Player(TextureManager.playerTextures[0], playerPos);
            portal = new Portal(TextureManager.roomTextures[0], portalPos);
            portal2 = new Portal(TextureManager.roomTextures[0], portalRoom3);


        }

        protected override void UnloadContent()
        {





            

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

     

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update();


            for (int i = 0; i < controllerArray.Length; i++)
            {
                if (connectedC[i].IsConnected && playerConnected[i] == false)
                {
                    playerArray[i] = new Player(TextureManager.playerTextureList[0], new Vector2(100*i+50,100), new Rectangle(0, 0, 60, 280), new Rectangle(0,0,10,10), i);
                    

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

            switch (currentRoom)
            {
                case Room.One:
                    
                    if (player.GetRect.Intersects(portal.GetRect) && oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
                    {
                       
                        //portal.GetSetPos = portalRoom2;
                        currentRoom = Room.Two;
                    }

                    break;
                case Room.Two:
                    if (player.GetRect.Intersects(portal.GetRect) && oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
                    {
                        portal.GetSetPos = portalPos;
                        currentRoom = Room.One;
                    }
                    else if(player.GetRect.Intersects(portal2.GetRect) && oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
                    {
                        portal2.GetSetPos = portalRoom3;
                        currentRoom = Room.Three;
                        portal.GetSetPos = portalRoom4;
                    }
                    break;
                case Room.Three:
                    if (player.GetRect.Intersects(portal.GetRect) && oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
                    {
                        
                        currentRoom = Room.Two;
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
            
         

            spriteBatch.Draw(TextureManager.roomTextures[1], WallTopPos, Color.White);
            spriteBatch.Draw(TextureManager.roomTextures[1], WallBottomPos, Color.White);
            spriteBatch.Draw(TextureManager.roomTextures[2], WallLeftPos, Color.White);
            spriteBatch.Draw(TextureManager.roomTextures[2], WallRightPos, Color.White);



       

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
            if (player.GetRect.Intersects(WallTopPos))
            {
                Vector2 tempVector;
                tempVector = player.GetSetPos;
                tempVector.Y = tempVector.Y + 5;
                player.GetSetPos = tempVector;
            }
            if (player.GetRect.Intersects(WallBottomPos))
            {
                Vector2 tempVector;
                tempVector = player.GetSetPos;
                tempVector.Y = tempVector.Y - 5;
                player.GetSetPos = tempVector;
            }
            if (player.GetRect.Intersects(WallLeftPos))
            {
                Vector2 tempVector;
                tempVector = player.GetSetPos;
                tempVector.X = tempVector.X + 5;
                player.GetSetPos = tempVector;
            }
            if (player.GetRect.Intersects(WallRightPos))
            {
                Vector2 tempVector;
                tempVector = player.GetSetPos;
                tempVector.X = tempVector.X - 5;
                player.GetSetPos = tempVector;
            }
        }
        

    }
}
