using Engine.Enums;
using Engine;
using System;

namespace Engine.Structs
{
    public struct EntityLocation(Room? room, int x, int y, Direction facing)
    {
        public Room? CurrentRoom { get; set; } = room;
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public Direction Facing { get; set; } = facing;

        public readonly bool IsInRoom
        {
            get => X >= Constants.RoomMinX && X <= Constants.RoomMaxX && 
                   Y >= Constants.RoomMinY && Y <= Constants.RoomMaxY;
        }

        public readonly bool IsOnWall
        {
            get => ((X == Constants.RoomWestWallOffset || X == Constants.RoomEastWallOffset) && 
                        Y >= Constants.RoomMinY && Y <= Constants.RoomMaxY)
                || ((Y == Constants.RoomNorthWallOffset || Y == Constants.RoomSouthWallOffset) && 
                        X >= Constants.RoomMinX && X <= Constants.RoomMaxX);
        }

        public readonly bool IsOutOfBounds
        {
            get => !(IsInRoom || IsOnWall);
        }

        public readonly Direction WhichWallIsUserOn()
        {
            if (!IsOnWall)
            {
                return Direction.Unspecified;
            }
            return Y switch
            {
                -1 => Direction.North,
                Constants.MaxRoomHeight + 1 => Direction.South,
                _ => X switch
                {
                    -1 => Direction.West,
                    Constants.MaxRoomWidth + 1 => Direction.East,
                    _ => Direction.Unspecified
                },
            };
        }

        public readonly EntityLocation NewLocationWithOffset(Direction direction, int distance = 1)
        {
            int x = X;
            int y = Y;
            switch (direction)
            {
                case Direction.North:
                    y-=distance;
                    break;
                case Direction.South:
                    y+=distance;
                    break;
                case Direction.East:
                    x+=distance;
                    break;
                case Direction.West:
                    x-=distance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return new EntityLocation(CurrentRoom, x, y, Facing);
        }

    }

    public class NoAvailablePositionException(string message) : Exception(message)
    {
    }
}
