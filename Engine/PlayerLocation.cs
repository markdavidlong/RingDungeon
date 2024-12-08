using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public struct PlayerLocation
    {
        public int RoomId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }

        public PlayerLocation(int roomId, int x, int y, Direction facing)
        {
            RoomId = roomId;
            X = x;
            Y = y;
            Facing = facing;
        }
    }

}
