using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class HumanPlayerTest
    {
 
        
        [TestMethod]
        public void CreateHumanPlayer()
        {
            // assert human objects are initialised correctly
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Boot);
            Assert.AreEqual(1500, player.PeekCash()); // player starts with £1500 cash
            Assert.AreEqual(0, player.GetCurrentSpace()); // player starts at "Go" space (0)
            Assert.IsFalse(player.InJail()); // player never starts in jail
            Assert.AreEqual(0, player.GetTurnsInJail()); // player never went to jail yet
            Assert.AreEqual(0, player.GetJailFreeCards()); // does not own any jail free cards at the start
            Assert.AreEqual(0, player.GetPropertiesOwned().Count); // no properties owned yet
            Assert.AreEqual(0, player.GetNumberOfStations()); // no stations owned yet
            Assert.AreEqual(0, player.GetNumberOfUtilities()); // no utilities owned yet
            Assert.AreEqual(0, player.GetDoublesRolled()); // no doubles rolled, player hasn't taken a turn yet
            Assert.IsFalse(player.RolledDouble()); // player hasn't taken a turn yet
            Assert.IsFalse(player.IsBankrupt()); // can't be bankrupt at the start of the game
            Assert.IsFalse(player.HasPassedFirstGo()); // not yet completed one circuit of the board
            Assert.AreEqual("Bob", player.GetPlayerName()); // correct name stored
            //Assert.AreEqual(1, player.GetPlayerID()); // correct player ID stored
            Assert.AreEqual(Token.Boot, player.GetToken()); // correct token stored
        }
        

        [TestMethod]
        public void PlayerRollsDice()
        {
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Boot);

            // sum of 2 six-sided dice is always 2 <= sum <= 12
            // test rolling dice 10 times
            for (int i = 0; i < 10; i++)
            {
                int sumDice = player.RollDice();
                Assert.IsTrue(sumDice >= 2 && sumDice <= 12);
                Assert.IsTrue(sumDice >= 2 && sumDice <= 12);
            }

            // test if a double is rolled, assert correct variables are updated?
            
        }

        [TestMethod]
        public void PlayerMoveToken()
        {
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Boot);

            int oldLocation, spaces, newLocation;
            // test rolling and moving the player token 10 times
            for (int i = 0; i < 10; i++)
            {
                oldLocation = player.GetCurrentSpace();
                
                // roll dice to determine number of spaces to move
                spaces = player.RollDice();
                player.MoveToken(spaces);

                // assert player has moved correct number of spaces and within board range
                newLocation = player.GetCurrentSpace();
                Assert.AreEqual((oldLocation + spaces) % 40, newLocation);
                Assert.IsTrue(newLocation >= 0 && newLocation < 40);

                Console.WriteLine("Bob was at space: {0} and rolled {1}. Bob is now at space {2}", oldLocation, spaces, newLocation);
            }
        }

        [TestMethod]
        public void PlayerMoveToSpace()
        {
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Boot);

            int newSpace = 29;
            player.MoveToSpace(newSpace);
            // player moves to correct space
            Assert.AreEqual(newSpace, player.GetCurrentSpace());

            newSpace = 0;
            // player moves to correct space
            Assert.AreEqual(newSpace, player.GetCurrentSpace());
        }

        [TestMethod]
        public void Player_GoToJail()
        {
            // player starts at space 0
            HumanPlayer player = new HumanPlayer("Helen", 1, Token.Cat);
            Assert.AreEqual(0, player.GetCurrentSpace());
            int jailSpace = 31;
            player.GoToJail(jailSpace);
            // check player is currently at jail space
            Assert.AreEqual(jailSpace, player.GetCurrentSpace());
            // check player status is in jail
            Assert.IsTrue(player.InJail());

            HumanPlayer player2 = new HumanPlayer("Happy", 2, Token.Goblet);
            // player moves somewhere on the board
            player2.MoveToken(player2.RollDice());
            // send player to jail
            player2.GoToJail(jailSpace);
            // check player is currently at jail space
            Assert.AreEqual(jailSpace, player2.GetCurrentSpace());
            // check player status is in jail
            Assert.IsTrue(player2.InJail());

            // releaseing player from jail
            player.ReleaseFromJail();
            Assert.IsFalse(player.InJail());
            Assert.AreEqual(0, player.GetTurnsInJail());
        }

        [TestMethod]
        public void Player_TurnsInJail()
        {
            // incrementing player's turns in jail and reseting to 0
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            Assert.AreEqual(0, player.GetTurnsInJail());
            player.IncrementTurnsInJail();
            Assert.AreEqual(1, player.GetTurnsInJail());
            player.IncrementTurnsInJail();
            Assert.AreEqual(2, player.GetTurnsInJail());
            player.IncrementTurnsInJail();
            Assert.AreEqual(3, player.GetTurnsInJail());
            player.ResetTurnsInJail();
            Assert.AreEqual(0, player.GetTurnsInJail());
            // may need to implement safety checks i.e. does player need to be in jail to increment this variable?

        }

        [TestMethod]
        public void Player_JailCardMethods()
        {
            // player has no jail free cards at the start
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            Assert.AreEqual(0, player.GetJailFreeCards());
            player.UseJailFreeCard();
            // nothing happens if player tries to use a jail free card when there are none
            Assert.AreEqual(0, player.GetJailFreeCards());
            // player gets a jail free card
            player.AddJailFreeCard();
            Assert.AreEqual(1, player.GetJailFreeCards());
            Assert.IsTrue(player.HasJailFreeCard());
            // player uses jail free card
            player.UseJailFreeCard();
            Assert.AreEqual(0, player.GetJailFreeCards());
        }

        [TestMethod]
        public void Player_Bankrupt()
        {
            // player starts off not bankrupt
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            Assert.IsFalse(player.IsBankrupt());
            // player goes bankrupt
            player.SetBankrupt();
            Assert.IsTrue(player.IsBankrupt());

            // bankrupt players will never be unbankrupted
        }

        [TestMethod]
        public void Player_CashMethods()
        {
            // player starts off with 1500 cash
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            Assert.AreEqual(1500, player.PeekCash());

            // player loses £500
            player.DeductCash(500);
            Assert.AreEqual(1000, player.PeekCash());

            // player receives £200
            player.ReceiveCash(200);
            Assert.AreEqual(1200, player.PeekCash());

            // player loses all cash
            int cash = player.EmptyCash();
            Assert.AreEqual(1200, cash);
            Assert.AreEqual(0, player.PeekCash());


        }

        [TestMethod]
        public void Player_BuyProperty()
        {
            Station falmer = new Station("Falmer Station", 200);
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            Assert.AreEqual(1500, player.PeekCash());

            player.BuyProperty(falmer);
            // property price deducted from player's cash
            Assert.AreEqual(1500 - 200, player.PeekCash());
            // check property is stored in player's possession
            List<IProperty> properties = player.GetPropertiesOwned();
            Assert.IsTrue(properties.Contains(falmer));
            // owner of property is updated correctly
            Assert.AreEqual(player,falmer.GetOwner());
        }

        [TestMethod]
        public void Player_SellProperty()
        {
            // player buys falmer station for £200
            Station falmer = new Station("Falmer Station", 200);
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            Assert.AreEqual(1500, player.PeekCash());
            Assert.AreEqual(1, player.GetNumberOfStations()); // check numebr of stations incremented correctly
            player.BuyProperty(falmer);

            // player sells falmer station for £200
            player.SellProperty(falmer);
            Assert.AreEqual(1500, player.PeekCash());
            Assert.IsNull(falmer.GetOwner());
            Assert.IsFalse(falmer.IsMortgaged());
            Assert.IsFalse(player.GetPropertiesOwned().Contains(falmer));
        }

        [TestMethod]
        public void Player_SellMortgagedProperty()
        {
            // player buys edison waters property for £300 & mortgages it
            Utility edison = new Utility("Edison Waters", 300);
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            player.BuyProperty(edison);
            player.Mortgage(edison);
            Assert.AreEqual(1350, player.PeekCash());
            Assert.IsTrue(edison.IsMortgaged());

            // player sells the property and receives half of its value due to mortaged state
            // property should be unmortgaged after sale and unowned
            player.SellProperty(edison);
            Assert.AreEqual(1500, player.PeekCash());
            Assert.IsNull(edison.GetOwner());
            Assert.IsFalse(edison.IsMortgaged());
        }

        [TestMethod]
        public void Player_MortgageProperty()
        {
            // player buys edison waters property for £300
            Utility edison = new Utility("Edison Waters", 300);
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Smartphone);
            player.BuyProperty(edison);

            // player mortgages property & receives cash £150 from mortgage
            player.Mortgage(edison);
            Assert.AreEqual(150, edison.CalculateTotalValue());
            Assert.AreEqual(1350, player.PeekCash());
            Assert.IsTrue(edison.IsMortgaged());

            // player unmortgages property & pays £150
            player.Unmortgage(edison);
            Assert.IsFalse(edison.IsMortgaged());
            Assert.AreEqual(300, edison.CalculateTotalValue());
            Assert.AreEqual(1200, player.PeekCash());

        }

        [TestMethod]
        public void Player_TradeProperty()
        {
            DevelopableLand crapperStreet = new DevelopableLand("Crapper Street", 100, Colour.Brown, new int[] { 20, 20, 40, 60, 40 });
            DevelopableLand gangsters = new DevelopableLand("Gangsters Paradise", 100, Colour.Brown, new int[] { 20, 20, 40, 60, 40 });
            Station lewes = new Station("Lewes Station", 200);
            HumanPlayer sarah = new HumanPlayer("Sarah", 0, Token.Boot);
            HumanPlayer bob = new HumanPlayer("Bob", 1, Token.Hatstand);

            // sarah buys crapper street, gangsters paradise and bob buys lewes station
            sarah.BuyProperty(crapperStreet);
            sarah.BuyProperty(gangsters);
            bob.BuyProperty(lewes);

            // assume bob and sarah agree to trade 2 brown properties for lewes station
            // trade method only handles updated player property lists and station/utility counts
            List<IProperty> tradeOff = new List<IProperty>() { crapperStreet, gangsters };
            List<IProperty> tradeReturn = new List<IProperty>() { lewes };
            sarah.TradeProperties(tradeOff, tradeReturn);
            bob.TradeProperties(tradeReturn, tradeOff);

            // check player's owned property lists are updated correctly
            Assert.IsTrue(bob.GetPropertiesOwned().Contains(crapperStreet));
            Assert.IsTrue(bob.GetPropertiesOwned().Contains(gangsters));
            Assert.IsFalse(bob.GetPropertiesOwned().Contains(lewes));
            Assert.AreEqual(0, bob.GetNumberOfStations()); // check stations count updated

            Assert.IsFalse(sarah.GetPropertiesOwned().Contains(crapperStreet));
            Assert.IsFalse(sarah.GetPropertiesOwned().Contains(gangsters));
            Assert.IsTrue(sarah.GetPropertiesOwned().Contains(lewes));
            Assert.AreEqual(1, sarah.GetNumberOfStations()); // check stations count updated

            // ensure owner of property objects updated correctly
            Assert.AreEqual(bob, crapperStreet.GetOwner());
            Assert.AreEqual(bob, gangsters.GetOwner());
            Assert.AreEqual(sarah, lewes.GetOwner());

            // attempting to trade property that isn't owned by that player throws exception
            try
            {
                sarah.TradeProperties(tradeOff, tradeReturn);
            }
            catch (HumanPlayerException e)
            {
                Console.WriteLine(e.Message);
                Assert.AreEqual(e.Message, "Cannot trade a property because the player doesn't own it.");
            }
            

        }

        [TestMethod]
        public void Player_BuysStationsUtilities()
        {

            Station brighton = new Station("Brighton Station", 200);
            Station falmer = new Station("Falmer Station", 200);
            Utility edison = new Utility("Edison Waters", 100);
            Utility hydra = new Utility("Hydra Centre", 100);

            // player starts with 0 stations and 0 utilities
            HumanPlayer player = new HumanPlayer("Lola", 0, Token.Smartphone);
            Assert.AreEqual(0, player.GetNumberOfStations());
            Assert.AreEqual(0, player.GetNumberOfUtilities());
            // player buy two stations
            player.BuyProperty(brighton);
            Assert.AreEqual(1, player.GetNumberOfStations());
            player.BuyProperty(falmer);
            Assert.AreEqual(2, player.GetNumberOfStations());

            // player buys two utilities
            player.BuyProperty(edison);
            Assert.AreEqual(1, player.GetNumberOfUtilities());
            player.BuyProperty(hydra);
            Assert.AreEqual(2, player.GetNumberOfUtilities());

            // player sells stations
            player.SellProperty(brighton);
            Assert.AreEqual(1, player.GetNumberOfStations());
            player.SellProperty(falmer);
            Assert.AreEqual(0, player.GetNumberOfStations());

            // player sells utilities
            player.SellProperty(edison);
            Assert.AreEqual(1, player.GetNumberOfUtilities());
            player.SellProperty(hydra);
            Assert.AreEqual(0, player.GetNumberOfUtilities());
        }

        [TestMethod]
        public void Player_CheckOwnedColours()
        {
            HumanPlayer player = new HumanPlayer("Bob", 0, Token.Boot);
            // brown group test (2 properties)
            Assert.IsFalse(player.OwnsAllColour(Colour.Brown)); // player owns 0/2 brown properties
            DevelopableLand crapperStreet = new DevelopableLand("Crapper Street", 100, Colour.Brown, new int[] { 20, 20, 40, 60, 40 });
            DevelopableLand gangsters = new DevelopableLand("Gangsters Paradise", 100, Colour.Brown, new int[] { 20, 20, 40, 60, 40 });

            player.BuyProperty(crapperStreet); // player owns 1/2 brown properties
            Assert.IsFalse(player.OwnsAllColour(Colour.Brown)); 

            player.BuyProperty(gangsters); // player owns 2/2 brown properties
            Assert.IsTrue(player.OwnsAllColour(Colour.Brown));

            // player sells a brown property
            player.SellProperty(crapperStreet);
            Assert.IsFalse(player.OwnsAllColour(Colour.Brown));

            // red group test (3 properties)
            Assert.IsFalse(player.OwnsAllColour(Colour.Red)); // player owns 0/3 red properties
            DevelopableLand yueFei = new DevelopableLand("Yue Fei Square", 10, Colour.Red, new int[] { 20, 20, 40, 60, 40 });
            DevelopableLand mulan = new DevelopableLand("Mulan Rouge", 10, Colour.Red, new int[] { 20, 20, 40, 60, 40 });
            DevelopableLand hanXin = new DevelopableLand("Han Xin Gardens", 10, Colour.Red, new int[] { 20, 20, 40, 60, 40 });

            player.BuyProperty(yueFei);
            Assert.IsFalse(player.OwnsAllColour(Colour.Red)); // player owns 1/3 red properties

            player.BuyProperty(mulan);
            Assert.IsFalse(player.OwnsAllColour(Colour.Red)); // player owns 2/3 red properties

            player.BuyProperty(hanXin);
            Assert.IsTrue(player.OwnsAllColour(Colour.Red)); // player owns 3/3 red properties

        }
    }

}
