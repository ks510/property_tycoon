using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents an action where the player must move to the stated board space
    /// in the stated direction on the game board.
    /// </summary>
    public class MoveToAction : IAction
    {
        private int boardSpaceID;
        private bool clockwise;

        /// <summary>
        /// Constructor for a move-to-boardspace action.
        /// </summary>
        /// <param name="boardSpaceID"></param>
        /// <param name="clockwise"></param>
        public MoveToAction(int boardSpaceID, bool clockwise)
        {
            this.boardSpaceID = boardSpaceID;
            this.clockwise = clockwise;
        }

        /// <summary>
        /// Return the board space number the player should move to.
        /// </summary>
        /// <returns>Board space number</returns>
        public int GetBoardSpaceID()
        {
            return this.boardSpaceID;
        }

        /// <summary>
        /// Return the direction the player should move to the stated board space.
        /// </summary>
        /// <returns>True if moving clockwise, false otherwise.</returns>
        public bool MoveClockwise()
        {
            return this.clockwise;
        }
    }
}
