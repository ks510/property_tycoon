using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public abstract class AbstractCard
    {
        private string description;
        private IAction action;

        /// <summary>
        /// Constructor for a Card object in Property Tycoon.
        /// </summary>
        /// <param name="description">Description of instructions the player must follow upon drawing the card.</param>
        /// <param name="action">Action required</param>
        public AbstractCard(string description, IAction action)
        {
            this.description = description;
            this.action = action;
        }

        /// <summary>
        /// Get the description of instructions the player must follow.
        /// </summary>
        /// <returns>String description</returns>
        public string GetDescription()
        {
            return this.description;
        }

        /// <summary>
        /// Get the action object representing the actions required upon drawing the card.
        /// </summary>
        /// <returns>Action required</returns>
        public IAction GetAction()
        {
            return this.action;
        }

        /// <summary>
        /// Get the name of this card type.
        /// </summary>
        /// <returns>Name of card</returns>
        public abstract string GetCardName();
    }
}
