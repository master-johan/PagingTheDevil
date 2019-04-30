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
        Vector2 roomFloor;
        List<Rectangle> wallRectList = new List<Rectangle>();
        List<Wall> wallTileList = new List<Wall>();
        List<Gateway> gateWayList = new List<Gateway>();
        Color color;
        public bool AllowedRoom { get; set; }
        public bool StartRoom { get; set; }
        bool bossRoom;

        public Room(Color color, bool allowedRoom, bool startRoom, bool bossRoom)
        {
            this.color = color;
            this.AllowedRoom = allowedRoom;
            this.StartRoom = startRoom;
            this.bossRoom = bossRoom;

            roomFloor = new Vector2(0, TextureManager.GameWindowStartY);

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

            spriteBatch.Draw(TextureManager.roomTextureList[3], roomFloor, color);

            if (gateWayList.Count > 0)
            {
                foreach (var g in gateWayList)
                {
                    g.Draw(spriteBatch);
                }
            }
            foreach (var w in wallTileList)
            {
                w.Draw(spriteBatch);
            }

            

        }

        private void DecidePos()
        {
            WallTopPos = new Rectangle(0, TextureManager.GameWindowStartY, WindowX, 32);
            WallBottomPos = new Rectangle(0, WindowY - 32, WindowX, 32);
            WallLeftPos = new Rectangle(0, TextureManager.GameWindowStartY, 32, WindowY);
            WallRightPos = new Rectangle(WindowX - 32, TextureManager.GameWindowStartY, 32, WindowY);
        }

        private void AddToList()
        {
            wallRectList.Add(WallTopPos);
            wallRectList.Add(WallBottomPos);
            wallRectList.Add(WallLeftPos);
            wallRectList.Add(WallRightPos);

            for (int i = 0; i < 30; i++)
            {
                wallTileList.Add(new Wall(TextureManager.roomTextureList[7], Vector2.Zero, new Rectangle(0, i * 32 + TextureManager.GameWindowStartY, 32, 32)));
                wallTileList.Add(new Wall(TextureManager.roomTextureList[6], Vector2.Zero, new Rectangle(TextureManager.WindowSizeX - 32, i * 32 + TextureManager.GameWindowStartY, 32, 32)));
            }
            for (int i = 0; i < 60; i++)
            {
                wallTileList.Add(new Wall(TextureManager.roomTextureList[5], Vector2.Zero, new Rectangle(i * 32, TextureManager.WindowSizeY - 32, 32, 32)));
                wallTileList.Add(new Wall(TextureManager.roomTextureList[4], Vector2.Zero, new Rectangle(i * 32, TextureManager.GameWindowStartY, 32, 32)));
            }
        }

        public List<Rectangle> GetWallRectList()
        {
            return wallRectList;
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
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallRectList[0].Width / 2, wallRectList[0].Y)));
                }
                else if (dir == 1)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallRectList[1].Width / 2, wallRectList[1].Y - 25)));
                }
                else if (dir == 2)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallRectList[2].X, wallRectList[2].Height/2)));
                }
                else if (dir == 3)
                {
                    gateWayList.Add(new Gateway(TextureManager.roomTextureList[0], new Vector2(wallRectList[3].X- 25, wallRectList[3].Height / 2)));
                }
            }
        }

        public List<Gateway> GetGatewayList()
        {
            return gateWayList;
        }
    }
}
