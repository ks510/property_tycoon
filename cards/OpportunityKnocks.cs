using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// This class represents an "Opportunity Knocks" card in the Property Tycoon game 
    /// where players must follow the instructions on the card.
    /// </summary>
    public class OpportunityKnocks : AbstractCard
    {
        private string cardName;

        /// <summary>
        /// Constructor for an Opportunity Knocks card.
        /// </summary>
        /// <param name="description">Description of instructions the player must follow.</param>
        /// <param name="action">Action required</param>
        public OpportunityKnocks(string description, IAction action) : base(description, action)
        {
            this.cardName = "Opportunity Knocks";
        }

        /// <summary>
        /// Get the name of this card type.
        /// </summary>
        /// <returns>Name of card (Opporunity Knocks)</returns>
        public override string GetCardName()
        {
            return this.cardName;
        }

        /// <summary>
        /// Get the string representation of this card in the format "Card Type: Description".
        /// </summary>
        /// <returns>String representation of card</returns>
        public override string ToString()
        {
            return String.Format("{0}: {1}", this.cardName, base.GetDescription());
        }
    }
}
