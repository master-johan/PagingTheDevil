using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.Manager
{
    class LevelManager
    {
        StreamReader streamReader;
        List<String> map;

        public LevelManager()
        {
            CurrentLevel = new Room[5, 5];
            map = new List<string>();
            ReadFile();
        }

        public Room[,] CurrentLevel { get; set; }

        private void ReadFile()
        {
            streamReader = new StreamReader(@"level1.txt");

            while (!streamReader.EndOfStream)
            {
                map.Add(streamReader.ReadLine());
            }
            streamReader.Close();

            for (int y = 0; y < map.Count(); y++)
            {
                string row = map[y];
                Room emptyRoom = new Room(Color.Black, false, false, false);
                Room startRoom = new Room(Color.LightGray, true, true, false);
                Room standardRoom = new Room(Color.Gray, true, false, false);
                Room bossRoom = new Room(Color.Blue, true, false, true);

                for (int x = 0; x < row.Length; x++)
                {
                    if (row[x] == '-' )
                    {
                        CurrentLevel[x, y] = emptyRoom;
                    }
                    else if (row[x] == 'R')
                    {
                        CurrentLevel[x, y] = standardRoom;
                    }
                    else if (row[x] == 'S')
                    {
                        CurrentLevel[x, y] = startRoom;
                    }
                    else if (row[x] == 'B')
                    {
                        CurrentLevel[x, y] = bossRoom;
                    }
                }
            }
        }
    }
}
