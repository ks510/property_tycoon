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
            // implements IProperty interface
            Assert.IsTrue(utility is IProperty);
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
            Utility utility1 = new Utility("Tesla Power Co", 150);
            Utility utility2 = new Utility("Edison Waters", 150);
            Assert.AreEqual(0, utility1.GetRent()); //unowned utilities, rent multiplier = 0x
            IPlayer player = new HumanPlayer("Paul", 0, Token.Cat);

            // player buys 1 utility, rent multipler = 4x
            player.BuyProperty(utility1);
            Assert.AreEqual(4, utility1.GetRent());

            // player goes to jail, rent multipler = 0x
            player.GoToJail(31);
            Assert.AreEqual(0, utility1.GetRent());

            // player released from jail and buys another utility
            player.ReleaseFromJail();
            player.BuyProperty(utility2);
            Assert.AreEqual(10, utility1.GetRent());
            Assert.AreEqual(10, utility2.GetRent());

            // player mortgages a utility, rent multipler = 0x for that utility
            player.Mortgage(utility1);
            Assert.AreEqual(0, utility1.GetRent());

            // should the rent on other utility consider the mortgaged property or not?
            // current version includes mortgaged properties in rent calculation
            Assert.AreEqual(10, utility2.GetRent());
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
