using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player
    {
        public string Name { get; set; }
        public PlayerLocation Location { get; set; }
        public int Health { get; set; }

        public Player(string name)
        {
            Name = name;
            // Set Start Location
            // Set Health, etc.
        }

        public bool Move(int deltaX, int deltaY, Room currentRoom)
        {
            // Calculate new position
            int newX = Location.X + deltaX;
            int newY = Location.Y + deltaY;

            // Check if the player stays within room bounds
            if (newX < 0 || newX >= 10 || newY < 0 || newY >= 10)
            {
                Direction direction;
                if (newX < 0)
                {
                    direction = Direction.West;
                }
                else if (newX >= 10)
                {
                    direction = Direction.East;
                }
                else if (newY < 0)
                {
                    direction = Direction.North;
                }
                else
                {
                    direction = Direction.South;
                }

                Wall wall = currentRoom.GetWall(direction);
                PlayerLocation newLocation = wall.AttemptPass(this, Location);
                if (!newLocation.Equals(Location))
                {
                    Location = newLocation;
                    return true; // Transition successful
                }
                return false; // Transition failed
            }

            // If within room bounds, move normally
            Location = new PlayerLocation(Location.RoomId, newX, newY);
            return true;
        }
    }

}
