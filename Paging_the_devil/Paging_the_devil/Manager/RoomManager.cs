using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Paging_the_devil.GameObject;

namespace Paging_the_devil.Manager
{
    class RoomManager
    {
        int nrOfPlayers;
        Player[] playerArray;

        Room currentRoom;

        Room[] roomArray;

        List<Room> roomList;
        List<Enemy> enemyList;

        public RoomManager(Player[] playerArray, int nrOfPlayers,List<Enemy> enemyList)
        {
            this.playerArray = playerArray;
            this.nrOfPlayers = nrOfPlayers;
            this.enemyList = enemyList;

            CreateRooms();
        }

        public void Update()
        {
            CollisionWithWall();
            DeleteAbilities();
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if(playerArray[i].GetRect.Intersects(currentRoom.GetGatewayList()[0].GetRect) && playerArray[i].Controller.ButtonPressed(Buttons.Y))
                {
                    currentRoom = roomList[1];
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentRoom.Draw(spriteBatch);

        }
    

        private void CollisionWithWall()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                bool[,] boolArray = new bool[4, currentRoom.GetWallList().Count];

                for (int j = 0; j < currentRoom.GetWallList().Count; j++)
                {

                    if (playerArray[i].GetRect.Intersects(currentRoom.GetWallList()[j].HitboxBot))
                    {
                        boolArray[0, j] = true;
                    }
                    else if (playerArray[i].GetRect.Intersects(currentRoom.GetWallList()[j].HitboxTop))
                    {
                        boolArray[1, j] = true;
                    }
                    else if (playerArray[i].GetRect.Intersects(currentRoom.GetWallList()[j].HitboxLeft))
                    {
                        boolArray[2, j] = true;
                    }
                    else if (playerArray[i].GetRect.Intersects(currentRoom.GetWallList()[j].HitboxRight))
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

        private void DeleteAbilities()
        {
            Ability toRemoveAbility = null;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var w in currentRoom.GetWallList())
                    {
                        if (a.GetRect.Intersects(w.GetRect))
                        {
                            toRemoveAbility = a;
                        }
                    }
                    //if (a.pos.Y > windowY || a.pos.X < 0 || a.pos.X > windowX || a.pos.Y < 0)
                    //{
                    //    toRemoveAbility = a;
                    //}
                }

                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var e in enemyList)
                    {
                        if (a.GetRect.Intersects(e.GetRect))
                        {
                            toRemoveAbility = a;
                        }
                    }
                }

                if (toRemoveAbility != null)
                {
                    playerArray[i].abilityList.Remove(toRemoveAbility);
                }
            }
        }
        private void CreateRooms()
        {
            roomList = new List<Room>();
            Room room1, room2, room3;
            room1 = new Room(Color.SteelBlue);
            room2 = new Room(Color.AliceBlue );
            room3 = new Room(Color.White);

            roomList.Add(room1);
            roomList.Add(room2);
            roomList.Add(room3);



            roomList[0].CreateGateWays(2);
            roomList[1].CreateGateWays(3);
            roomList[1].CreateGateWays(2);
            roomList[2].CreateGateWays(3);

            currentRoom = roomList[0];
        }
    }
}
