using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;


namespace Paging_the_devil.GameObject
{
    public class HUD
    {
        Rectangle heathBar;
        Rectangle btnX;
        Rectangle btnA;
        Rectangle btnY;
        Rectangle btnB;
        Rectangle hudBox;
        Vector2 pos;


        public HUD (GraphicsDevice graphicsDevice, Vector2 pos)
        {
            this.pos = pos;

            hudBox = new Rectangle((int)pos.X, (int)pos.Y, TextureManager.WindowSizeX / 5, TextureManager.WindowSizeY / 8);
            heathBar = new Rectangle(hudBox.X + 12, hudBox.Y + 45, hudBox.Width / 2, hudBox.Height / 3);
            btnX = new Rectangle(hudBox.X + 225, hudBox.Y + 45, hudBox.Width / 9, hudBox.Height / 3);
            btnA = new Rectangle(hudBox.X + 274, hudBox.Y + 70, hudBox.Width / 9, hudBox.Height / 3);
            btnB = new Rectangle(hudBox.X + 323, hudBox.Y + 45, hudBox.Width / 9, hudBox.Height / 3);
            btnY = new Rectangle(hudBox.X + 274, hudBox.Y + 15, hudBox.Width / 9, hudBox.Height / 3);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(TextureManager.hudTextureList[4], hudBox, Color.White);
                spriteBatch.Draw(TextureManager.hudTextureList[1], heathBar, Color.White);
                spriteBatch.Draw(TextureManager.hudTextureList[2], btnX, Color.White);
                spriteBatch.Draw(TextureManager.hudTextureList[0], btnA, Color.White);
                spriteBatch.Draw(TextureManager.hudTextureList[1], btnB, Color.White);
                spriteBatch.Draw(TextureManager.hudTextureList[3], btnY, Color.White);
                
            
        }
    }
}
