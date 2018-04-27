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

        [TestMethod]
        public void Auction_BiddingSameAmount()
        {
            HumanPlayer bob = new HumanPlayer("Bob", 0, Token.Boot);
            HumanPlayer sarah = new HumanPlayer("Sarah", 1, Token.Cat);
            List<IPlayer> bidding = new List<IPlayer> { bob, sarah };
            IProperty station = new Station("Falmer Station", 200);

            Auction stationAuction = new Auction(station, bidding);

            // bob bids £50 first
            stationAuction.PlaceBid(bob, new Bid(50, true));
            // sarah also bids £50 but not allowed
            try
            {
                stationAuction.PlaceBid(sarah, new Bid(50, true));
            }
            catch (AuctionException e)
            {
                Console.WriteLine(e.Message);
                Assert.AreEqual("Cannot bid the same amount as another player!", e.Message);
            }
            // can't place bid, try £51 instead
            stationAuction.PlaceBid(sarah, new Bid(51, true));
            Assert.AreEqual(sarah, stationAuction.GetHighestBidder());
        }

        [TestMethod]
        public void Auction_HasFinished()
        {
            HumanPlayer bob = new HumanPlayer("Bob", 0, Token.Boot);
            HumanPlayer sarah = new HumanPlayer("Sarah", 1, Token.Cat);
            HumanPlayer tom = new HumanPlayer("Tom", 2, Token.Smartphone);
            HumanPlayer hope = new HumanPlayer("Hope", 3, Token.Goblet);
            List<IPlayer> bidding = new List<IPlayer> { bob, sarah, tom };
            IProperty station = new Station("Falmer Station", 200);

            Auction stationAuction = new Auction(station, bidding);
            // bob and sarah have bidded
            stationAuction.PlaceBid(bob, new Bid(100, true));
            stationAuction.PlaceBid(sarah, new Bid(200, true));
            // checking if bidding has finished
            Assert.IsFalse(stationAuction.FinishedBidding());

            // tom finally bids, but types £5000 instead of £500 (he only has £1500)
            try
            {
                stationAuction.PlaceBid(tom, new Bid(5000, true));
            }
            catch (AuctionException e)
            {
                Console.WriteLine(e.Message);
                Assert.AreEqual("Insufficient cash to make this bid!", e.Message);
            }
            // tom bids £500 correctly this time!
            stationAuction.PlaceBid(tom, new Bid(500, true));
            // all players have bidded, auction is finished, tom wins
            Assert.IsTrue(stationAuction.FinishedBidding());
            Assert.AreEqual(tom, stationAuction.GetHighestBidder());


        }
    }
}
