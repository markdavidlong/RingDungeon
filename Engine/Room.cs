using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engine
{
    public class Room
    {
        public int RoomId { get; private set; }
        private readonly Dungeon _dungeon;

        public int NorthWallIndex { get; set; }
        public int SouthWallIndex { get; set; }
        public int EastWallIndex { get; set; }
        public int WestWallIndex { get; set; }

        public List<Monster> Monsters { get; private set; }

        public Room(int roomId, Dungeon dungeon)
        {
            RoomId = roomId;
            _dungeon = dungeon;
            Monsters = [];
        }

        public void AddMonster(Monster monster)
        {
            Monsters.Add(monster);
        }

        public Wall GetWall(Direction direction)
        {
            int index;
            switch (direction)
            {
                case Direction.North:
                    index = NorthWallIndex;
                    break;
                case Direction.South:
                    index = SouthWallIndex;
                    break;
                case Direction.East:
                    index = EastWallIndex;
                    break;
                case Direction.West:
                    index = WestWallIndex;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _dungeon.GetWallByIndex(index);
        }
    }

}
