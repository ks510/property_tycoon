

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents an action where the player receives the stated amount of cash
    /// from the stated entity.
    /// </summary>
    public class ReceiveMoneyAction : IAction
    {
        private int amount;
        private Sender receiveFrom;

        /// <summary>
        /// Constructor for a receive-money action.
        /// </summary>
        /// <param name="amount">The amount of cash the player receives.</param>
        /// <param name="receiveFrom">Entity(s) the cash is debited from.</param>
        public ReceiveMoneyAction(int amount, Sender receiveFrom)
        {
            this.amount = amount;
            this.receiveFrom = receiveFrom;
        }

        /// <summary>
        /// Return the amount of cash to pay the player.
        /// </summary>
        /// <returns>Amount to pay player.</returns>
        public int GetAmount()
        {
            return this.amount;
        }

        /// <summary>
        /// Return paying entity of cash to the player.
        /// </summary>
        /// <returns>Paying entity.</returns>
        public Sender GetReceiveFrom()
        {
            return this.receiveFrom;
        }
    }
}
