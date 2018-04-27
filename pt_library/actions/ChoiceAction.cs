

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents a "choice" action where the player chooses which action to take next.
    /// </summary>
    public class ChoiceAction : IAction
    {
        private IAction choice1;
        private IAction choice2;

        /// <summary>
        /// Constructor for a "choice" action where the player can choose to take one of two
        /// actions.
        /// </summary>
        /// <param name="choice1">First choice of action.</param>
        /// <param name="choice2">Second choice of action.</param>
        public ChoiceAction(IAction choice1, IAction choice2)
        {
            this.choice1 = choice1;
            this.choice2 = choice2;
        }

        /// <summary>
        /// Return the first choice of action available for player to choose.
        /// </summary>
        /// <returns>Action.</returns>
        public IAction GetChoice1()
        {
            return this.choice1;
        }

        /// <summary>
        /// Return the second choice of action available for player to choose.
        /// </summary>
        /// <returns>Action.</returns>
        public IAction GetChoice2()
        {
            return this.choice2;
        }
    }
}
