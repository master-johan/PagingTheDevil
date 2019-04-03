using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil
{
    public class Room
    {
        int windowX, windowY;
        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
        GraphicsDeviceManager graphics;
        Wall wallTop, wallBot, wallLeft, wallRight;
        List<Wall> wallList = new List<Wall>();

        public Room(GraphicsDeviceManager graphics/*, Player[] playerArray*/)
        {
            this.graphics = graphics;

            GameWindow();

            WallTopPos = new Rectangle(0, 0, windowX, 20);
            WallBottomPos = new Rectangle(0, windowY - 20, windowX, 20);
            WallLeftPos = new Rectangle(0, 0, 20, windowY);
            WallRightPos = new Rectangle(windowX - 20, 0, 20, windowY);
                                                
            wallList.Add(wallTop = new Wall(TextureManager.roomTextures[1], Vector2.Zero, WallTopPos));
            wallList.Add(wallBot = new Wall(TextureManager.roomTextures[1], Vector2.Zero, WallBottomPos));
            wallList.Add(wallLeft = new Wall(TextureManager.roomTextures[2], Vector2.Zero, WallLeftPos));
            wallList.Add(wallRight = new Wall(TextureManager.roomTextures[2], Vector2.Zero, WallRightPos));
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < wallList.Count; i++)
            {
                wallList[i].Draw(spriteBatch);
            }
        }
        public void GameWindow()
        {
            graphics.PreferredBackBufferHeight = windowY = 700;
            graphics.PreferredBackBufferWidth = windowX = 1350;
            graphics.ApplyChanges();
        }
        public List<Wall> GetWallList()
        {
            return wallList;
        }
    }
}
