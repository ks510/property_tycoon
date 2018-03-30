using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void InitialiseUtilityWithCorrectData()
        {
            Utility utility = new Utility("Edison Water", 150);

            // correct name
            Assert.AreEqual("Edison Water", utility.GetPropertyName());
            // correct price stored
            Assert.AreEqual(150, utility.GetPrice());

            // correct multiplers initialised
            int[] multiplers = new int[] { 4, 10 };
            var actualMultiplers = utility.GetMultiplers();
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(multiplers[i], actualMultiplers[i]);
            }

            // check initial owner null
            Assert.IsNull(utility.GetOwner());
            // check initialise unmortgaged
            Assert.IsFalse(utility.IsMortgaged());
        }

        [TestMethod]
        public void UtilityCorrectGetterMethods()
        {
            Utility utility = new Utility("Edison Waters", 150);

            // TODO: correct rent multiplier returned based on number of utilities owned

            // TODO: correct current owner player returned

            // correct price returned
            Assert.AreEqual(150, utility.GetPrice());

            // correct total value returned when unmortgaged
            Assert.AreEqual(150, utility.CalculateTotalValue());

            // utility is undevelopable so can always be sold
            Assert.IsTrue(utility.CanSellProperty());

            // correct utility name returned
            Assert.AreEqual("Edison Waters", utility.GetPropertyName());

            // utilities are undevelopable
            Assert.IsFalse(utility.IsDevelopable());

        }

        [TestMethod]
        public void UtilityCorrectRentMultiplierReturned()
        {
            //TODO: if player is in jail, rent = 0
            Utility utility = new Utility("Tesla Power Co", 150);
            //IPlayer player = new HumanPlayer();
            //utility.SetOwner(player)
            //player.GoToJail();
            //Assert.AreEqual(1, utility.GetRent());

            // if utility is mortgaged, rent = 0
            Utility utility2 = new Utility("Edison Waters", 150);
            utility2.Mortgage();
            Assert.IsTrue(utility2.IsMortgaged());
            Assert.AreEqual(0, utility2.GetRent());

            //TODO: if player owns x utilies, return correct rent:
            // 1 utility = 4
            // 2 utilities = 10
        }

        [TestMethod]
        public void SellUtility_Unmortgaged()
        {
            Utility utility = new Utility("Tesla Power Co", 200);
            // utility can always be sold
            Assert.IsTrue(utility.CanSellProperty());

            // unmortgaged utility sells for original price value (£150)
            int sellingPrice = utility.SellPropertyToBank();
            Assert.AreEqual(200, sellingPrice);
            // station is unowned after selling
            Assert.IsNull(utility.GetOwner());
        }

        [TestMethod]
        public void SellUtility_Mortgaged()
        {
            Utility utility = new Utility("Edison Waters", 150);
            utility.Mortgage();
            // mortgage utility
            Assert.IsTrue(utility.IsMortgaged());

            int sellingPrice = utility.SellPropertyToBank();
            // mortgaged utility sells for half of original price value
            Assert.AreEqual(150 / 2, sellingPrice);
            // utility is unowned after selling
            Assert.IsNull(utility.GetOwner());
            // utility is unmortgaged after sale
            Assert.IsFalse(utility.IsMortgaged());
        }
    }
}
