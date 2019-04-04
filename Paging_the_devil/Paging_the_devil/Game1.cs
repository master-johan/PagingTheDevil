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

        GameManager gameManager;

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
            gameManager = new GameManager(GraphicsDevice, graphics, this);
            

            room = new Room(graphics);
            




            spriteBatch = new SpriteBatch(GraphicsDevice);
          
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


            gameManager.Update(gameTime);
        }
        


        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            gameManager.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
