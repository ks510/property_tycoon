using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class Auction
    {
        private IProperty property;
        private int startingBid;
        List<IPlayer> biddingPlayers;
        Dictionary<IPlayer, Bid> bids;

        /// <summary>
        /// Constructor for an Auction. All eligible players are added to the auction.
        /// Assumes the starting bid is £1 for all auctions.
        /// </summary>
        /// <param name="property">Property for sale</param>
        /// <param name="biddingPlayers">Eligible players</param>
        public Auction(IProperty property, List<IPlayer> biddingPlayers)
        {
            this.property = property;
            this.startingBid = 1;   // minimum bid must be £1 for all auctions
            this.bids = new Dictionary<IPlayer, Bid>(); // no bids made yet
            this.biddingPlayers = biddingPlayers;

        }

        /// <summary>
        /// Places the given player's bid on auction. Throws exception if the player
        /// isn't eligible to bid.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="bid"></param>
        public void PlaceBid(IPlayer player, Bid bid)
        {
            if (biddingPlayers.Contains(player)) // only eligible players can bid in this auction
            {
                if (bids.ContainsKey(player))   // players can only make a single bid per auction
                {
                    throw (new AuctionException("Player has already bidded in this auction"));
                }
                else
                {
                    // check the player has sufficient cash to bid
                    if ((bid.GetBidAmount() > player.PeekCash()))
                    {
                        throw (new AuctionException("Insufficient cash to make this bid!"));
                    }

                    foreach (KeyValuePair<IPlayer, Bid> existingBids in bids)
                    {
                        // check if another player has already bidded the exact same amount
                        // to prevent ties in bidding, only one (or none) player can be the highest bidder
                        if (existingBids.Value.GetBidAmount() == bid.GetBidAmount())
                        {
                            throw (new AuctionException("Cannot bid the same amount as another player!"));
                        }
                    }
                    // otherwise place the bid
                    bids.Add(player, bid);
                }
            }
            else
            {
                throw (new AuctionException("Player not eligible to bid in auction."));
            }
        }

        /// <summary>
        /// Return the given player's bid in this auction.
        /// </summary>
        /// <param name="player">player's bid to return</param>
        /// <returns>Player's bid or null if the player hasn't bidded in this auction</returns>
        public Bid GetPlayerBid(IPlayer player)
        {
            try
            {
                return bids[player];
            } catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Check if all players have made their bid in the auction, including £0 bids.
        /// </summary>
        /// <returns>True if all players have bidded, false otherwise</returns>
        public bool FinishedBidding()
        {
            foreach (IPlayer player in biddingPlayers)
            {
                Bid bid;
                try
                {
                    bid = bids[player];
                } catch (Exception)
                {
                    bid = null;
                }

                if (bid == null)   // if any player hasn't bidded, return false
                {
                    return false;
                } 
            }

            return true;
        }

        /// <summary>
        /// Return the highest bidding player in this auction. Returns null if no
        /// players have made bids more than £0.
        /// </summary>
        /// <returns>Highest bidding player</returns>
        public IPlayer GetHighestBidder()
        {
            IPlayer highestBidder = null;
            int highestBid = 0;
             
            foreach (KeyValuePair<IPlayer, Bid> bid in bids)
            {
                int bidAmount = bid.Value.GetBidAmount();
                // bids must be bigger than £1 to be considered
                if (bidAmount > highestBid && bidAmount >= startingBid)
                {
                    highestBidder = bid.Key;
                    highestBid = bidAmount;
                }
            }

            return highestBidder;
        }

        /// <summary>
        /// Return the property for sale in this auction
        /// </summary>
        /// <returns>Property for sale</returns>
        public IProperty GetProperty()
        {
            return this.property;
        }


    }

    /// <summary>
    /// Exception class for Auction-related errors.
    /// </summary>
    public class AuctionException : Exception
    {
        public AuctionException(string message) : base(message)
        {
        }
    }

}
