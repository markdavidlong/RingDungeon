using System;
using Engine.Enums;
using Engine.PatternBases;
using Engine.Structs;

namespace Engine
{
    public interface IWallObserver
    {
        void OnWallUnlocked(Wall wall);
        void OnUnlockFailed(Wall wall);
        void OnSecretPassageFound(Wall wall);
        void OnSecretPassageNotFound(Wall wall);
        void OnLocationChanging(Wall wall);
    }

    public class Wall(BorderType borderType)
        : Observable<IWallObserver>
    {
        public Room? NorthOrWestRoom { get; set; }
        public Room? SouthOrEastRoom { get; set; }

        public bool IsHorizontalWall { get; set; } = false;
        public bool IsVerticalWall { get => !IsHorizontalWall; }

        public BorderType BorderType { get; private set; } = borderType;

        public EntityLocation AttemptPass(Player player)
        {
            EntityLocation currentLocation = player.Location;

            Room? currentRoom = currentLocation.CurrentRoom;
            Room? targetRoom = null;

            if (currentRoom == null)
            {
                // TODO: Throw an exception
            }


            if (NorthOrWestRoom == currentRoom)
            {
                targetRoom = SouthOrEastRoom;
            }
            else if (SouthOrEastRoom == currentRoom)
            {
                targetRoom = NorthOrWestRoom;
            }

            int targetX = currentLocation.X;
            int targetY = currentLocation.Y;

            if (BorderType == BorderType.SolidWall)
            {
                return currentLocation; // Can't pass through solid walls
            }
            else if (BorderType == BorderType.LockedDoor)
            {
                return currentLocation; // Door is locked
            }
            else if (BorderType == BorderType.SecretPassage)
            {
                return currentLocation; // Passage is undiscovered
            }
            else if (BorderType == BorderType.DiscoveredPassage)
            {
                // If player is aligned with the passage, move through it
            }
            else if (BorderType == BorderType.Door)
            {
                // If player is aligned with the door, move through it  
            }

            // Normal transition logic
            var loc = new EntityLocation(targetRoom, targetX, targetY, Direction.North);
            NotifyObservers(observer => observer.OnLocationChanging(this));
            return loc;
        }

        public string GetWallBlock(int offset)
        {   // █▓▒░  
            // TODO: Implement different wall types.  These are placeholders.

            if (IsHorizontalWall && offset >= Constants.RoomMinX && offset <= Constants.RoomMaxX)
            {
                return "░░";  
            }
            else if (IsVerticalWall && offset >= Constants.RoomMinY && offset <= Constants.RoomMaxY)
            {
                return "░░";  
            }

            return "  ";
        }

    } // Class Wall
} // Namespace Engine