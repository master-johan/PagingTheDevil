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
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
          
        }
        protected override void Update(GameTime gameTime)
        {
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
