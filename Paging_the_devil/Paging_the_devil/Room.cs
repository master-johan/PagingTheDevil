using System.Collections.Generic;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paging_the_devil.Manager;

namespace Paging_the_devil
{
    class Room
    {
        Rectangle wallTopPos;
        Rectangle wallLeftPos;
        Rectangle wallRightPos;
        Rectangle wallBottomPos;

        Vector2 roomFloor;

        List<Rectangle> wallRectList = new List<Rectangle>();

        List<Wall> wallTileList = new List<Wall>();

        List<Gateway> gateWayList = new List<Gateway>();

        Color color;

        bool bossRoom;

        public bool AllowedRoom { get; set; }
        public bool StartRoom { get; set; }

        public Room(Color color, bool AllowedRoom, bool StartRoom, bool bossRoom)
        {
            this.color = color;
            this.AllowedRoom = AllowedRoom;
            this.StartRoom = StartRoom;
            this.bossRoom = bossRoom;

            roomFloor = new Vector2(0, ValueBank.GameWindowStartY);

            DecidePos();
            AddToList();
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureBank.roomTextureList[3], roomFloor, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
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

            //spriteBatch.Draw(TextureBank.roomTextureList[1], wallTopPos, Color.Black);
            //spriteBatch.Draw(TextureBank.roomTextureList[1], wallBottomPos, Color.Red);
            //spriteBatch.Draw(TextureBank.roomTextureList[1], wallLeftPos, Color.Yellow);
            //spriteBatch.Draw(TextureBank.roomTextureList[1], wallRightPos, Color.Blue);
        }

        private void DecidePos()
        {
            wallTopPos = new Rectangle(0, ValueBank.GameWindowStartY, ValueBank.WindowSizeX, 32);
            wallBottomPos = new Rectangle(0, ValueBank.WindowSizeY - 32, ValueBank.WindowSizeX, 32);
            wallLeftPos = new Rectangle(0, ValueBank.GameWindowStartY, 32, ValueBank.WindowSizeY);
            wallRightPos = new Rectangle(ValueBank.WindowSizeX - 32, ValueBank.GameWindowStartY, 32, ValueBank.WindowSizeY);
        }

        private void AddToList()
        {
            wallRectList.Add(wallTopPos);
            wallRectList.Add(wallBottomPos);
            wallRectList.Add(wallLeftPos);
            wallRectList.Add(wallRightPos);

            for (int i = 0; i < 30; i++)
            {
                wallTileList.Add(new Wall(TextureBank.roomTextureList[7], Vector2.Zero, new Rectangle(0, i * 32 + ValueBank.GameWindowStartY, 32, 32)));
                wallTileList.Add(new Wall(TextureBank.roomTextureList[6], Vector2.Zero, new Rectangle(ValueBank.WindowSizeX - 32, i * 32 + ValueBank.GameWindowStartY, 32, 32)));
            }
            for (int i = 0; i < 60; i++)
            {
                wallTileList.Add(new Wall(TextureBank.roomTextureList[5], Vector2.Zero, new Rectangle(i * 32, ValueBank.WindowSizeY - 32, 32, 32)));
                wallTileList.Add(new Wall(TextureBank.roomTextureList[4], Vector2.Zero, new Rectangle(i * 32, ValueBank.GameWindowStartY, 32, 32)));
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
                    gateWayList.Add(new Gateway(TextureBank.roomTextureList[0], new Vector2(wallRectList[0].Width / 2, wallRectList[0].Y)));
                }
                else if (dir == 1)
                {
                    gateWayList.Add(new Gateway(TextureBank.roomTextureList[0], new Vector2(wallRectList[1].Width / 2, wallRectList[1].Y - 25)));
                }
                else if (dir == 2)
                {
                    gateWayList.Add(new Gateway(TextureBank.roomTextureList[0], new Vector2(wallRectList[2].X, wallRectList[2].Height/2)));
                }
                else if (dir == 3)
                {
                    gateWayList.Add(new Gateway(TextureBank.roomTextureList[0], new Vector2(wallRectList[3].X- 25, wallRectList[3].Height / 2)));
                }
            }
        }
        public List<Gateway> GetGatewayList()
        {
            return gateWayList;
        }
    }
}
