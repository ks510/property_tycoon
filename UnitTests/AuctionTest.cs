using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class AuctionTest
    {
        [TestMethod]
        public void AuctionBid()
        {
            HumanPlayer bob = new HumanPlayer("Bob", 0, Token.Boot);
            HumanPlayer sarah = new HumanPlayer("Sarah", 1, Token.Cat);
            HumanPlayer tom = new HumanPlayer("Tom", 2, Token.Smartphone);
            HumanPlayer hope = new HumanPlayer("Hope", 3, Token.Goblet);
            Bid bobBid = new Bid(50, true);
            Bid sarahBid = new Bid(100, true);
            Bid tomBid = new Bid(0, true);
            List<IPlayer> bidding = new List<IPlayer> { bob, sarah, tom };
            IProperty station = new Station("Falmer Station", 200);

            Auction stationAuction = new Auction(station, bidding);
            // correct property at auction
            Assert.AreEqual(station, stationAuction.GetProperty());
            // no bids yet, no highest bidder
            Assert.IsNull(stationAuction.GetHighestBidder());

            // hope tries to bid but not eligible
            try
            {
                stationAuction.PlaceBid(hope, new Bid(200, false));
            } catch (AuctionException e)
            {
                Console.WriteLine(e.Message);
                Assert.IsNull(stationAuction.GetPlayerBid(hope));
            }

            // Tom bids £0 (he doesn't want to bid)
            stationAuction.PlaceBid(tom, tomBid);
            // check correct bid placed
            Assert.AreEqual(tomBid, stationAuction.GetPlayerBid(tom));
            // not all players have bidded
            Assert.IsFalse(stationAuction.FinishedBidding());
            // tom doesn't want to bid, no highest bidder
            Assert.IsNull(stationAuction.GetHighestBidder());


            // Bob bids £50
            stationAuction.PlaceBid(bob, bobBid);
            // check correct bid amount placed
            Assert.AreEqual(bobBid, stationAuction.GetPlayerBid(bob));
            // not all players have bidded
            Assert.IsFalse(stationAuction.FinishedBidding());
            // bob is the highest bidder
            Assert.AreEqual(bob, stationAuction.GetHighestBidder());

            // Sarah bids £100
            stationAuction.PlaceBid(sarah, sarahBid);
            // check correct bid amount placed
            Assert.AreEqual(sarahBid, stationAuction.GetPlayerBid(sarah));
            // all players have bidded now
            Assert.IsTrue(stationAuction.FinishedBidding());
            // sarah is the highest bidder
            Assert.AreEqual(sarah, stationAuction.GetHighestBidder());

        }
    }
}
