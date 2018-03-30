using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents a "draw card" action and stores the type of card to draw (Pot Luck or Opportunity Knocks)
    /// </summary>
    public class DrawCardAction : IAction
    {
        private string cardName;

        /// <summary>
        /// Constructor for a "draw card" action.
        /// </summary>
        /// <param name="cardName">Type of card to draw (Pot Luck or Opportunity Knocks)</param>
        public DrawCardAction(string cardName)
        {
            this.cardName = cardName;
        }

        /// <summary>
        /// Return the type of card the player should draw.
        /// </summary>
        /// <returns>Card name</returns>
        public string GetCardName()
        {
            return this.cardName;
        }
    }
}
