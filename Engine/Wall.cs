using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Wall
    {
        public int RoomId1 { get; private set; }
        public int RoomId2 { get; private set; }
        public BorderType BorderType { get; private set; }
        public bool IsLocked { get; private set; }
        public bool IsLockedFromThisSide { get; private set; }
        public PlayerLocation? PortalDestination { get; private set; }

        public Wall(int roomId1, int roomId2, BorderType borderType, bool isLocked = false, bool isLockedFromThisSide = false, PlayerLocation? portalDestination = null)
        {
            RoomId1 = roomId1;
            RoomId2 = roomId2;
            BorderType = borderType;
            IsLocked = isLocked;
            IsLockedFromThisSide = isLockedFromThisSide;
            PortalDestination = portalDestination;
        }

        public abstract PlayerLocation AttemptPass(Player player, PlayerLocation currentLocation);
    }

    public class HorizontalWall : Wall
    {
        public HorizontalWall(int roomId1, int roomId2, BorderType borderType, bool isLocked = false, bool isLockedFromThisSide = false, PlayerLocation? portalDestination = null)
            : base(roomId1, roomId2, borderType, isLocked, isLockedFromThisSide, portalDestination)
        {
        }

        public override PlayerLocation AttemptPass(Player player, PlayerLocation currentLocation)
        {
            // Implement logic specific to horizontal walls
            int targetRoomId = currentLocation.RoomId == RoomId1 ? RoomId2 : RoomId1;
            int targetX = currentLocation.X;
            int targetY = currentLocation.Y;

            if (BorderType == BorderType.SolidWall)
            {
                return currentLocation; // Can't pass through solid walls
            }

            if (BorderType == BorderType.LockedDoor)
            {
                if (IsLocked && !(IsLockedFromThisSide && currentLocation.RoomId == RoomId1))
                {
                    return currentLocation; // Door is locked
                }
            }

            // Check for portal
            if (PortalDestination.HasValue)
            {
                return PortalDestination.Value; // Warp to portal destination
            }

            // Normal transition logic
            return new PlayerLocation(targetRoomId, targetX, targetY, Direction.North);
        }
    }

    public class VerticalWall : Wall
    {
        public VerticalWall(int roomId1, int roomId2, BorderType borderType, bool isLocked = false, bool isLockedFromThisSide = false, PlayerLocation? portalDestination = null)
            : base(roomId1, roomId2, borderType, isLocked, isLockedFromThisSide, portalDestination)
        {
        }

        public override PlayerLocation AttemptPass(Player player, PlayerLocation currentLocation)
        {
            // Implement logic specific to vertical walls
            int targetRoomId = currentLocation.RoomId == RoomId1 ? RoomId2 : RoomId1;
            int targetX = currentLocation.X;
            int targetY = currentLocation.Y;

            if (BorderType == BorderType.SolidWall)
            {
                return currentLocation; // Can't pass through solid walls
            }

            if (BorderType == BorderType.LockedDoor)
            {
                if (IsLocked && !(IsLockedFromThisSide && currentLocation.RoomId == RoomId1))
                {
                    return currentLocation; // Door is locked
                }
            }

            // Check for portal
            if (PortalDestination.HasValue)
            {
                return PortalDestination.Value; // Warp to portal destination
            }

            // Normal transition logic
            return new PlayerLocation(targetRoomId, targetX, targetY, Direction.North);
        }
    }

}
