using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Dungeon
    {
        public List<Room> Rooms { get; private set; }
        public List<Wall> Walls { get; private set; }

        private const int DungeonWidth = 8;
        private const int DungeonHeight = 10;

        public Dungeon()
        {
            Rooms = [];
            Walls = [];
            InitializeDungeon();
        }

        private void InitializeDungeon()
        {
            SetInitialBlankDungeon();

            RandomlyFillDungeon();
        }

        private void SetInitialBlankDungeon()
        {
            for (int i = 0; i < DungeonWidth; i++)
            {
                for (int j = 0; j < DungeonHeight; j++)
                {
                    int roomId = i * DungeonWidth + j;
                    Room room = new Room(roomId, this);
                    Rooms.Add(room);

                    // Initialize blank walls between rooms and add them to the list
                    if (i > 0)
                    {
                        var northWall = new HorizontalWall(roomId - 8, roomId, BorderType.NoWall);
                        Walls.Add(northWall);
                        room.NorthWallIndex = Walls.Count - 1;
                        Rooms[roomId - DungeonWidth].SouthWallIndex = Walls.Count - 1;
                    }
                    if (j > 0)
                    {
                        var westWall = new VerticalWall(roomId - 1, roomId, BorderType.NoWall);
                        Walls.Add(westWall);
                        room.WestWallIndex = Walls.Count - 1;
                        Rooms[roomId - 1].EastWallIndex = Walls.Count - 1;
                    }
                }
            }
        }

        private void RandomlyFillDungeon()
        {
            // Add Monsters and Treasures to Rooms

            // Specify the wall types and add doors, passageways, and locked doors  
        }

        public string SaveDungeonStateToJson()
        {
            // Save the state of the dungeon to a file to a JSON file
            return "\"Dungeon\": {}";
        }

        public void LoadDungeonStateFromJson(string json)
        {
            // Load the state of the dungeon from a json file
        }


        public Wall GetWallByIndex(int index)
        {
            return Walls[index];
        }

        public Room GetRoomByCoordinates(int x, int y)
        {
            int index = x * DungeonWidth + y;
            return Rooms[index];
        }
    }

}
