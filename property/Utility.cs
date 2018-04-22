using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents a Utility property in Property Tycoon. Stores all information
    /// related to this Utility including fixed rent multiplers (4x, 10x).
    /// </summary>
    public class Utility : IProperty
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

        /// <see cref="IProperty.GetRent"/>
        public int GetRent()
        {
            // rent is £0 if property is unowned, mortgaged or owner is jailed
            if (this.owner == null || this.mortgaged || owner.InJail())
            {
                return 0;
            }
            else // return rent multiplier based on the number of utilities owned by the owner in total
            {
                return multipliers[owner.GetNumberOfUtilities() - 1];
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
            if (this.mortgaged)
            {
                return this.price / 2;
            }
            else
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

            // return value of property depending on mortgage status
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

        /// <see cref="GetPropertyName"/>
        public string GetPropertyName()
        {
            return this.utilityName;
        }

        /// <summary>
        /// Return the table used to lookup the rent multiplier for this Utility.
        /// </summary>
        /// <returns>Multiplier table</returns>
        public int[] GetMultiplers()
        {
            return this.multipliers;
        }
    }
}
