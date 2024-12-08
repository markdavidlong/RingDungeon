namespace Engine
{
    public static class Constants
    {
        // Room dimensions
        public const int MaxRoomWidth = 15;
        public const int MaxRoomHeight = 15 ;

        public const int RoomMinX = 0;
        public const int RoomMaxX = MaxRoomWidth - 1;
        public const int RoomMinY = 0;
        public const int RoomMaxY = MaxRoomHeight - 1;

        public const int RoomNorthWallOffset = -1;
        public const int RoomEastWallOffset = MaxRoomWidth;
        public const int RoomSouthWallOffset = MaxRoomHeight;
        public const int RoomWestWallOffset = -1;

        // This is the center of the room in terms of the X position
        public const int RoomXCenter = Constants.MaxRoomWidth / 2;
        // This is the center of the room in terms of the Y position
        public const int RoomYCenter = Constants.MaxRoomHeight / 2;

        // Dungeon dimensions
        public const int DungeonWidth = 3;
        public const int DungeonHeight = 3;

    }
}

