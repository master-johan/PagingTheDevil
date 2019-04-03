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
        public int WindowX { get; private set; }
        public int WindowY { get; private set; }

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
        GraphicsDeviceManager graphics;
        Wall wallTop, wallBot, wallLeft, wallRight;
        List<Wall> wallList = new List<Wall>();

        public Room(GraphicsDeviceManager graphics/*, Player[] playerArray*/)
        {
            this.graphics = graphics;

            GameWindow();

            WallTopPos = new Rectangle(0, 0, WindowX, 20);
            WallBottomPos = new Rectangle(0, WindowY - 20, WindowX, 20);
            WallLeftPos = new Rectangle(0, 0, 20, WindowY);
            WallRightPos = new Rectangle(WindowX - 20, 0, 20, WindowY);
                                                
            wallList.Add(wallTop = new Wall(TextureManager.roomTextureList[1], Vector2.Zero, WallTopPos));
            wallList.Add(wallBot = new Wall(TextureManager.roomTextureList[1], Vector2.Zero, WallBottomPos));
            wallList.Add(wallLeft = new Wall(TextureManager.roomTextureList[2], Vector2.Zero, WallLeftPos));
            wallList.Add(wallRight = new Wall(TextureManager.roomTextureList[2], Vector2.Zero, WallRightPos));
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
            graphics.PreferredBackBufferHeight = WindowY = 700;
            graphics.PreferredBackBufferWidth = WindowX = 1350;
            graphics.ApplyChanges();
        }
        public List<Wall> GetWallList()
        {
            return wallList;
        }
    }
}
