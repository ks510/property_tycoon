using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class HumanPlayer : IPlayer
    {
        private static bool debug = true; //debugging purposes

        private int cash;
        private int currentSpace;
        private bool inJail;
        private int turnsInJail;
        private int jailFreeCards;
        private List<IProperty> propertiesOwned;
        private int stations;
        private int utilities;
        private int doublesRolled;
        private bool rolledDouble;
        private bool bankrupt;
        private bool passedFirstGo;
        private string playerName;
        private int playerID;
        private Token token;

        /// <summary>
        /// Constructor for a human player in the Property Tycoon game.
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="playerID">Player ID</param>
        /// <param name="token">Token that represents this player</param>
        public HumanPlayer(string playerName, int playerID, Token token)
        {
            this.cash = 1500;
            this.currentSpace = 0;
            this.inJail = false;
            this.turnsInJail = 0;
            this.jailFreeCards = 0;
            this.propertiesOwned = new List<IProperty>();
            this.stations = 0;
            this.utilities = 0;
            this.doublesRolled = 0;
            this.rolledDouble = false;
            this.bankrupt = false;
            this.passedFirstGo = false;
            this.playerName = playerName;
            this.playerID = playerID;
            this.token = token;
        }

        public int RollDice()
        {
            int dice1 = Dice.RollDice();
            int dice2 = Dice.RollDice();

            if (debug)
            {
                Console.WriteLine(dice1);
                Console.WriteLine(dice2);
            }

            // check if player rolled double and update variables accordingly
            if (dice1 == dice2)
            {
                rolledDouble = true;
                doublesRolled++;

                if (debug)
                {
                    Console.WriteLine("Rolled Double, rolledDouble: {0}, doublesRolled: {1}", rolledDouble, doublesRolled);
                }
            }
            // return the sum of the player's dice rolls
            return dice1 + dice2;
        }

        public bool RolledDouble()
        {
            return this.rolledDouble;
        }

        public int GetDoublesRolled()
        {
            return this.doublesRolled;
        }

        public void ResetDoublesRolled()
        {
            this.doublesRolled = 0;
        }

        public void MoveToken(int spaces)
        {
            currentSpace = (currentSpace + spaces) % 40;
        }

        public int GetCurrentSpace()
        {
            return this.currentSpace;
        }

        public void MoveToSpace(int spaceID)
        {
            this.currentSpace = spaceID;
        }

        public void GoToJail(int jailSpaceID)
        {
            this.inJail = true;
            this.currentSpace = jailSpaceID;
        }

        public bool InJail()
        {
            return this.inJail;
        }

        public void ReleaseFromJail()
        {
            this.inJail = false;
            this.turnsInJail = 0;
        }

        public void IncrementTurnsInJail()
        {
            this.turnsInJail++;
        }

        public int GetTurnsInJail()
        {
            return this.turnsInJail;
        }

        public void ResetTurnsInJail()
        {
            this.turnsInJail = 0;
        }

        public void UseJailFreeCard()
        {
            // if player has at least one card, use a card by decrementing
            if(jailFreeCards > 0)
            {
                jailFreeCards--;
            }
        }

        public int GetJailFreeCards()
        {
            return this.jailFreeCards;
        }

        public void AddJailFreeCard()
        {
            this.jailFreeCards++;
        }

        public bool HasJailFreeCard()
        {
            return jailFreeCards > 0;
        }

        public void SetBankrupt()
        {
            this.bankrupt = true;
        }

        public bool IsBankrupt()
        {
            return this.bankrupt;
        }

        public List<IProperty> GetPropertiesOwned()
        {
            return this.propertiesOwned;
        }


        public int PeekCash()
        {
            return this.cash;
        }

        public void ReceiveCash(int amount)
        {
            cash = cash + amount;
        }

        public void DeductCash(int amount)
        {
            cash = cash - amount;
        }

        public int EmptyCash()
        {
            int totalCash = this.cash;
            this.cash = 0;
            return totalCash;
        }

        public void BuyProperty(IProperty property)
        {
            DeductCash(property.GetPrice());
            property.SetOwner(this);
            propertiesOwned.Add(property);

            // if station or utility, update variables
            if (property.GetType() == typeof(Station))
            {
                stations++;
            }
            else if (property.GetType() == typeof(Utility))
            {
                utilities++;
            }
        }

        public void SellProperty(IProperty property)
        {
            ReceiveCash(property.CalculateTotalValue()); // player receives full or half value if mortgaged
            property.SetOwner(null); // property is unowned
            property.Unmortgage();   // ensures property is always unmortgaged after sale
            propertiesOwned.Remove(property); // property removed from player's possession

            // if station or utility, update variables
            if (property.GetType() == typeof(Station))
            {
                stations--;
            }
            else if (property.GetType() == typeof(Utility))
            {
                utilities--;
            }
        }

        public void Mortgage(IProperty property)
        {
            if (propertiesOwned.Contains(property) && !property.IsMortgaged())
            {
                property.Mortgage();
                ReceiveCash(property.GetPrice() / 2);   // half of property value received in cash excluding improvements
            }
        }

        public void Unmortgage(IProperty property)
        {
            if(propertiesOwned.Contains(property) && property.IsMortgaged())
            {
                DeductCash(property.GetPrice() / 2);    // pay half of property value to unmortgage
                property.Unmortgage();  // set property to unmortgaged
            }
        }

        public void TradeProperties(List<IProperty> tradedOff, List<IProperty> received)
        {
            // remove all properties traded off
            foreach (IProperty owned in tradedOff) {
                // check if this player actually owns the property they are trading off
                if (propertiesOwned.Contains(owned))
                {
                    propertiesOwned.Remove(owned);
                    // update counts if old property was a station or utility
                    if (owned.GetType() == typeof(Station))
                    {
                        stations--;
                    }
                    else if (owned.GetType() == typeof(Utility))
                    {
                        utilities--;
                    }
                }
                else // player cannot trade off a property they don't own!
                {
                    throw new HumanPlayerException("Cannot trade a property because the player doesn't own it.");
                }
            }

            // add new properties received from trade
            foreach (IProperty newProperty in received)
            {
                propertiesOwned.Add(newProperty);
                newProperty.SetOwner(this);

                // update counts if new property is a station or utility
                if (newProperty.GetType() == typeof(Station))
                {
                    stations++;
                }
                else if (newProperty.GetType() == typeof(Utility))
                {
                    utilities++;
                }
            }
        }

        public int GetNumberOfStations()
        {
            return this.stations;
        }

        public int GetNumberOfUtilities()
        {
            return this.utilities;
        }


        public string GetPlayerName()
        {
            return this.playerName;
        }

        public Token GetToken()
        {
            return this.token;
        }

        public bool OwnsAllColour(Colour group)
        {
            // check through all player's owned properties and count the number of [group] colour owned
            int groupCount = 0;
            foreach (IProperty property in propertiesOwned)
            {
                if (property is DevelopableLand)
                {
                    // if property colour matches the group colour we want, increment count
                    DevelopableLand land = (DevelopableLand)property;
                    if (land.GetColourGroup() == group)
                    {
                        groupCount++;
                    }
                }
            }
            // all groups have 3 properties except Brown and Deep Blue
            if (group == Colour.Brown || group == Colour.DeepBlue)
            {
                return groupCount == 2;
            } else
            {
                return groupCount == 3;
            }
            
        }

        public bool HasPassedFirstGo()
        {
            return this.passedFirstGo;
        }

        public void SetPassedFirstGo()
        {
            this.passedFirstGo = true;
        }
    }

    /// <summary>
    /// Exception class for HumanPlayer-related errors.
    /// </summary>
    public class HumanPlayerException : Exception
    {
        public HumanPlayerException(string message) : base(message)
        {
        }
    }
}
