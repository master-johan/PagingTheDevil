using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject;

namespace Paging_the_devil.Manager
{
    class HUDManager
    {

        int nrOfPlayers;
        public HUD[] playerHudArray { get; set; }
        HUD textBoxHud;
        Vector2 pos;
        Player[] playerArray;




        public HUDManager(Player[] playerArray, int nrOfPlayers)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;
            playerHudArray = new HUD[4];

            CreateHUDs();
        }

        public void Update()
        {
            //for (int i = 0; i < nrOfPlayers; i++)
            //{
            //    pos.X = TextureManager.WindowSizeX / 5 * i ;
            //    pos.Y = 0;

            //    if(i > 1)
            //    {
            //        pos.X = TextureManager.WindowSizeX / 5 * (i + 1);
            //    }

            //    playerHudArray[i] = new HUD(graphicsDevice, pos,p);
            //}

            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerHudArray[i].Update();
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerHudArray[i].Draw(spriteBatch);
            }

        }

        public void GetNrOfPlayersToHud(int nrOfPlayers)
        {
            this.nrOfPlayers = nrOfPlayers;
        }

        private void CreateHUDs()
        {

            for (int i = 0; i < nrOfPlayers; i++)
            {
                pos = new Vector2(TextureManager.WindowSizeX / 5 * i, 0);

                if (i > 1)
                {
                    pos.X = TextureManager.WindowSizeX / 5 * (i + 1);
                }

                playerHudArray[i] = new HUD(pos, playerArray[i]);
            }

        }
    }
}
