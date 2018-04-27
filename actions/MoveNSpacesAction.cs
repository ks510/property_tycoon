

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents an action where the player must move the stated amount of spaces
    /// in the stated direction on the game board.
    /// </summary>
    public class MoveNSpacesAction : IAction
    {
        private int numberOfSpaces;
        private bool clockwise;

        /// <summary>
        /// Constructor for a move-n-spaces action.
        /// </summary>
        /// <param name="numberOfSpaces"></param>
        /// <param name="clockwise"></param>
        public MoveNSpacesAction(int numberOfSpaces, bool clockwise)
        {
            this.numberOfSpaces = numberOfSpaces;
            this.clockwise = clockwise;
        }

        /// <summary>
        /// Return the number of spaces the player must move on the board.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfSpaces()
        {
            return this.numberOfSpaces;
        }

        /// <summary>
        /// Return the direction to move around the game board.
        /// </summary>
        /// <returns>True if clockwise, false otherwise.</returns>
        public bool MoveClockwise()
        {
            return this.clockwise;
        }
    }
}
