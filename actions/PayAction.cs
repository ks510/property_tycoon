using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents an action where the player must pay the named recipient
    /// the given amount of cash.
    /// </summary>
    public class PayAction : IAction
    {
        private int amount;
        private Recipient payTo;

        /// <summary>
        /// Constructor for a payment action from the player to named recipient.
        /// </summary>
        /// <param name="amount">Cash to pay recipient</param>
        /// <param name="payTo">Recipient</param>
        public PayAction(int amount, Recipient payTo)
        {
            this.amount = amount;
            this.payTo = payTo;
        }

        /// <summary>
        /// Return the amount needed to pay by player.
        /// </summary>
        /// <returns></returns>
        public int GetAmount()
        {
            return this.amount;
        }

        /// <summary>
        /// Return the recipient of payment from player.
        /// </summary>
        /// <returns>Recipient</returns>
        public Recipient GetPayTo()
        {
            return this.payTo;
        }
    }
}
