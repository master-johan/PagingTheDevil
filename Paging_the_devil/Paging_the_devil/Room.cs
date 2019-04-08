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
    class Room
    {
        public int WindowX { get; private set; }
        public int WindowY { get; private set; }

        Rectangle WallTopPos, WallLeftPos, WallRightPos, WallBottomPos;
        Wall wallTop, wallBot, wallLeft, wallRight;
        List<Wall> wallList = new List<Wall>();
        List<Gateway> gateWayList = new List<Gateway>();

        public Room(/*, Player[] playerArray*/)
        {
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

            spriteBatch.Draw(TextureManager.roomTextureList[3], Vector2.Zero, Color.White);
            for (int i = 0; i < wallList.Count; i++)
            {
                wallList[i].Draw(spriteBatch);
            }

            if (gateWayList.Count > 0)
            {
                foreach (var g in gateWayList)
                {
                    g.Draw(spriteBatch);
                }
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

        /// <summary>
        /// dir reprecents direction. 0 = North, 1= South, 2 = West, 3 = East.
        /// </summary>
        /// <param name="dir"></param>
        public void CreateGateWays(int dir)
        {
            if (dir >=0 && dir <=3)
            {
                if (dir == 0)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallList[0].GetRect.Width / 2, wallList[0].pos.Y)));
                }
                else if (dir == 1)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallList[1].GetRect.Width / 2, wallList[1].pos.Y)));
                }
                else if (dir == 2)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallList[2].pos.X, wallList[2].GetRect.Height/2)));
                }
                else if (dir == 3)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallList[3].pos.X, wallList[3].GetRect.Height / 2)));
                }
            }
        }

        public List<Gateway> GetGatewayList()
        {
            return gateWayList;
        }
    }
}
