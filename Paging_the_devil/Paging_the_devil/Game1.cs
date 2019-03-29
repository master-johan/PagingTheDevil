using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            GameWindow();
            TextureManager.LoadTextures(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            heroPos1 = new Vector2(100, 100);
            heroPos2 = new Vector2(200, 200);

            

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
       

            for (int i = 0; i < noPlayers; i++)
            {
                playerArray[i].Draw(spriteBatch);
            }
            

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
