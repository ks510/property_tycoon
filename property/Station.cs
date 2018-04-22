using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents a Station property in Property Tycoon. Stores all information about this
    /// Station including a fixed rent table (£25, £50, £100, £200) with respect to the 
    /// total number of stations owned by the owner.
    /// </summary>
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

        /// <see cref="IProperty.GetRent"/>
        public int GetRent()
        {
            // rent is £0 if property is unowned, mortgaged or owner is jailed
            if (this.owner == null || this.mortgaged || owner.InJail())
            {
                return 0;
            } 
            else // return rent based on the number of stations owned by the owner in total
            {
                return rentTable[owner.GetNumberOfStations() - 1];
            }
        }

        /// <see cref="IProperty.GetOwner"/>
        public IPlayer GetOwner()
        {
            return this.owner;
        }
    
        /// <see cref="IProperty.SetOwner"/>
        public void SetOwner(IPlayer player)
        {
            this.owner = player;
        }

        /// <see cref="IProperty.GetPrice"/>
        public int GetPrice()
        {
            return this.price;
        }

        /// <see cref="IProperty.CalculateTotalValue"/>
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

        /// <see cref="IProperty.IsMortgaged"/>
        public bool IsMortgaged()
        {
            return this.mortgaged;
        }

        /// <see cref="IProperty.Mortgage"/>
        public void Mortgage()
        {
            this.mortgaged = true;
        }

        /// <see cref="IProperty.Unmortgage"/>
        public void Unmortgage()
        {
            this.mortgaged = false;
        }

        /// <see cref="IProperty.IsDevelopable"/>
        public bool IsDevelopable()
        {
            return false;
        }

        /// <see cref="IProperty.CanSellProperty"/>
        public bool CanSellProperty()
        {
            return true;
        }

        /// <see cref="IProperty.SellPropertyToBank"/>
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

        /// <see cref="IProperty.GetPropertyName"/>
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
