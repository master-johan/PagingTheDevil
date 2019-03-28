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
        int windowX, windowY;

        GameState currentGamestate;
        TextureManager textureManager;
        Player player;
        Portal portal;
        Vector2 portalPos, portalPos2, playerPos, playerPos2;
        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
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

            GameWindow();

            WallTopPos = new Rectangle(0, 0, windowX, 20);
            WallBottomPos = new Rectangle(0, windowY - 20, windowX, 20);
            WallLeftPos = new Rectangle(0, 0, 20, windowY);
            WallRightPos = new Rectangle(windowX - 20, 0, 20, windowY);

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
            player.Update();

            if (player.GetRect.Intersects(portal.GetRect))
            {
                portal.GetSetPos = portalPos2;
                player.GetSetPos = playerPos2;
            }

            Collision();

            portal.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            portal.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.playerTextures[2], WallTopPos, Color.White);
            spriteBatch.Draw(TextureManager.playerTextures[2], WallBottomPos, Color.White);
            spriteBatch.Draw(TextureManager.playerTextures[3], WallLeftPos, Color.White);
            spriteBatch.Draw(TextureManager.playerTextures[3], WallRightPos, Color.White);



            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void GameWindow()
        {
            graphics.PreferredBackBufferHeight = windowY = 500;
            graphics.PreferredBackBufferWidth = windowX = 1000;
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
