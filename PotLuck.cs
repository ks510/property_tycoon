using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// This class represents a "Pot Luck" card in the Property Tycoon game 
    /// where players must follow the instructions on the card.
    /// </summary>
    public class PotLuck : AbstractCard
    {
        private string cardName;

        /// <summary>
        /// Constructor for a Pot Luck card.
        /// </summary>
        /// <param name="description">Description of instructions the player must follow.</param>
        /// <param name="action">Action required</param>
        public PotLuck(string description, IAction action) : base(description,action)
        {
            this.cardName = "Pot Luck";
        }

        /// <seealso cref="PropertyTycoonProject.AbstractCard.GetCardName"/>
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
