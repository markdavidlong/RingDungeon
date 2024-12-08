#pragma warning disable IDE0079  // TODO: Remove this when done with the class

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Engine.Enums;

namespace Engine
{
    
    public class Dungeon
    {
        public List<Room> Rooms { get; private set; }

        Wall[,] VertWalls  = new Wall[Constants.DungeonWidth + 1, Constants.DungeonHeight + 1] ;
        Wall[,] HorizWalls = new Wall[Constants.DungeonWidth +1 , Constants.DungeonHeight + 1];



        public Dungeon()
        {
            Rooms = [];
            InitializeDungeon();
        }

        private void InitializeDungeon()
        {
            // Initialize VertWalls array with new vertical walls
            for (int i = 0; i <= Constants.DungeonWidth; i++)
            {
                for (int j = 0; j <= Constants.DungeonHeight; j++)
                {
                    VertWalls[i, j] = new Wall(BorderType.SolidWall) { IsHorizontalWall = false };
                }
            }

            // Initialize HorizWalls array with new horizontal walls
            for (int i = 0; i <= Constants.DungeonWidth; i++)
            {
                for (int j = 0; j <= Constants.DungeonHeight; j++)
                {
                    HorizWalls[i, j] = new Wall(BorderType.SolidWall) { IsHorizontalWall = true };
                }
            }

            SetInitialBlankDungeon();

            RandomlyFillDungeon();
        }

        private void SetInitialBlankDungeon()
        {
            for (int i = 0; i < Constants.DungeonWidth; i++)
            {
                for (int j = 0; j < Constants.DungeonHeight; j++)
                {
                    int roomId = i * Constants.DungeonWidth + j;

                    var room = new Room(roomId, HorizWalls[i, j], HorizWalls[i + 1, j], VertWalls[i, j], VertWalls[i + 1, j]);
                    Rooms.Add(room);
                }
            }
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Work in progress")]
        private void RandomlyFillDungeon()
        {
            // Add Monsters and Treasures to Rooms

            // Specify the wall types and add doors, passageways, and locked doors  
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Work in progress")]
        public string SaveDungeonStateToJson()
        {
            // Save the state of the dungeon to a file to a JSON file
            return "\"Dungeon\": {}";
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Work in progress")]
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Work in progress")]
        public void LoadDungeonStateFromJson(string json)
        {
            // Load the state of the dungeon from a json file
        }


 

        public Room GetRoomByCoordinates(int x, int y)
        {
            int index = x * Constants.DungeonWidth + y;
            return Rooms[index];
        }
    }

}

#pragma warning restore IDE0079  // TODO: Remove this when done with the class