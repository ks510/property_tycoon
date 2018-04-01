using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class DevelopableLand : IProperty
    {
        private IPlayer owner;
        private string name;
        private int price;
        private int[] rentTable;
        private bool mortgaged;
        private Colour group;
        private int houses;

        public DevelopableLand(string name, int price, Colour group, 
                                                            int[] rentTable)
        {
            this.owner = null;
            this.name = name;
            this.price = price;
            // implement array length check = 6 and throw exception
            this.rentTable = rentTable;
            this.mortgaged = false;
            this.group = group;
            this.houses = 0;

        }

        /// <summary>
        /// Return the current cost of rent on this property, based on its development state.
        /// If the owner is in jail or the property is mortgaged, no rent is payable.
        /// </summary>
        /// <returns>Current cost of rent or 0 if owner is in jail or the property is mortgaged.</returns>
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
                return rentTable[houses];
            }
        }

        /// <summary>
        /// Return the player that currently owns this property.
        /// </summary>
        /// <returns>Owner player or null if unowned (i.e. owned by bank).</returns>
        public IPlayer GetOwner()
        {
            return this.owner;
        }

        /// <summary>
        /// Change the ownership of this property to the given player.
        /// </summary>
        /// <param name="player">New owner of property.</param>
        public void SetOwner(IPlayer player)
        {
            this.owner = player;
        }

        /// <summary>
        /// Return the original price of this property (undeveloped, not mortgaged).
        /// </summary>
        /// <returns>Original price of property</returns>
        public int GetPrice()
        {
            return this.price;
        }

        /// <summary>
        /// Calculate and return the total worth of this property, including any 
        /// developments made. If property is mortgaged, only half of its original
        /// price will be added to calculation.
        /// </summary>
        /// <returns>Total worth of property.</returns>
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

        /// <summary>
        /// Check if this property is mortgaged or not.
        /// </summary>
        /// <returns>True if property is mortgaged, false otherwise.</returns>
        public bool IsMortgaged()
        {
            return this.mortgaged;
        }

        /// <summary>
        /// Mortgage the current property.
        /// </summary>
        public void Mortgage()
        {
            this.mortgaged = true;
        }

        /// <summary>
        /// Unmortgage the current property.
        /// </summary>
        public void Unmortgage()
        {
            this.mortgaged = false;
        }

        /// <summary>
        /// Check if this property is a developable property.
        /// </summary>
        /// <returns>True if developable property, false otherwise.</returns>
        public bool IsDevelopable()
        {
            return true;
        }

        /// <summary>
        /// Check if this property can be sold.
        /// </summary>
        /// <returns>True if property is undeveloped, false otherwise.</returns>
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

        /// <summary>
        /// Sell the property to the bank for cash. Property becomes unowned.
        /// </summary>
        /// <returns>Cash value from sale of property. If property is mortgaged, half of the original
        /// price is returned instead.</returns>
        public int SellPropertyToBank()
        {
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
                return 0;
            }
        }

        /// <summary>
        /// Return the string name of this property.
        /// </summary>
        /// <returns>Name of property.</returns>
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
        /// Return the colour group of this property.
        /// </summary>
        /// <returns>Colour group</returns>
        public Colour GetGroup()
        {
            return this.group;
        }

        /// <summary>
        /// Return the cost of development per house for this property.
        /// </summary>
        /// <returns>Cost of development.</returns>
        public int GetDevelopCost()
        {
            return (int)this.group;
        }

        public int GetDevelopment()
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

        public bool Develop()
        {
            if(houses == 5)
            {
                return false;
            }
            //TODO: get all properties in the same group from PropertyTycoon static method
            //check player owns all properties in group
            //add house check development difference < 1 (difference between max and min developed property)
            //if ok, return
            //else, roll back changes and return false, cannot develop due to development difference

            return true;
        }

        public bool Undevelop()
        {
            if(houses == 0)
            {
                return false;
            }
            //TODO: get all properties in the same group from PropertyTycoon static method
            //check player owns all properties in group
            //add house check development difference < 1 (difference between max and min developed property)
            //if ok, return
            //else, roll back changes and return false, cannot undevelop due to development difference
            return true;
        }

        //TODO
        public int CheckDevelopmentDifference()
        {
            //Use static method in PropertyTycoon class to get array of properties in the same colour group
            //var properties = PropertyTycoon.GetPropertiesInGroup(this.colour);
            //for loop to check for min and max developed property
            //return difference value (max - min)
            return -1;
        }

        /// <summary>
        /// Add a house to this property, increasing the development level by 1.
        /// </summary>
        public void AddHouse()
        {
            if(IsMaxDeveloped())
            {
                //throw exception - property cannot be develop further
            } else
            {
                this.houses++;
            }
        }

        /// <summary>
        /// Remove a house/hotel from this property, decreasing the development level by 1.
        /// </summary>
        public void RemoveHouse()
        {
            this.houses--;
        }

        

        
    }
}
