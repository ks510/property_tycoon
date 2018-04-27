

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents a player's bid in an auction. Stores the bid amount 
    /// and whether the bid is public or not.
    /// </summary>
    public class Bid
    {
        private int bidAmount;
        private bool publicBid;

        /// <summary>
        /// Constructor for a bid.
        /// </summary>
        /// <param name="bidAmount">Amount the player is bidding</param>
        /// <param name="publicBid">Is the bid public or not?</param>
        public Bid(int bidAmount, bool publicBid) {
            this.bidAmount = bidAmount;
            this.publicBid = publicBid;
        }

        /// <summary>
        /// Return the bid amount.
        /// </summary>
        /// <returns>Bid amount</returns>
        public int GetBidAmount()
        {
            return this.bidAmount;
        }

        /// <summary>
        /// Check if this bid is public or not.
        /// </summary>
        /// <returns>True if public bid, false if private bid</returns>
        public bool IsPublicBid()
        {
            return this.publicBid;
        }


    }
}
