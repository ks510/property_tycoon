using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Implementing classes represent a Property available for purchase 
    /// by players on the Property Tycoon game board.
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// Get the current cost of rent on the property.
        /// </summary>
        /// <returns>Cost of rent</returns>
        int GetRent();

        /// <summary>
        /// Get the current player that owns the property.
        /// </summary>
        /// <returns>Owner (player)</returns>
        IPlayer GetOwner();

        /// <summary>
        /// Update the owner of the property to the given player.
        /// </summary>
        /// <param name="player">New owner of the property.</param>
        void SetOwner(IPlayer player);

        /// <summary>
        /// Get the original cash price of the property (undeveloped).
        /// </summary>
        /// <returns>Price of property</returns>
        int GetPrice();

        /// <summary>
        /// Calculate the total worth of the property, including any houses or hotel.
        /// </summary>
        /// <returns>Total value of property</returns>
        int CalculateTotalValue();

        /// <summary>
        /// Check if a property is currently mortgaged or not.
        /// </summary>
        /// <returns>True if mortgaged, false otherwise.</returns>
        bool IsMortgaged();

        /// <summary>
        /// Mortgage the current property.
        /// </summary>
        void Mortgage();

        /// <summary>
        /// Unmortgaged the current property.
        /// </summary>
        void Unmortgage();

        /// <summary>
        /// Check if the property can be developed by players or not.
        /// </summary>
        /// <returns>True if property can be developed, false otherwise.</returns>
        bool IsDevelopable();

        /// <summary>
        /// Check if the property can be sold.
        /// </summary>
        /// <returns>True if property is eligible to be sold, false otherwise</returns>
        bool CanSellProperty();

        /// <summary>
        /// Sell the current property to the bank (property becomes unowned).
        /// </summary>
        /// <returns>Cash value of the property.</returns>
        int SellPropertyToBank();

        /// <summary>
        /// Get the name of the current property.
        /// </summary>
        /// <returns>Name of property</returns>
        string GetPropertyName();

    }
}
