using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void CreateBoard()
        {
            IBoardSpace[] boardSpaces = new IBoardSpace[] { new GoSpace(), new FreeParkingSpace() };
            Board board = new Board(boardSpaces);

            // correct spaces stored and retrieved
            IBoardSpace space = board.GetSpace(0);
            Assert.IsTrue(space is GoSpace);
            space = board.GetSpace(1);
            Assert.IsTrue(space is FreeParkingSpace);
            // free parking starts with £0
            Assert.AreEqual(0, board.PeekFreeParking());

        }

        [TestMethod]
        public void Board_FreeParking()
        {
            IBoardSpace[] boardSpaces = new IBoardSpace[40];
            Board board = new Board(boardSpaces);
            // free parking starts at £0
            Assert.AreEqual(0, board.PeekFreeParking());
            // add £50 to free parking
            board.AddToFreeParking(50);
            Assert.AreEqual(50, board.PeekFreeParking());
            // add another £200 to free parking
            board.AddToFreeParking(200);
            Assert.AreEqual(250, board.PeekFreeParking());

            // receive and empty free parking funds
            int funds = board.ReceiveFreeParking();
            Assert.AreEqual(250, funds);
            Assert.AreEqual(0, board.PeekFreeParking());
        }
    
    }

}
