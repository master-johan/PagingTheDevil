﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.GameObject;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.GameObject.Characters;
using Microsoft.Xna.Framework.Media;

namespace Paging_the_devil.Manager
{
    class RoomManager
    {
        Player[] playerArray;

        Room[,] currentLevel;

        List<Enemy> enemyList;
        List<Gateway> gatewayList;

        LevelManager levelManager;

        Gateway gatewayNorth;
        Gateway gatewaySouth;
        Gateway gatewayEast;
        Gateway gatewayWest;

        Vector2 TargetDummySpawnPos;

        int RoomCoordinateX;
        int RoomCoordinateY;
        int nrOfPlayers;
        int spawnSpiderMaxY;
        int spawnSpiderMaxX;
        int spawnSpiderMinY;
        int spawnSpiderMinX;
        int spawnSlimeMaxX;
        int spawnSlimeMinX;
        int spawnSlimeMaxY;
        int spawnSlimeMinY;
        int spawnSmallDevilMaxX;
        int spawnSmallDevilMinX;
        int spawnSmallDevilMaxY;
        int spawnSmallDevilMinY;

        float timePassed;

        bool[,] enemiesSpawned;
        bool clear;

        public Room CurrentRoom { get; set; }

        public RoomManager(Player[] playerArray, int nrOfPlayers, List<Enemy> enemyList, LevelManager levelManager)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;
            this.enemyList = enemyList;
            this.levelManager = levelManager;

            gatewayList = new List<Gateway>();

            currentLevel = levelManager.CurrentLevel;
            GetStaringRoom();

            enemiesSpawned = new bool[5, 5];

            clear = true;
            SettingMaxAndMinValues();

            TargetDummySpawnPos = new Vector2(1700, 200);
        }

        public void Update(GameTime gameTime)
        {
            DeclareGateways();
            ShowGateways();
            CollisionWithWall();
            GoIntoGateway();
            AddEnemiesToRoom(gameTime);
            enemyCollisionWithWalls();
            SpawnPlayers();
            foreach (var gate in gatewayList)
            {
                if (enemyList.Count == 0 || enemyList[0] is TargetDummy)
                {
                    gate.Open = true;
                }
                else
                {
                    gate.Open = false;
                }
            }
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
        /// Den här metoden sätter alla max och min värden för fienders spawn position.
        /// </summary>
        private void SettingMaxAndMinValues()
        {
            spawnSpiderMaxX = 1875;
            spawnSpiderMaxY = 1030;
            spawnSpiderMinX = 35;
            spawnSpiderMinY = 175;
            spawnSlimeMaxX = 1500;
            spawnSlimeMinX = 350;
            spawnSlimeMaxY = 700;
            spawnSlimeMinY = 400;
            spawnSmallDevilMaxX = 1800;
            spawnSmallDevilMinX = 70;
            spawnSmallDevilMaxY = 950;
            spawnSmallDevilMinY = 200;
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

            Vector2 north = new Vector2(CurrentRoom.GetWallRectList()[0].Width / 2, CurrentRoom.GetWallRectList()[0].Y + 24);
            Vector2 south = new Vector2(CurrentRoom.GetWallRectList()[1].Width / 2, CurrentRoom.GetWallRectList()[1].Y + 10);
            Vector2 west = new Vector2(CurrentRoom.GetWallRectList()[2].X + 24, CurrentRoom.GetWallRectList()[2].Height / 2);
            Vector2 east = new Vector2(CurrentRoom.GetWallRectList()[3].X + 10, CurrentRoom.GetWallRectList()[3].Height / 2);

            gatewayList.Add(gatewayNorth = new Gateway(TextureBank.roomTextureList[8], north, 0, TextureBank.roomTextureList[9]));
            gatewayList.Add(gatewaySouth = new Gateway(TextureBank.roomTextureList[8], south, 1, TextureBank.roomTextureList[9]));
            gatewayList.Add(gatewayWest = new Gateway(TextureBank.roomTextureList[8], west, 2, TextureBank.roomTextureList[9]));
            gatewayList.Add(gatewayEast = new Gateway(TextureBank.roomTextureList[8], east, 3, TextureBank.roomTextureList[9]));
            
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
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY + 1].AllowedRoom)
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
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY + 1].AllowedRoom)
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
                    if (currentLevel[RoomCoordinateX, RoomCoordinateY - 1].AllowedRoom)
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

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (playerArray[i].GetRect.Intersects(gatewaySouth.GetRect) && gatewaySouth.IsVisible)
                {
                    if (playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0 || playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList[0] is TargetDummy)
                    {
                        //Tar bort fienderna ur listan när man går nedåt eftersom targetdummy måste tas bort när man går ur första rummet
                        //där dörren är south.
                        //enemyList.Clear();
                        RoomCoordinateY += 1;
                        temp = new Vector2(gatewayNorth.pos.X + TextureBank.roomTextureList[8].Width / 2, gatewayNorth.pos.Y + 50);
                    }
                }
                else if (playerArray[i].GetRect.Intersects(gatewayNorth.GetRect) && gatewayNorth.IsVisible)
                {
                    if (playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0 || playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList[0] is TargetDummy)
                    {
                        RoomCoordinateY -= 1;
                        temp = new Vector2(gatewaySouth.pos.X + TextureBank.roomTextureList[8].Width / 2, gatewaySouth.pos.Y - 50);
                    }
                }

                else if (playerArray[i].GetRect.Intersects(gatewayEast.GetRect) && gatewayEast.IsVisible)
                {
                    if (playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0 || playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList[0] is TargetDummy)
                    {
                        enemyList.Clear();
                        RoomCoordinateX += 1;
                        temp = new Vector2(gatewayWest.pos.X + 50, gatewayWest.pos.Y + TextureBank.roomTextureList[8].Height / 2);
                    }
                }
                else if (playerArray[i].GetRect.Intersects(gatewayWest.GetRect) && gatewayWest.IsVisible)
                {
                    if (playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList.Count == 0 || playerArray[i].Controller.ButtonPressed(Buttons.Y) && enemyList[0] is TargetDummy)
                    {
                        RoomCoordinateX -= 1;
                        temp = new Vector2(gatewayEast.pos.X - 50, gatewayEast.pos.Y + TextureBank.roomTextureList[8].Height / 2);
                    }
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
        /// Om spelaren dött och andra clutchar rummet får de döda 50 hp
        /// </summary>
        private void SpawnPlayers()
        {
            if (enemyList.Count != 0)
            {
                clear = false;
            }

            if (!clear && enemyList.Count == 0)
            {
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    if (playerArray[i].HealthPoints == 0)
                    {
                        playerArray[i].HealthPoints = 50;
                    }
                }
                clear = true;
            }
        }
        /// <summary>
        /// Den här metoden spawnar en smallDevil-fiender.
        /// </summary>
        private void SpawnSmallRedDevil()
        {
            int x = ValueBank.rand.Next(spawnSmallDevilMinX, spawnSmallDevilMaxX);
            int y = ValueBank.rand.Next(spawnSmallDevilMinY, spawnSmallDevilMaxY);

            enemyList.Add(new SmallDevil(TextureBank.enemyTextureList[0], new Vector2(x, y), playerArray, nrOfPlayers));
        }
        /// <summary>
        /// Den här metoden spawnar en slime
        /// </summary>
        private void SpawnSlime()
        {
            int x = ValueBank.rand.Next(spawnSlimeMinX, spawnSlimeMaxX);
            int y = ValueBank.rand.Next(spawnSlimeMinY, spawnSlimeMaxY);

            enemyList.Add(new Slime(TextureBank.enemyTextureList[1], new Vector2(x, y), playerArray, nrOfPlayers));
        }

        private void SpawnTopSpider()
        {
            enemyList.Add(new WallSpider(TextureBank.enemyTextureList[2], new Vector2(spawnSpiderMinX, spawnSpiderMinY), playerArray, nrOfPlayers));
        }
        private void SpawnBotSpider()
        {
            enemyList.Add(new WallSpider(TextureBank.enemyTextureList[2], new Vector2(spawnSpiderMaxX, spawnSpiderMaxY), playerArray, nrOfPlayers));
        }
        private void SpawnLeftSpider()
        {
            enemyList.Add(new WallSpider(TextureBank.enemyTextureList[2], new Vector2(spawnSpiderMinX, spawnSpiderMaxY), playerArray, nrOfPlayers));
        }
        private void SpawnRightSpider()
        {
            enemyList.Add(new WallSpider(TextureBank.enemyTextureList[2], new Vector2(spawnSpiderMaxX, spawnSpiderMinY), playerArray, nrOfPlayers));
        }
        private void SpawnDevil()
        {
            int x = ValueBank.WindowSizeX / 2;
            int y = ValueBank.WindowSizeY / 2 + ValueBank.GameWindowStartY;

            enemyList.Add(new Devil(TextureBank.enemyTextureList[3], new Vector2(x, y), playerArray, nrOfPlayers));
        }
        private void SpawnTargetDummy()
        {
            enemyList.Add(new TargetDummy(TextureBank.enemyTextureList[4], TargetDummySpawnPos, playerArray, nrOfPlayers));
        }
        /// <summary>
        /// Den här metoden lägger till fiender till rummen
        /// </summary>
        private void AddEnemiesToRoom(GameTime gameTime)
        {
            if (RoomReturn(0, 0))
            {
                Spawner(0, 0, false, false, false, false, false, false, false, true);

                enemiesSpawned[0, 0] = true;
            }
            //Första rummet, OBS inte startrum
            if (RoomReturn(1, 0))
            {
                Spawner(1, 0, true, false, false, false, false, false, false, false);
                enemiesSpawned[1, 0] = true;
            }
            //Andra rummet
            if (RoomReturn(2, 0))
            {
                Spawner(2, 1, true, true, false, true, false, false, false, false);
                enemiesSpawned[2, 0] = true;
            }
            //Tredje rummet
            if (RoomReturn(3, 0))
            {
                Spawner(4, 1, true, true, false, false, false, false, false, false);

                enemiesSpawned[3, 0] = true;
            }
            //Fjärde rummet
            if (RoomReturn(4,0))
            {
                Spawner(3, 2, true, true, false, false, false, true, false, false);

                enemiesSpawned[4,0] = true;
            }
            //Femte
            if (RoomReturn(4, 1))
            {
                Spawner(6, 3, true, true, false, true, false, true, false, false);

                enemiesSpawned[4, 1] = true;
            }
            //Sjätte
            if (RoomReturn(4, 2))
            {
                Spawner(10, 4, true, true, false, false, false, false, false, false);

                enemiesSpawned[4, 2] = true;
            }
            //Sjunde
            if (RoomReturn(3, 2))
            {
                Spawner(15, 1, true, true, false, true, false, false, false, false);

                enemiesSpawned[3, 2] = true;
            }
            //Åttonde
            if (RoomReturn(2, 2))
            {
                Spawner(18, 2, true, true, false, true, false, true, false, false);

                enemiesSpawned[2, 2] = true;
            }
            //Nionde
            if (RoomReturn(2, 3))
            {
                Spawner(5, 6, true, true, true, true, true, true, false, false);

                enemiesSpawned[2, 3] = true;
            }
            //Bossrummet
            if (RoomReturn(2, 4))
            {
                MediaPlayer.Play(SoundBank.BgMusicList[2]);
                Spawner(10, 2, true, true, false, true, false, true, true, false);

                enemiesSpawned[2, 4] = true;
            }
        }
        private bool RoomReturn(int first, int second)
        {
            if (RoomCoordinateX == first && RoomCoordinateY == second && !enemiesSpawned[first, second])
            {
                return true;
            }
            return false;
        }

        private void Spawner(int firstLoop, int secondLoop, bool smallDevil, bool slime, bool spiderTop, bool spiderRight, bool spiderBot, bool spiderLeft, bool devil, bool targetDummy)
        {
            for (int i = 0; i < firstLoop; i++)
            {
                if (smallDevil)
                {
                    SpawnSmallRedDevil();
                }
            }
            for (int i = 0; i < secondLoop; i++)
            {
                if (slime)
                {
                    SpawnSlime();
                }
            }
            if (spiderTop)
            {
                SpawnTopSpider();
            }
            if (spiderRight)
            {
                SpawnRightSpider();
            }
            if (spiderLeft)
            {
                SpawnLeftSpider();
            }
            if (spiderBot)
            {
                SpawnBotSpider();
            }
            if (devil)
            {
                SpawnDevil();
            }
            if (targetDummy)
            {
                SpawnTargetDummy();
            }
        }

        public bool CheckIfBossRoomDefeated()
        {
            if (RoomCoordinateX == 2 && RoomCoordinateY == 4 && enemyList.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            for (int i = 0; i < enemiesSpawned.GetLength(0); i++)
            {
                for (int j = 0; j < enemiesSpawned.GetLength(1); j++)
                {
                    enemiesSpawned[i, j] = false;
                }
            }
            CurrentRoom = currentLevel[0, 0];
            enemyList.Clear();
            RoomCoordinateX = 0;
            RoomCoordinateY = 0;          
        }
    }
}
