using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Paging_the_devil.GameObject;

namespace Paging_the_devil
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 heroPos1;
        Vector2 heroPos2;
        TextureManager textureManager;
        Player player1;
        Player player2;

        private List<Player> playerList;
        //Controller controller;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            GameWindow();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureManager = new TextureManager(Content);
            heroPos1 = new Vector2(100, 100);
            heroPos2 = new Vector2(200, 200);
          
            player1 = new Player(TextureManager.playerTextureList[0], heroPos1, new Rectangle(0, 0, 60, 280), new Rectangle(0,0,10,10) /*PlayerState.One*/);
            player2 = new Player(TextureManager.playerTextureList[0], heroPos2, new Rectangle(0, 0, 60, 280), new Rectangle(0,0,10,10) /*PlayerState.Two*/);

            //playerList = new List<Player>()
            //{
                
            //}

        }

     

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //GamePadCapabilities c = GamePad.GetCapabilities(PlayerIndex.One);
            // GamePadState gamePadState;

            // TODO: Add your update logic here
            //if (player1.currentPlayer == PlayerState.One)
                player1.Update();


            //if (player2.currentPlayer == PlayerState.Two)
                player2.Update2();
            





            //if (c.IsConnected)
            //{
            //    GamePadState state = GamePad.GetState(PlayerIndex.One);

            //    if (c.HasLeftXThumbStick)
            //    {
            //        heroPos.X += state.ThumbSticks.Left.X * 10.0f;
            //        heroPos.Y += state.ThumbSticks.Left.Y * 10.0f;
            //    }

            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //if (player1.playerOne)
            //{
            //    player1.Draw(spriteBatch);
            //}

            //if (player2.playerTwo)
            //{
            //    player2.Draw(spriteBatch);
            //}

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void GameWindow()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
        }
    }
}
