

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents a "draw card" action and stores the type of card to draw (Pot Luck or Opportunity Knocks)
    /// </summary>
    public class DrawCardAction : IAction
    {
        private CardType card;

        /// <summary>
        /// Constructor for a "draw card" action.
        /// </summary>
        /// <param name="card">Type of card to draw (Pot Luck or Opportunity Knocks)</param>
        public DrawCardAction(CardType card)
        {
            this.card = card;
        }

        /// <summary>
        /// Return the type of card the player should draw.
        /// </summary>
        /// <returns>Card name</returns>
        public CardType GetCardType()
        {
            return this.card;
        }
    }
}
