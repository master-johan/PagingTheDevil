using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Paging_the_devil.GameObject;
using System;

namespace Paging_the_devil
{
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameManager gameManager;

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
            gameManager = new GameManager(GraphicsDevice, graphics, this);
            
            //room = new Room(graphics);
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
          
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            /*
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
            }*/
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