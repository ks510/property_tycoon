using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class Utility
    {
        private IPlayer owner;
        private string utilityName;
        private int price;
        private int[] multipliers;
        private bool mortgaged;

        /// <summary>
        /// Constructor for a Utility property.
        /// </summary>
        /// <param name="utilityName">Name of station</param>
        /// <param name="price">Value of station</param>
        public Utility(string utilityName, int price)
        {
            this.owner = null;
            this.utilityName = utilityName;
            this.price = price;
            this.multipliers = new int[] { 4, 10 };
            this.mortgaged = false;
        }

        /// <summary>
        /// Return the current multipler for the cost of rent on the utility, based on the
        /// total number of utilies owned by the owner of this utility. Rent is always 0 
        /// when either of the following conditions occur:
        /// - Utility is mortgaged OR
        /// - Owner is in jail
        /// </summary>
        /// <returns>Rent multipler</returns>
        public int GetRent()
        {
            if (this.mortgaged)
            {
                return 0;

            }
            else if (owner.InJail())
            {
                return 0;
            }
            else
            {
                //TODO: implement player and test check number of utilities owned
                return multipliers[owner.GetNumberOfUtilities() - 1];
            }
        }

        /// <summary>
        /// Return the current owner of the utility.
        /// </summary>
        /// <returns>Player that currently owns the utility.</returns>
        public IPlayer GetOwner()
        {
            return this.owner;
        }

        /// <summary>
        /// Change the ownership of this utility to the given player.
        /// </summary>
        /// <param name="player">New owner of the utility.</param>
        public void SetOwner(IPlayer player)
        {
            this.owner = player;
        }

        /// <summary>
        /// Return the original cash price of this utility.
        /// </summary>
        /// <returns>Price of utility.</returns>
        public int GetPrice()
        {
            return this.price;
        }

        /// <summary>
        /// Return the total worth of the utility. If the utility is mortgaged, half of its
        /// original cash price is returned instead.
        /// </summary>
        /// <returns>Total cash value of station.</returns>
        public int CalculateTotalValue()
        {
            if (this.mortgaged)
            {
                return this.price / 2;
            }
            else
            {
                return this.price;
            }

        }

        /// <summary>
        /// Check if this utility is mortgaged or not.
        /// </summary>
        /// <returns>True if utility is mortgaged, false otherwise.</returns>
        public bool IsMortgaged()
        {
            return this.mortgaged;
        }

        /// <summary>
        /// Mortgage the current utility.
        /// </summary>
        public void Mortgage()
        {
            this.mortgaged = true;
        }

        /// <summary>
        /// Unmortgage the current utility.
        /// </summary>
        public void Unmortgage()
        {
            this.mortgaged = false;
        }

        /// <summary>
        /// Check if the utility is developable. Always returns false.
        /// </summary>
        /// <returns>False</returns>
        public bool IsDevelopable()
        {
            return false;
        }

        /// <summary>
        /// Check if the utility can be sold. Always returns true because utilities
        /// are undevelopable.
        /// </summary>
        /// <returns>True</returns>
        public bool CanSellProperty()
        {
            return true;
        }

        /// <summary>
        /// Sell the utility to the bank for cash. Utility becomes unowned.
        /// </summary>
        /// <returns>Cash value from sale of station. If station is mortgaged, half of the original
        /// price is returned instead.</returns>
        public int SellPropertyToBank()
        {
            // sold property becomes unowned
            this.owner = null;

            // return value of property depending on mortgaged or not
            if (mortgaged)
            {
                // reset mortgage state
                this.Unmortgage();
                return (this.price / 2);
            }
            else
            {
                return this.price;
            }
        }

        /// <summary>
        /// Return the string name of this utility.
        /// </summary>
        /// <returns>Name of utility.</returns>
        public string GetPropertyName()
        {
            return this.utilityName;
        }

        /// <summary>
        /// Return the table used to lookup the rent multiplier.
        /// </summary>
        /// <returns>Multiplier table</returns>
        public int[] GetMultiplers()
        {
            return this.multipliers;
        }
    }
}
