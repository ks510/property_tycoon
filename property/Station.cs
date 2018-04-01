using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class Station : IProperty
    {
        private IPlayer owner;
        private string stationName;
        private int price;
        private int[] rentTable;
        private bool mortgaged;

        /// <summary>
        /// Constructor for a Station property.
        /// </summary>
        /// <param name="stationName">Name of station</param>
        /// <param name="price">Value of station</param>
        public Station(string stationName, int price)
        {
            this.owner = null;
            this.stationName = stationName;
            this.price = price;
            this.rentTable = new int[] { 25, 50, 100, 200 };
            this.mortgaged = false;
        }

        /// <summary>
        /// Return the current cost of rent on the station, based on the total number
        /// of stations owned by the owner of this station. Rent is always 0 when either
        /// of the following conditions occur:
        /// - Station is mortgaged OR
        /// - Owner is in jail
        /// </summary>
        /// <returns>Current cost of rent</returns>
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
                //TODO: implement player and test check number of stations owned
                return rentTable[owner.GetNumberOfStations() - 1];
            }
        }

        /// <summary>
        /// Return the current owner of the station.
        /// </summary>
        /// <returns>Player that currently owns the station.</returns>
        public IPlayer GetOwner()
        {
            return this.owner;
        }

        /// <summary>
        /// Change the ownership of this station to the given player.
        /// </summary>
        /// <param name="player">New owner of the station.</param>
        public void SetOwner(IPlayer player)
        {
            this.owner = player;
        }

        /// <summary>
        /// Return the original cash price of the station.
        /// </summary>
        /// <returns>Price of station.</returns>
        public int GetPrice()
        {
            return this.price;
        }

        /// <summary>
        /// Return the total worth of the station. If the station is mortgaged, half of its
        /// original cash price is returned instead.
        /// </summary>
        /// <returns>Total cash value of station.</returns>
        public int CalculateTotalValue()
        {
            if(this.mortgaged)
            {
                return this.price / 2;
            } else
            {
                return this.price;
            }

        }

        /// <summary>
        /// Check if the station is mortgaged or not.
        /// </summary>
        /// <returns>True if station is mortgaged, false otherwise.</returns>
        public bool IsMortgaged()
        {
            return this.mortgaged;
        }

        /// <summary>
        /// Mortgage the current station.
        /// </summary>
        public void Mortgage()
        {
            this.mortgaged = true;
        }

        /// <summary>
        /// Unmortgage the current station.
        /// </summary>
        public void Unmortgage()
        {
            this.mortgaged = false;
        }

        /// <summary>
        /// Check if the station is developable. Always returns false.
        /// </summary>
        /// <returns>False</returns>
        public bool IsDevelopable()
        {
            return false;
        }

        /// <summary>
        /// Check if the station can be sold. Always returns true because stations
        /// are undevelopable.
        /// </summary>
        /// <returns>True</returns>
        public bool CanSellProperty()
        {
            return true;
        }

        /// <summary>
        /// Sell the station to the bank for cash. Station becomes unowned.
        /// </summary>
        /// <returns>Cash value from sale of station. If station is mortgaged, half of the original
        /// price is returned instead.</returns>
        public int SellPropertyToBank()
        {
            // sold property becomes unowned
            this.owner = null;

            // return value of property depending on mortgaged or not
            if(mortgaged)
            {
                // reset mortgage state
                this.Unmortgage();
                return (this.price / 2);
            } else
            {
                return this.price;
            }
        }

        /// <summary>
        /// Return the string name of this station.
        /// </summary>
        /// <returns>name of station.</returns>
        public string GetPropertyName()
        {
            return this.stationName;
        }

        /// <summary>
        /// Return the table used to calculate the amount of rent to pay based on the total 
        /// number of stations owned by the owner.
        /// </summary>
        /// <returns>Rent table</returns>
        public int[] GetRentTable()
        {
            return this.rentTable;
        }



        


    }
}
