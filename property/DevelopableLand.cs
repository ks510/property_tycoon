using System;

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents a property in Property Tycoon where players can develop by buying houses
    /// and hotels. Stores all information about the property. Properties can only develop
    /// up to 1 hotel per property (equivalent of 5 houses)
    /// </summary>
    public class DevelopableLand : IProperty
    {
        private IPlayer owner;
        private string name;
        private int price;
        private int[] rentTable;
        private bool mortgaged;
        private Colour group;
        private int houses;

        /// <summary>
        /// Constructor for a developable property.
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="price">Cost of property</param>
        /// <param name="group">Colour group this property belongs to</param>
        /// <param name="rentTable">Rent amount according to the development level</param>
        public DevelopableLand(string name, int price, Colour group, 
                                                            int[] rentTable)
        {
            this.owner = null;
            this.name = name;
            this.price = price;
            this.mortgaged = false;
            this.group = group;
            this.houses = 0;

            // check rent table must have only 5 values (1, 2, 3, 4 houses and hotel rent cost)
            if (rentTable.Length == 6)
            {
                this.rentTable = rentTable;
            }
            else
            {
                throw new DevelopableLandException("Property initialised with incorrect rent table. Possibly game data file error.");
            }

        }

        /// <see cref="IProperty.GetRent"/>
        public int GetRent()
        {
            // rent is £0 if property is unowned, mortgaged or owner is jailed
            if (this.owner == null || this.mortgaged || owner.InJail())
            {
                return 0;
            }
            else // return rent based the development level of the property
            {
                return rentTable[houses];
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
            int value = 0;
            // add original value of property to total
            if(mortgaged)
            {
                value += (this.price / 2);
            }
            else
            {
                value += this.price;
            }
            // add the value of any developments to total
            int housePrice = this.GetDevelopCost();
            value += (this.houses * housePrice);

            return value;

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
            return true;
        }

        /// <see cref="IProperty.CanSellProperty"/>
        public bool CanSellProperty()
        {
            // property can only be sold if undeveloped
            if(houses > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <see cref="IProperty.SellPropertyToBank"/>
        public int SellPropertyToBank()
        {
            // check if the property is undeveloped before it can be sold
            if(this.CanSellProperty())
            {
                this.owner = null;
                if(mortgaged)
                {
                    this.Unmortgage();
                    return (this.price / 2);
                }
                else
                {
                    return this.price;
                }
            }
            else
            {
                throw new DevelopableLandException("Cannot sell the property while it is developed!");
            }
        }

        /// <see cref="IProperty.GetPropertyName"/>
        public string GetPropertyName()
        {
            return this.name;
        }

        /// <summary>
        /// Return the table used to calculate the amount of rent to pay based on the 
        /// development state of the property.
        /// <returns>Rent table</returns>
        public int[] GetRentTable()
        {
            return this.rentTable;
        }

        /// <summary>
        /// Return the cost of development per house for this property.
        /// </summary>
        /// <returns>Cost of development.</returns>
        public int GetDevelopCost()
        {
            return (int)this.group;
        }

        /// <summary>
        /// Return the development level of this property (5 houses = 1 hotel).
        /// </summary>
        /// <returns>Development level</returns>
        public int GetHouses()
        {
            return this.houses;
        }

        /// <summary>
        /// Check if this property has reached the maximum development level 
        /// (5 houses i.e. 1 hotel)
        /// </summary>
        /// <returns>False if maximum development not reached, true otherwise.</returns>
        public bool IsMaxDeveloped()
        {
            // property only developable if maximum development not reached
            if (this.houses < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Increase the development level by 1. Throws exception if the maximum development
        /// level has been reached.
        /// </summary>
        public void Develop()
        {
            if(IsMaxDeveloped())
            {
                throw new DevelopableLandException("Property has reached maximum development, cannot develop further.");
            }
            else
            {
                this.houses++;
            }
        }

        /// <summary>
        /// Remove a house/hotel from this property, decreasing the development level by 1.
        /// Throws exception if the property is undeveloped.
        /// </summary>
        public void Undevelop()
        {
            if (houses > 0)
            {
                this.houses--;
            }
            else
            {
                throw new DevelopableLandException("No developments on this property to sell.");
            }
        }

        /// <summary>
        /// Return the colour group
        /// </summary>
        /// <returns></returns>
        public Colour GetColourGroup()
        {
            return this.group;
        }

        
    }

    /// <summary>
    /// An exception class to indicate errors related to DevelopableLand.
    /// </summary>
    public class DevelopableLandException : Exception
    {
        public DevelopableLandException(string message) : base(message)
        {

        }
    }
}
