using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.Manager;

namespace Paging_the_devil
{
    class Room
    {
        public int WindowX { get; private set; }
        public int WindowY { get; private set; }

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
        Wall wallTop, wallBot, wallLeft, wallRight;
        List<Wall> wallList = new List<Wall>();
        Color color;
        public bool AllowedRoom { get; set; }
        public bool StartRoom { get; set; }
        public bool bossRoom { get; set; }

        public Room(Color color, bool allowedRoom, bool startRoom, bool bossRoom)
        {
            this.color = color;
            this.AllowedRoom = allowedRoom;
            this.StartRoom = startRoom;
            this.bossRoom = bossRoom;

            WindowX = TextureManager.WindowSizeX;
            WindowY = TextureManager.WindowSizeY;
            DecidePos();
            AddToList();
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(TextureManager.roomTextureList[3], Vector2.Zero, color);
            for (int i = 0; i < wallList.Count; i++)
            {
                wallList[i].Draw(spriteBatch);
            }
        }

        private void DecidePos()
        {
            WallTopPos = new Rectangle(0, 0, WindowX, 20);
            WallBottomPos = new Rectangle(0, WindowY - 20, WindowX, 20);
            WallLeftPos = new Rectangle(0, 0, 20, WindowY);
            WallRightPos = new Rectangle(WindowX - 20, 0, 20, WindowY);
        }

        private void AddToList()
        {
            wallList.Add(wallTop = new Wall(TextureManager.roomTextureList[1], Vector2.Zero, WallTopPos));
            wallList.Add(wallBot = new Wall(TextureManager.roomTextureList[1], Vector2.Zero, WallBottomPos));
            wallList.Add(wallLeft = new Wall(TextureManager.roomTextureList[2], Vector2.Zero, WallLeftPos));
            wallList.Add(wallRight = new Wall(TextureManager.roomTextureList[2], Vector2.Zero, WallRightPos));
        }
        public List<Wall> GetWallList()
        {
            return wallList;
        }
    }
}
