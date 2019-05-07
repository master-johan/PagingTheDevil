using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.GameObject;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.GameObject.Characters;

namespace Paging_the_devil.Manager
{
    class RoomManager
    {
        Player[] playerArray;

        Room[,] currentLevel;

        List<Enemy> enemyList;

        LevelManager levelManager;

        Gateway gatewayNorth;
        Gateway gatewaySouth;
        Gateway gatewayEast;
        Gateway gatewayWest;

        int RoomCoordinateX;
        int RoomCoordinateY;
        int nrOfPlayers;

        bool[,] enemiesSpawned;

        public Room CurrentRoom { get; set; }

        public RoomManager(Player[] playerArray, int nrOfPlayers, List<Enemy> enemyList, LevelManager levelManager)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;
            this.enemyList = enemyList;
            this.levelManager = levelManager;

            currentLevel = levelManager.CurrentLevel;
            GetStaringRoom();
            DeclareGateways();
            ShowGateways();
            enemiesSpawned = new bool[5, 5];
        }

        public void Update()
        {
            CollisionWithWall();
            GoIntoGateway();
            AddEnemiesToRoom();
            enemyCollisionWithWalls();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentRoom.Draw(spriteBatch);
            if (gatewayEast.IsVisible)
            {
                gatewayEast.Draw(spriteBatch);
            }

            if (gatewayNorth.IsVisible)
            {
                gatewayNorth.Draw(spriteBatch);
            }

            if (gatewaySouth.IsVisible)
            {
                gatewaySouth.Draw(spriteBatch);
            }

            if (gatewayWest.IsVisible)
            {
                gatewayWest.Draw(spriteBatch);
            }
        }
        /// <summary>
        /// Den här metoden hanterar kollisionen mellan spelare och väggar.
        /// </summary>
        private void CollisionWithWall()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                bool[,] boolArray = new bool[4, CurrentRoom.GetWallRectList().Count];

                for (int j = 0; j < CurrentRoom.GetWallRectList().Count; j++)
                {
                    if (playerArray[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[0, j] = true;
                    }

                    else if (playerArray[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[1, j] = true;
                    }

                    else if (playerArray[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[2, j] = true;
                    }

                    else if (playerArray[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[3, j] = true;
                    }
                }

                bool[] blockedDirections = new bool[4];

                for (int y = 0; y < boolArray.GetLength(0); y++)
                {
                    bool isBlocked = false;
                    for (int x = 0; x < boolArray.GetLength(1); x++)
                    {
                        if (boolArray[x, y])
                        {
                            isBlocked = true;
                            break;
                        }
                    }
                    blockedDirections[y] = isBlocked;
                }


                if (blockedDirections[0] == true)
                {
                    playerArray[i].UpMovementBlocked = true;
                }
                else
                {
                    playerArray[i].UpMovementBlocked = false;
                }

                if (blockedDirections[1] == true)
                {
                    playerArray[i].DownMovementBlocked = true;
                }
                else
                {
                    playerArray[i].DownMovementBlocked = false;
                }

                if (blockedDirections[2] == true)
                {
                    playerArray[i].LeftMovementBlocked = true;
                }
                else
                {
                    playerArray[i].LeftMovementBlocked = false;
                }

                if (blockedDirections[3] == true)
                {
                    playerArray[i].RightMovementBlocked = true;
                }
                else
                {
                    playerArray[i].RightMovementBlocked = false;
                }

            }
        }
        /// <summary>
        /// Den här metoden hanterar kollisionen mellan fiende och väggar.
        /// </summary>
        private void enemyCollisionWithWalls()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                bool[,] boolArray = new bool[4, CurrentRoom.GetWallRectList().Count];

                for (int j = 0; j < CurrentRoom.GetWallRectList().Count; j++)
                {
                    if (enemyList[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[0, j] = true;
                    }

                    else if (enemyList[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[1, j] = true;
                    }

                    else if (enemyList[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[2, j] = true;
                    }

                    else if (enemyList[i].GetRect.Intersects(CurrentRoom.GetWallRectList()[j]))
                    {
                        boolArray[3, j] = true;
                    }
                }

                bool[] blockedDirections = new bool[4];

                for (int y = 0; y < boolArray.GetLength(0); y++)
                {
                    bool isBlocked = false;
                    for (int x = 0; x < boolArray.GetLength(1); x++)
                    {
                        if (boolArray[x, y])
                        {
                            isBlocked = true;
                            break;
                        }
                    }
                    blockedDirections[y] = isBlocked;
                }

                if (blockedDirections[0] == true)
                {
                    enemyList[i].UpMovementBlocked = true;
                }

                else
                {
                    enemyList[i].UpMovementBlocked = false;
                }

                if (blockedDirections[1] == true)
                {
                    enemyList[i].DownMovementBlocked = true;
                }

                else
                {
                    enemyList[i].DownMovementBlocked = false;
                }

                if (blockedDirections[2] == true)
                {
                    enemyList[i].LeftMovementBlocked = true;
                }

                else
                {
                    enemyList[i].LeftMovementBlocked = false;
                }

                if (blockedDirections[3] == true)
                {
                    enemyList[i].RightMovementBlocked = true;
                }

                else
                {
                    enemyList[i].RightMovementBlocked = false;
                }
            }
        }
        /// <summary>
        /// Den här metoden hämtar vilket rum som är startrummet.
        /// </summary>
        private void GetStaringRoom()
        {
            for (int y = 0; y < currentLevel.GetLength(1); y++)
            {
                for (int x = 0; x < currentLevel.GetLength(0); x++)
                {
                    if (currentLevel[x, y].StartRoom)
                    {
                        CurrentRoom = currentLevel[x, y];
                        RoomCoordinateX = x;
                        RoomCoordinateY = y;
                    }
                }
            }
        }
        /// <summary>
        /// Den här metoden deklarerar var positionen på gatewaysen ska vara.
        /// </summary>
        private void DeclareGateways()
        {
            int halfPortalSize = TextureBank.roomTextureList[0].Height / 2;

            Vector2 north = new Vector2(CurrentRoom.GetWallRectList()[0].Width / 2 - halfPortalSize, CurrentRoom.GetWallRectList()[0].Y);
            Vector2 south = new Vector2(CurrentRoom.GetWallRectList()[1].Width / 2 - halfPortalSize, CurrentRoom.GetWallRectList()[1].Y - halfPortalSize);
            Vector2 west = new Vector2(CurrentRoom.GetWallRectList()[2].X , CurrentRoom.GetWallRectList()[2].Height/ 2);
            Vector2 east = new Vector2(CurrentRoom.GetWallRectList()[3].X - halfPortalSize, CurrentRoom.GetWallRectList()[3].Height / 2);

            gatewayNorth = new Gateway(TextureBank.roomTextureList[0], north);
            gatewaySouth = new Gateway(TextureBank.roomTextureList[0], south);
            gatewayWest = new Gateway(TextureBank.roomTextureList[0], west);
            gatewayEast = new Gateway(TextureBank.roomTextureList[0], east);
        }
        /// <summary>
        /// Den här metoden räknar på vilken gateway som ska visas.
        /// </summary>
        private void ShowGateways()
        {
            //Ifall man står i x0 koordinat.
            if (RoomCoordinateX == 0)
            {
                //Ifall man står i y0 och x0 koordinat.
                if (RoomCoordinateY == 0)
                {
                    if (currentLevel[RoomCoordinateX,RoomCoordinateY + 1].AllowedRoom)
                    {
                        gatewaySouth.IsVisible = true;
                    }

                    else
                    {
                        gatewaySouth.IsVisible = false;
                    }
                }
                //ifall man står i x0 och y4 koordnat.
                else if (RoomCoordinateY == 4)
                {
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY - 1].AllowedRoom)
                    {
                        gatewayNorth.IsVisible = true;
                    }

                    else
                    {
                        gatewayNorth.IsVisible = false;
                    }
                }
                //Ifall man står i x0 och y1,y2,y3 koordinat.
                else
                {
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY + 1].AllowedRoom)
                    {
                        gatewaySouth.IsVisible = true;
                    }

                    else
                    {
                        gatewaySouth.IsVisible = false;
                    }

                    if (currentLevel[RoomCoordinateX, RoomCoordinateY - 1].AllowedRoom)
                    {
                        gatewayNorth.IsVisible = true;
                    }

                    else
                    {
                        gatewayNorth.IsVisible = false;
                    }
                }
                if (currentLevel[RoomCoordinateX + 1, RoomCoordinateY].AllowedRoom)
                {
                    gatewayEast.IsVisible = true;
                }

                else
                {
                    gatewayEast.IsVisible = false;
                }
            }
            //Ifall man står i x4 koordinat.
            else if (RoomCoordinateX == 4)
            {
                //Ifall man står i x4 och y0 koordinat.
                if (RoomCoordinateY == 0)
                {
                    if (currentLevel[RoomCoordinateX,RoomCoordinateY + 1].AllowedRoom)
                    {
                        gatewaySouth.IsVisible = true;
                    }

                    else
                    {
                        gatewaySouth.IsVisible = false;
                    }
                }
                //Ifall man står i x4 och y4 koordinat.
                else if (RoomCoordinateY == 4)
                {
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY - 1].AllowedRoom)
                    {
                        gatewayNorth.IsVisible = true;
                    }

                    else
                    {
                        gatewayNorth.IsVisible = false;
                    }
                }
                //Ifall man står i x4 och y1,y2,y3 koordinat.
                else
                {
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY - 1].AllowedRoom)
                    {
                        gatewayNorth.IsVisible = true;
                    }

                    else
                    {
                        gatewayNorth.IsVisible = false;
                    }

                    if (currentLevel[RoomCoordinateX, RoomCoordinateY + 1].AllowedRoom)
                    {
                        gatewaySouth.IsVisible = true;
                    }

                    else
                    {
                        gatewaySouth.IsVisible = false;
                    }
                }
                if (currentLevel[RoomCoordinateX - 1, RoomCoordinateY].AllowedRoom)
                {
                    gatewayWest.IsVisible = true;
                }

                else
                {
                    gatewayWest.IsVisible = false;
                }
            }
            //Ifall man står i x1,x2,x3 koordinat.
            else
            {
                //Ifall man står i x1,x2,x3 och y0 koordinat.
                if (RoomCoordinateY == 0)
                {
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY + 1].AllowedRoom)
                    {
                        gatewaySouth.IsVisible = true;
                    }

                    else
                    {
                        gatewaySouth.IsVisible = false;
                    }
                }
                //Ifall man står i x1,x2,x3 och y4 koordinat.
                else if (RoomCoordinateY == 4)
                {
                    if (currentLevel[RoomCoordinateX,RoomCoordinateY - 1].AllowedRoom)
                    {
                        gatewayNorth.IsVisible = true;
                    }

                    else
                    {
                        gatewayNorth.IsVisible = false;
                    }
                }

                else
                {
                    //Ifall man står i x1,x2,x3 och y1,y2,y3 koordinat
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY - 1].AllowedRoom)
                    {
                        gatewayNorth.IsVisible = true;
                    }

                    else
                    {
                        gatewayNorth.IsVisible = false;
                    }

                    if (currentLevel[RoomCoordinateX, RoomCoordinateY + 1].AllowedRoom)
                    {
                        gatewaySouth.IsVisible = true;
                    }

                    else
                    {
                        gatewaySouth.IsVisible = false;
                    }
                }

                if (currentLevel[RoomCoordinateX - 1, RoomCoordinateY].AllowedRoom)
                {
                    gatewayWest.IsVisible = true;
                }

                else
                {
                    gatewayWest.IsVisible = false;
                }

                if (currentLevel[RoomCoordinateX + 1, RoomCoordinateY].AllowedRoom)
                {
                    gatewayEast.IsVisible = true;
                }

                else
                {
                    gatewayEast.IsVisible = false;
                }
            }
            //Ifall man står i ytterkanten ska vissa gateways inte visas.
            if (RoomCoordinateX == 0)
            {
                gatewayWest.IsVisible = false;
            }

            else if (RoomCoordinateX == 4)
            {
                gatewayEast.IsVisible = false;
            }

            if (RoomCoordinateY == 0)
            {
                gatewayNorth.IsVisible = false;
            }

            else if (RoomCoordinateY == 4)
            {
                gatewaySouth.IsVisible = false;
            }
        }
        /// <summary>
        /// Den här metoden sköter vilken gateway man går in i.
        /// </summary>
        private void GoIntoGateway()
        {
            int tempX = RoomCoordinateX;
            int tempY = RoomCoordinateY;
            Vector2 temp = Vector2.Zero;

            if (playerArray[0].GetRect.Intersects(gatewaySouth.GetRect) && gatewaySouth.IsVisible)
            {
                if (playerArray[0].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0)
                {
                    RoomCoordinateY += 1;
                    temp = new Vector2(gatewayNorth.pos.X + TextureBank.roomTextureList[0].Width / 2, gatewayNorth.pos.Y + TextureBank.roomTextureList[0].Height / 2 + 25);
                }
            }

            else if (playerArray[0].GetRect.Intersects(gatewayNorth.GetRect) && gatewayNorth.IsVisible)
            {
                if (playerArray[0].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0)
                {
                    RoomCoordinateY -= 1;
                    temp = new Vector2(gatewaySouth.pos.X + TextureBank.roomTextureList[0].Width / 2, gatewaySouth.pos.Y + TextureBank.roomTextureList[0].Height / 2 - 25);
                }
            }

            else if (playerArray[0].GetRect.Intersects(gatewayEast.GetRect) && gatewayEast.IsVisible)
            {
                if (playerArray[0].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0)
                {
                    RoomCoordinateX += 1;
                    temp = new Vector2(gatewayWest.pos.X + TextureBank.roomTextureList[0].Width / 2 + 25, gatewayWest.pos.Y + TextureBank.roomTextureList[0].Height / 2);
                }
            }

            else if (playerArray[0].GetRect.Intersects(gatewayWest.GetRect) && gatewayWest.IsVisible)
            {
                if (playerArray[0].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0)
                {
                    RoomCoordinateX -= 1;
                    temp = new Vector2(gatewayEast.pos.X + TextureBank.roomTextureList[0].Width / 2 - 25, gatewayEast.pos.Y + TextureBank.roomTextureList[0].Height / 2);
                }
            }

            if (RoomCoordinateX != tempX || RoomCoordinateY != tempY)
            {
                CurrentRoom = currentLevel[RoomCoordinateX, RoomCoordinateY];
                ShowGateways();
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    playerArray[i].pos = temp;
                    SoundBank.SoundEffectList[6].Play();
                }

                for (int i = 0; i < nrOfPlayers; i++)
                {
                    playerArray[i].abilityList.Clear();
                }
            }
        }
        /// <summary>
        /// Den här metoden spawnar en smallDevil-fiender.
        /// </summary>
        private void SpawnSmallRedDevil()
        {
            int x = ValueBank.rand.Next((ValueBank.WindowSizeX / 2) + 20, ValueBank.WindowSizeX - TextureBank.enemyTextureList[0].Width - 20);
            int y = ValueBank.rand.Next(120, ValueBank.WindowSizeY - TextureBank.enemyTextureList[0].Height - 40);

            enemyList.Add(new SmallDevil(TextureBank.enemyTextureList[0], new Vector2(x, y), playerArray, nrOfPlayers));
        }
        /// <summary>
        /// Den här metoden spawnar en slime
        /// </summary>
        private void SpawnSlime()
        {
            int x = ValueBank.rand.Next((ValueBank.WindowSizeX / 2) + 20, ValueBank.WindowSizeX - TextureBank.enemyTextureList[0].Width - 20);
            int y = ValueBank.rand.Next(120, ValueBank.WindowSizeY - TextureBank.enemyTextureList[0].Height - 40);

            enemyList.Add(new Slime(TextureBank.enemyTextureList[1], new Vector2(x, y), playerArray, nrOfPlayers));
        }

        private void SpawnSpider()
        {
            enemyList.Add(new WallSpider(TextureBank.enemyTextureList[2], new Vector2(35, 175), playerArray, nrOfPlayers));
        }
        private void SpawnDevil()
        {
            int x = ValueBank.WindowSizeX / 2;
            int y = ValueBank.WindowSizeY / 2 + ValueBank.GameWindowStartY;

            enemyList.Add(new Devil(TextureBank.enemyTextureList[3], new Vector2(x, y), playerArray, nrOfPlayers));

        }
        /// <summary>
        /// Den här metoden lägger till fiender till rummen
        /// </summary>
        private void AddEnemiesToRoom()
        {
            if (RoomCoordinateX == 3 && RoomCoordinateY == 1 && !enemiesSpawned[3, 1])
            {
                for (int i = 0; i < 7; i++)
                {
                    //SpawnSpider();
                    SpawnSmallRedDevil();
                    //SpawnDevil();
                    //SpawnSlime();
                }

                enemiesSpawned[3, 1] = true;
            }

            if (RoomCoordinateX == 2 && RoomCoordinateY == 1 && !enemiesSpawned[2, 1])
            {
                SpawnSlime();
                SpawnSlime();
                enemiesSpawned[2, 1] = true;
            }

            if (RoomCoordinateX == 2 && RoomCoordinateY == 2 && !enemiesSpawned[2, 2])
            {
                SpawnSmallRedDevil();
                SpawnSmallRedDevil();
                SpawnSmallRedDevil();
                SpawnSpider();
                SpawnSpider();
                SpawnSlime();

                enemiesSpawned[2, 2] = true;
            }
            if (RoomCoordinateX == 2 && RoomCoordinateY == 3 && !enemiesSpawned[2,3])
            {
                SpawnSmallRedDevil();
                SpawnSmallRedDevil();
                SpawnSmallRedDevil();
                SpawnSmallRedDevil();
                SpawnSlime();
                SpawnSlime();
                SpawnSlime();
                SpawnSpider();
                enemiesSpawned[2, 3] = true;
            }
            
            if (RoomCoordinateX == 2 && RoomCoordinateY == 4 && !enemiesSpawned[2, 4])
            {
                SpawnDevil();
                enemiesSpawned[2, 4] = true;
            }

        }
    }
}
