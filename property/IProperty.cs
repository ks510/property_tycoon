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
        /// Return the current cost of rent on this property. Rent is always 0 if the property is unowned
        /// OR the owner is in jail OR the property is mortgaged. For utilities, this method returns the
        /// multipler rather than literal rent amount.
        /// </summary>
        /// <returns>Current cost of rent</returns>
        int GetRent();

        /// <summary>
        /// Return the current player that owns this property.
        /// </summary>
        /// <returns>Owner (player)</returns>
        IPlayer GetOwner();

        /// <summary>
        /// Update the owner of this property to the given player or null if unowned.
        /// </summary>
        /// <param name="player">New owner of the property.</param>
        void SetOwner(IPlayer player);

        /// <summary>
        /// Get the original cash price of the property (undeveloped). This value is unaffected
        /// by the mortgage status of the property.
        /// </summary>
        /// <returns>Price of property</returns>
        int GetPrice();

        /// <summary>
        /// Calculate the current total worth of the property, including any developments
        /// and mortgage status.
        /// </summary>
        /// <returns>Total worth of property</returns>
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
        /// Check if this property type can be developed by players or not. Only stations
        /// and utilities cannot be developed in Property Tycoon.
        /// </summary>
        /// <returns>True if property type is developable, false otherwise.</returns>
        bool IsDevelopable();

        /// <summary>
        /// Check if the property can be sold. Note that Stations and Utilities can always 
        /// be sold as they are undevelopable properties.
        /// </summary>
        /// <returns>True if property is eligible to be sold, false otherwise</returns>
        bool CanSellProperty();

        /// <summary>
        /// Sell the current property to the bank (property becomes unowned).
        /// </summary>
        /// <returns>Cash value of the property.</returns>
        int SellPropertyToBank();

        /// <summary>
        /// Return the name of the current property.
        /// </summary>
        /// <returns>Name of property</returns>
        string GetPropertyName();


    }
}
