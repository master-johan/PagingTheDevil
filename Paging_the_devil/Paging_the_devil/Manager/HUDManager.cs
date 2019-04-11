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
        GraphicsDevice graphicsDevice;
        int nrOfPlayers;
        HUD[] playerHudArray;
        HUD textBoxHud;
        Vector2 pos;
        
       

        public HUDManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            playerHudArray = new HUD[4];
            
        }

        public void Update()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                pos.X = TextureManager.WindowSizeX / 5 * i ;
                pos.Y = 0;

                if(i > 1)
                {
                    pos.X = TextureManager.WindowSizeX / 5 * (i + 1);
                }

                playerHudArray[i] = new HUD(graphicsDevice, pos);
            }
        }

        public void Draw (SpriteBatch spriteBatch)
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
    }
}
