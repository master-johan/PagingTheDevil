using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.GameObject;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.Manager
{
    class HUDManager
    {
        int nrOfPlayers;

        Vector2 pos;

        Rectangle hudBackground;

        Player[] playerArray;

        public HUD[] playerHudArray { get; set; }

        public HUDManager(Player[] playerArray, int nrOfPlayers)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;
            playerHudArray = new HUD[4];
            hudBackground = new Rectangle(0, 0, ValueBank.WindowSizeX, ValueBank.WindowSizeY / 8);
            CreateHUDs();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerHudArray[i].Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureBank.menuTextureList[3], hudBackground, Color.Black);

            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerHudArray[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Den här metoden hämtar antalet spelare till till HUD:en
        /// </summary>
        /// <param name="nrOfPlayers"></param>
        public void GetNrOfPlayersToHud(int nrOfPlayers)
        {
            this.nrOfPlayers = nrOfPlayers;
        }
        /// <summary>
        /// Den här metoden skapar HUD:s
        /// </summary>
        private void CreateHUDs()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                pos = new Vector2(ValueBank.WindowSizeX / 5 * i, 0);

                if (i > 1)
                {
                    pos.X = ValueBank.WindowSizeX / 5 * (i + 1);
                }

                playerHudArray[i] = new HUD(pos, playerArray[i]);
            }
        }
        private void CreateHUDBackground()
        {
          
        }
    }
}
