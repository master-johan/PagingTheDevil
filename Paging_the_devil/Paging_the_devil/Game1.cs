using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Paging_the_devil
{
    enum GameState { RoomOne, RoomTwo}
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameState currentGamestate;
        TextureManager textureManager;
        Player player;
        Portal portal;
        Vector2 portalPos, portalPos2, playerPos, playerPos2;
        Rectangle portalHitbox;

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
            currentGamestate = GameState.RoomOne;


            spriteBatch = new SpriteBatch(GraphicsDevice);

            portalPos = new Vector2(300, 430);
            portalPos2 = new Vector2(300, -10);

            playerPos = new Vector2(200, 400);
            playerPos2 = new Vector2(320, 100);

            textureManager = new TextureManager(Content);

            player = new Player(TextureManager.playerTextures[0], playerPos);
            portal = new Portal(TextureManager.playerTextures[1], portalPos);
                    
           
        }

        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (player.GetRect.Intersects(portal.GetRect))
            {
                portal.SetPos = portalPos2;
                player.SetPos = playerPos2;
            }

            player.Update();
            portal.Update();


            switch (currentGamestate)
            {
                case GameState.RoomOne:

                    break;
                case GameState.RoomTwo:
                   
                    break;
                default:
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            portal.Draw(spriteBatch);
            player.Draw(spriteBatch);

            switch (currentGamestate)
            {
                case GameState.RoomOne:

                    break;
                case GameState.RoomTwo:

                    //spriteBatch.Draw(TextureManager.playerTextures[1], Vector2.Zero, Color.Red);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
