namespace Engine.Enums
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
        Unspecified
    }

    public static class DirectionExtensions
    {
        // return the opposite direction
        public static Direction Opposite(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.South,
                Direction.South => Direction.North,
                Direction.East => Direction.West,
                Direction.West => Direction.East,
                _ => Direction.Unspecified
            };
        }
    }
}
