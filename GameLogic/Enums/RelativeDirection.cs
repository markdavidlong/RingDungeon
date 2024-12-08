using System;

namespace Engine.Enums
{
    public enum RelativeDirection
    {
        Forward,
        Right,
        Backward,
        Left
    }

    public static class RelativeDirectionExtensions
    {
        public static Direction ToAbsoluteDirection(this RelativeDirection relativeDirection, Direction facing)
        {
            return relativeDirection switch
            {
                RelativeDirection.Forward => facing,
                RelativeDirection.Right => (Direction) (((int) facing + 1) % 4),
                RelativeDirection.Backward => (Direction) (((int) facing + 2) % 4),
                RelativeDirection.Left => (Direction) (((int) facing + 3) % 4),
                _ => throw new ArgumentException("Invalid relative direction"),
            };
        }
    }
}
