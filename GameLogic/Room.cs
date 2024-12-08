using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Engine.Enums;
using Engine.PatternBases;
using Engine.Structs;

namespace Engine
{
    public interface IRoomObserver
    {
        void OnMonsterAdded(Room room, Monster monster);
        void OnWallChanged(Room room, Direction direction, Wall newWall);
    }

    public class Room(int roomId, Wall northWall, Wall southWall, Wall westWall, Wall eastWall) : Observable<IRoomObserver>
    {


        public int RoomId { get; private set; } = roomId;

        public Wall NorthWall { get; set; } = northWall;
        public Wall SouthWall { get; set; } = southWall;
        public Wall EastWall { get; set; } = eastWall;
        public Wall WestWall { get; set; } = westWall;

        // TODO: This will actually be a calculated value based on the room's contents
        // once Treasures are implemented
        public bool HasTreasure { get; set; } = true;

        public List<Monster> Monsters { get; private set; } = [];

        public void AddMonster(Monster monster)
        {
            // TODO: This needs to be expanded out to place monsters in the room in appropriate locations
            Monsters.Add(monster);
            NotifyObservers(observer => observer.OnMonsterAdded(this, monster));
        }

        public void MovePlayer(Player player, Direction direction)
        {
            // Copy the current location of the player into a temp location.
        
            // Then use the EntityDirection to move it the right direction.
            EntityLocation newLocation = player.Location.NewLocationWithOffset(direction);
            // If the new location is in the room and there is nothing blocking the user, update the player's location.
            if (newLocation.IsInRoom)
            {
                if (!IsSomethingAt(newLocation.X, newLocation.Y))
                {
                    newLocation.Facing = direction;
                    player.Location = newLocation;

                    //Console.WriteLine("Moving player to new location: " + newLocation.X + "," + newLocation.Y);
                    newLocation.Facing = direction;
                    player.Location = newLocation;
                }
                else
                {
                    //Console.WriteLine("Something is blocking the player!");
                }
            }
            else
            {
                //Console.WriteLine("Player not in room!");
            }

        }

        public Wall GetWall(Direction direction)
        {
            return direction switch
            {
                Direction.North => NorthWall,
                Direction.South => SouthWall,
                Direction.East => EastWall,
                Direction.West => WestWall,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
            };
        }

        public EntityLocation GetNextAvailableWallPosition(EntityLocation originalPosition)
        {
            int x = originalPosition.X;
            int y = originalPosition.Y;

            if (!IsSomethingAt(x,y))
            {
                return originalPosition;
            }

            if (y == 0) // North wall
            {
                for (int i = 1; i < Constants.MaxRoomWidth / 2; i++)
                {
                    if (x - i >= 0 && !IsSomethingAt(x - i, y))
                    {
                        return new EntityLocation(this, x - i, y, originalPosition.Facing);
                    }
                    if (x + i <= Constants.MaxRoomWidth && !IsSomethingAt(x + i, y))
                    {
                        return new EntityLocation(this, x + i, y, originalPosition.Facing);
                    }
                }
                throw new NoAvailablePositionException("No available position found.");
            }
            else if (y == Constants.MaxRoomHeight) // South wall
            {
                for (int i = 1; i < Constants.MaxRoomWidth / 2; i++)
                {
                    if (x - i >= 0 && !IsSomethingAt(x - i, y))
                    {
                        return new EntityLocation(this, x - i, y, originalPosition.Facing);
                    }
                    if (x + i < Constants.MaxRoomWidth && !IsSomethingAt(x + i, y))
                    {
                        return new EntityLocation(this, x + i, y, originalPosition.Facing);
                    }
                }
                throw new NoAvailablePositionException("No available position found.");
            }
            else if (x == 0) // West wall
            {
                for (int i = 1; i < Constants.MaxRoomHeight / 2; i++)
                {
                    if (y - i >= 0 && !IsSomethingAt(x, y - i))
                    {
                        return new EntityLocation(this, x, y - i, originalPosition.Facing);
                    }
                    if (y + i < Constants.MaxRoomHeight && !IsSomethingAt(x, y + i))
                    {
                        return new EntityLocation(this, x, y + i, originalPosition.Facing);
                    }
                }
                throw new NoAvailablePositionException("No available position found.");
            }
            else if (x == Constants.MaxRoomWidth) // East wall
            {
                for (int i = 1; i < Constants.MaxRoomHeight / 2; i++)
                {
                    if (y - i >= 0 && !IsSomethingAt(x, y - i))
                    {
                        return new EntityLocation(this, x, y - i, originalPosition.Facing);
                    }
                    if (y + i < Constants.MaxRoomHeight && !IsSomethingAt(x, y + i))
                    {
                        return new EntityLocation(this, x, y + i, originalPosition.Facing);
                    }
                }
                throw new NoAvailablePositionException("No available position found.");
            }
            throw new ArgumentException("Was not called with a wall-adjacent position.");
        }

        private bool IsSomethingAt(int x, int y)
        {
            //Console.WriteLine(" Checking for something at " + x + "," + y);
            // check each monster in room.  If any of them are at the x,y position, return true
            foreach (Monster monster in Monsters)
            {
                if (monster.Location.X == x && monster.Location.Y == y)
                {
                    //Console.WriteLine("  Monster is at " + x + "," + y);
                    return true;
                }
            }
            foreach (Player player in Game.Players)
            {
                if (player == Game.CurrentPlayer!)
                {
                    continue;
                } else if (player.Location.CurrentRoom == this && player.Location.X == x && player.Location.Y == y)
                {
                    //Console.WriteLine("  Player is at " + x + "," + y);
                    return true;
                }
            }

            //Console.WriteLine("  Nothing is at " + x + "," + y);
            return false;
        }

        public string GetRoomBlock(int x, int y)
        {
            // Return a string representation of the room at the given x, y position
            // This will be used to draw the room on the screen.  
            // If the block contains a player, then it should return 1,2,3,4 or 5, depending on the player number.
            // If the block contains a monster, then it should return M
            // If the block contains a treasure, then it should return T
            // If the block has an offset that is -1 or Width+1 or Height+1, then it should return whatever is returned from
            // that particlar wall's GetWallBlock method.
            // Otherwise, it is an empty floor tile so it should return "." or " " or something similar.

            // Check for players
            for (int i = 0; i < Game.Players.Count; i++)
            {
                Player player = Game.Players[i];
                if (player.Location.CurrentRoom == this && player.Location.X == x && player.Location.Y == y)
                {
                    return "P" + (i + 1).ToString(); // Return player number (1, 2, 3, 4, or 5)
                }
            }

            // Check for monsters
            int monNumber = 0;
            foreach (Monster monster in Monsters)
            {
                if (monster.Location.X == x && monster.Location.Y == y)
                {
                    return "☠️" + monNumber; 
                }
                monNumber++;
            }

            // Check for treasure
            if (HasTreasure && x==Constants.RoomXCenter && y == Constants.RoomYCenter /*&& IsTreasureAt(x, y)*/)
            {
                return "$$";  
            }

            // Check for wall blocks

            if (x == Constants.RoomWestWallOffset && (y >= Constants.RoomNorthWallOffset && y <= Constants.RoomSouthWallOffset))
            {
                // return "W";
                return WestWall.GetWallBlock(y);
            }

            if (x == Constants.RoomEastWallOffset && (y >= Constants.RoomNorthWallOffset && y <= Constants.RoomSouthWallOffset))
            {
                // return "E";
                return EastWall.GetWallBlock(y);
            }

            if (y == Constants.RoomNorthWallOffset && (x >= Constants.RoomWestWallOffset && x <= Constants.RoomEastWallOffset))
            {
                // return "N";
                return NorthWall.GetWallBlock(x);
            }

            if (y == Constants.RoomSouthWallOffset && (x >= Constants.RoomWestWallOffset && x <= Constants.RoomEastWallOffset))
            {
                // return "S";
                return SouthWall.GetWallBlock(x);
            }


            // Empty floor tile
            return "▒▒";
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append($"Room {RoomId} {GetRoomXY(RoomId)}    ");
            builder.Append($"{Game.CurrentPlayer!.Name} is currently at {Game.CurrentPlayer!.Location.X},{Game.CurrentPlayer!.Location.Y}");
            builder.Append("\n\n");
            for (int y = Constants.RoomNorthWallOffset; y <= Constants.RoomSouthWallOffset; y++)
            {
                for (int x = Constants.RoomWestWallOffset; x <= Constants.RoomEastWallOffset; x++)
                {
                    builder.Append(GetRoomBlock(x, y));
                }
                builder.Append('\n');
            }
            return builder.ToString();
        }

        public static string GetRoomXY(int id)
        {
            if (id >= 0 && id < Constants.DungeonWidth * Constants.DungeonHeight)
            {
                int x = id / Constants.DungeonWidth;
                int y = id % Constants.DungeonHeight;
                return $"({x}, {y})";
            }
            else
            {
                return "(Invalid)";
            }
        }

        } // Class Room
} // Namespace Engine
