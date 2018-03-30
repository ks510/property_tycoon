using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject

{    /// <summary>
     /// Represents an action where the player must pay for the cost of repairs
     /// of each house and hotel they currently own.
     /// </summary>
    public class PayRepairsAction : IAction
    {
        private int houseCost;
        private int hotelCost;

        /// <summary>
        /// Constructor for a repairs payment action.
        /// </summary>
        /// <param name="houseCost">Cost per house.</param>
        /// <param name="hotelCost">Cost per hotel.</param>
        public PayRepairsAction(int houseCost, int hotelCost)
        {
            this.houseCost = houseCost;
            this.hotelCost = hotelCost;
        }

        /// <summary>
        /// Return the cost of repairs per house.
        /// </summary>
        /// <returns>Cost per house.</returns>
        public int GetHouseCost()
        {
            return this.houseCost;
        }

        /// <summary>
        /// Return the cost of repairs per hotel.
        /// </summary>
        /// <returns>Cost per hotel.</returns>
        public int GetHotelCost()
        {
            return this.hotelCost;
        }
    }
}
