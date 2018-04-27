using System.Collections.Generic;


namespace PropertyTycoonLibrary
{
    public interface IPlayer
    {
        /// <summary>
        /// Player rolls the dice for their turn and returns the sum of the dice. 
        /// If the player rolls a double, increment the total doubles rolled for this turn.
        /// The actual number rolled for each dice will not be stored.
        /// </summary>
        /// <returns>Sum of the two dice rolled.</returns>
        int RollDice();

        /// <summary>
        /// Check if the player rolled a double in the current turn.
        /// </summary>
        /// <returns></returns>
        bool HasRolledDouble();

        /// <summary>
        /// This player has rolled a double, update its state and increment the number of doubles
        /// rolled in this turn.
        /// </summary>
        void RolledDouble();

        /// <summary>
        /// Return the number of the board space the player is currently on.
        /// </summary>
        /// <returns></returns>
        int GetCurrentSpace();

        /// <summary>
        /// Move this player's token the given number of spaces on the board.
        /// Player moves forward if the number is positive and backwards if negative.
        /// </summary>
        /// <param name="spaces">Number of spaces to move</param>
        void MoveToken(int spaces);

        /// <summary>
        /// Move this player's token to the given space on the board. 
        /// </summary>
        /// <param name="spaceID"></param>
        void MoveToSpace(int spaceID);

        /// <summary>
        /// Reset the number of doubles rolled by the player in the current turn to 0.
        /// </summary>
        void ResetDoublesRolled();

        /// <summary>
        /// Return the number of doubles rolled by the player in the current turn.
        /// </summary>
        int GetDoublesRolled();

        /// <summary>
        /// Sends the player to jail.
        /// <paramref name="jailSpaceID"/>Board space ID of jail space</param>
        /// </summary>
        void GoToJail(int jailSpaceID);

        /// <summary>
        /// Release the player from jail. Player moves to the "Just Visiting".
        /// </summary>
        void ReleaseFromJail();

        /// <summary>
        /// Check if the player is currently in jail.
        /// </summary>
        /// <returns>True if player is in jail, false otherwise</returns>
        bool InJail();

        /// <summary>
        /// Increment the number of turns the player has spent in jail.
        /// </summary>
        void IncrementTurnsInJail();

        /// <summary>
        /// Return the number of turns the player has spent in jail.
        /// </summary>
        /// <returns>Number of turns in jail.</returns>
        int GetTurnsInJail();

        /// <summary>
        /// Use a jail free card if this player has at least one jail free card.
        /// </summary>
        void UseJailFreeCard();

        /// <summary>
        /// Player receives a Jail Free card.
        /// </summary>
        void AddJailFreeCard();

        /// <summary>
        /// Check if this player has at least one jail free card.
        /// </summary>
        /// <returns>True if player has at least one jail free card, false otherwise.</returns>
        bool HasJailFreeCard();

        /// <summary>
        /// Player becomes bankrupt and will not take anymore turns for the rest of the game.
        /// </summary>
        void SetBankrupt();

        /// <summary>
        /// Check if the player is bankrupt or not.
        /// </summary>
        /// <returns>True if player is bankrupt, false otherwise.</returns>
        bool IsBankrupt();

        /// <summary>
        /// This player buys the given property. The price of the property is deducted from the player's cash.
        /// </summary>
        /// <param name="property">Property to buy</param>
        void BuyProperty(IProperty property);

        /// <summary>
        /// This player sells the given property. The player receives the price of the property in cash or 
        /// half of the price if the property was mortaged.
        /// </summary>
        /// <param name="property">Property to sell</param>
        void SellProperty(IProperty property);

        /// <summary>
        /// Return all of the properties owned by this player.
        /// </summary>
        /// <returns>All properties owned by player.</returns>
        List<IProperty> GetPropertiesOwned();

        /// <summary>
        /// Update the player's list of currently owned properties to complete a trade. The owner of received
        /// properties will be updated as well as the number of stations/utilities owned by this player.
        /// </summary>
        /// <param name="tradedOff">List of player's properties given to another player in a trade.</param>
        /// <param name="received">List of properties received from another player in a trade.</param>
        void TradeProperties(List<IProperty> tradedOff, List<IProperty> received);
        
        /// <summary>
        /// Return the total number of stations owned by this player.
        /// </summary>
        /// <returns></returns>
        int GetNumberOfStations();

        /// <summary>
        /// Return the total number of utilities owned by this player.
        /// </summary>
        /// <returns></returns>
        int GetNumberOfUtilities();

        /// <summary>
        /// Return the total cash held by this player.
        /// </summary>
        /// <returns>Total cash held by player</returns>
        int PeekCash();

        /// <summary>
        /// Player receives the given amount of cash.
        /// </summary>
        /// <param name="amount">Amount of cash to receive</param>
        void ReceiveCash(int amount);

        /// <summary>
        /// Player's cash is decreased by the given amount.
        /// </summary>
        /// <param name="amount">Amount of cash to deduct</param>
        void DeductCash(int amount);

        /// <summary>
        /// Return the player's total cash held and reset it to 0.
        /// </summary>
        /// <returns></returns>
        int EmptyCash();

        /// <summary>
        /// Return the name of this player.
        /// </summary>
        /// <returns>Name of player</returns>
        string GetPlayerName();

        /// <summary>
        /// Return the token representing this player.
        /// </summary>
        /// <returns>Player's token</returns>
        Token GetToken();

        /// <summary>
        /// Mortgage the given property if the player owns it and receives half of the property's
        /// original value in cash.
        /// </summary>
        /// <param name="property">Property to mortgage</param>
        void Mortgage(IProperty property);

        /// <summary>
        /// Unmortgage the given property if it is mortgaged.
        /// </summary>
        /// <param name="property">Property to unmortgage</param>
        void Unmortgage(IProperty property);

        /// <summary>
        /// Check if this player owns all properties in the given colour group.
        /// </summary>
        /// <param name="group">Colour group of properties to check</param>
        /// <returns>True if this player owns all properties in this group or false otherwise.</returns>
        bool OwnsAllColour(Colour group);

        /// <summary>
        /// Check if this player has completed their first circuit of the board.
        /// </summary>
        /// <returns>True if the player has completed at least one circuit of board, false otherwise.</returns>
        bool HasPassedFirstGo();

        /// <summary>
        /// Update the player's status when they have completed their first circuit of the board.
        /// </summary>
        void SetPassedFirstGo();

        /// <summary>
        /// Player develops the property by purchasing the next house/hotel and pays the cash to bank.
        /// Throws exception if the player doesn't own this property, the player doesn't have
        /// enough cash to buy the development or the property has already reached maximum development.
        /// </summary>
        /// <param name="property">Property to develop</param>
        void DevelopProperty(DevelopableLand property);

        /// <summary>
        /// Player sells a house/hotel on the property and receives the original development cost
        /// paid back from the Bank. Throws exception if the player doesn't own this property.
        /// </summary>
        /// <param name="property">Property to undevelop</param>
        void UndevelopProperty(DevelopableLand property);
    }
}
