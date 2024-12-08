using System;
using System.Collections.Generic;
using Engine.Enums;


namespace Engine
{
    public class GameController()
    {
        

        private void ValidateCurrentPlayer()
        {
            if (Game.CurrentPlayer == null)
            {
                throw new InvalidOperationException("There is no current player.");
            }
        }

        public void DoMoveCurrentPlayerForward()
        {
            // Determine which way the player is facing and decide which cardinal direction that corresponds to
            ValidateCurrentPlayer();
            Direction facing = Game.CurrentPlayer!.Facing;
            RelativeDirection relativeDirection = RelativeDirection.Forward;
            Direction absoluteDirection = relativeDirection.ToAbsoluteDirection(facing);
            Game.CurrentPlayer!.Location.CurrentRoom!.MovePlayer(Game.CurrentPlayer!, absoluteDirection);
        }

        public void DoMoveCurrentPlayerBackwards()
        {
            ValidateCurrentPlayer();
            Direction facing = Game.CurrentPlayer!.Facing;
            RelativeDirection relativeDirection = RelativeDirection.Backward;
            Direction absoluteDirection = relativeDirection.ToAbsoluteDirection(facing);
            Game.CurrentPlayer!.Location.CurrentRoom!.MovePlayer(Game.CurrentPlayer!, absoluteDirection);
        }

        public void DoMoveCurrentPlayerLeft()
        {
            ValidateCurrentPlayer();
            Direction facing = Game.CurrentPlayer!.Facing;
            RelativeDirection relativeDirection = RelativeDirection.Left;
            Direction absoluteDirection = relativeDirection.ToAbsoluteDirection(facing);
            Game.CurrentPlayer!.Location.CurrentRoom!.MovePlayer(Game.CurrentPlayer!, absoluteDirection);
        }

        public void DoMoveCurrentPlayerRight()
        {
            ValidateCurrentPlayer();
            Direction facing = Game.CurrentPlayer!.Facing;
            RelativeDirection relativeDirection = RelativeDirection.Right;
            Direction absoluteDirection = relativeDirection.ToAbsoluteDirection(facing);
            Game.CurrentPlayer!.Location.CurrentRoom!.MovePlayer(Game.CurrentPlayer!, absoluteDirection);
        }

        public void DoSearchForSecretDoor()
        {
        }

        public void DoQuit()
        {
        }

        public void DoAttack()
        {
        }

        public void DoTryHealingPotion()
        {
        }

        public void DoUseMagic()
        {
        }

        public void DoGetTreasure()
        {
        }

        public void DoOpenDoor()
        {
        }
    }
}
