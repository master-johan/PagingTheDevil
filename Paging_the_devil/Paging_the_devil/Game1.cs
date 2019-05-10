using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.Manager;

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
            TextureBank.LoadTextures(Content);
            SoundBank.LoadSound(Content);
            ValueBank.Values();

            gameManager = new GameManager(GraphicsDevice, graphics, this);
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
          
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            gameManager.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            gameManager.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}