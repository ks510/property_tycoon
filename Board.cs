using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents the board for Property Tycoon. The board manages all board spaces and
    /// the free parking funds. The board does not manage the card piles.
    /// </summary>
    public class Board
    {
        private IBoardSpace[] board;
        private int freeParkingFunds;

        /// <summary>
        /// Constructor for a Property Tycoon board with 40 spaces.
        /// </summary>
        /// <param name="boardSpaces">Board spaces</param>
        public Board(IBoardSpace[] boardSpaces)
        {
            this.board = boardSpaces;
            this.freeParkingFunds = 0; // free parking starts with £0
        }

        /// <summary>
        /// Return the space on the given number on the board.
        /// </summary>
        /// <param name="spaceID"></param>
        /// <returns></returns>
        public IBoardSpace GetSpace(int spaceID)
        {
            return board[spaceID];
        }

        /// <summary>
        /// Increase the total money on Free Parking by the given amount.
        /// </summary>
        /// <param name="amount">Money to add to Free Parking</param>
        public void AddToFreeParking(int amount)
        {
            freeParkingFunds = freeParkingFunds + amount;
        }

        /// <summary>
        /// Return the total money of Free Parking without emptying the funds.
        /// </summary>
        /// <returns>Total money</returns>
        public int PeekFreeParking()
        {
            return this.freeParkingFunds;
        }

        /// <summary>
        /// Return the total money on Free Parking and reset the funds to £0.
        /// </summary>
        /// <returns>Total money</returns>
        public int ReceiveFreeParking()
        {
            int funds = freeParkingFunds;
            freeParkingFunds = 0;

            return funds;
        }
    }

}
