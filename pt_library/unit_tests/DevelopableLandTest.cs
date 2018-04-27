using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class DevelopableLandTest
    {
        [TestMethod]
        public void InitaliseDevelopableLandWithCorrectData()
        {
            int[] rentTable = new int[6] { 30, 60, 100, 150, 200, 320 };
            int price = 200;
            Colour group = Colour.Blue;
            DevelopableLand reyLane = new DevelopableLand("Rey Lane", price, group, rentTable);

            // correct name
            Assert.AreEqual("Rey Lane", reyLane.GetPropertyName());
            // correct price
            Assert.AreEqual(200, reyLane.GetPrice());
            // correct group
            Assert.AreEqual(Colour.Blue, reyLane.GetColourGroup());
            // correct rent table
            var actualRentTable = reyLane.GetRentTable();
            for(int i = 0; i < 5; i++)
            {
                Assert.AreEqual(rentTable[i], actualRentTable[i]);
            }
            // initially unowned
            Assert.IsNull(reyLane.GetOwner());
            // initially unmortgaged
            Assert.IsFalse(reyLane.IsMortgaged());
            // initially undeveloped
            Assert.AreEqual(0, reyLane.GetHouses());
            // implements IProperty interface
            Assert.IsTrue(reyLane is IProperty);
            
        }

        [TestMethod]
        public void DevelopableLand_CorrectRentReturned()
        {
            
            int[] rentTable = new int[6] { 10, 30, 90, 160, 250, 350 };
            DevelopableLand crapperStreet = new DevelopableLand("Crapper Street",
                                                60, Colour.Brown, rentTable);
            IPlayer player = new HumanPlayer("Bob", 0, Token.Goblet);
            // Bob buys crapper street, rent = £10
            player.BuyProperty(crapperStreet);
            Assert.AreEqual(10, crapperStreet.GetRent());

            // Bob goes to jail, rent = £0
            player.GoToJail(31);
            Assert.AreEqual(0, crapperStreet.GetRent());

            // Bob is released from jail and develops property by 1 house, rent = £30
            // note: development difference rule is enforced by PropertyTycoon
            player.ReleaseFromJail();
            player.DevelopProperty(crapperStreet);
            Assert.AreEqual(30, crapperStreet.GetRent());

            // Bob develops more
            player.DevelopProperty(crapperStreet);
            player.DevelopProperty(crapperStreet);
            Assert.AreEqual(160, crapperStreet.GetRent());

            // Bob sells 1 house
            player.UndevelopProperty(crapperStreet);
            Assert.AreEqual(90, crapperStreet.GetRent());

        }

        [TestMethod]
        public void SellDevelopableLand_Developed_Unmortgaged()
        {
            int[] rentTable = new int[6] { 4, 20, 60, 180, 320, 500 };
            DevelopableLand gangstersParadise = new DevelopableLand("Gangsters Paradise",
                                                60, Colour.Brown, rentTable);
            // develop property with 1 house
            gangstersParadise.Develop();
            Assert.AreEqual(1, gangstersParadise.GetHouses());

            // check if property can be sold
            Assert.IsFalse(gangstersParadise.CanSellProperty());

            // cannot sell, must undevelop property first
            gangstersParadise.Undevelop();
            Assert.AreEqual(0, gangstersParadise.GetHouses());

            // unmortgaged property sells for original price value (£60)
            Assert.IsTrue(gangstersParadise.CanSellProperty());
            int sellingPrice = gangstersParadise.SellPropertyToBank();
            Assert.AreEqual(60, sellingPrice);
            // property is unowned after selling
            Assert.IsNull(gangstersParadise.GetOwner());
        }

        [TestMethod]
        public void SellDevelopableLand_Mortgaged()
        {
            int[] rentTable = new int[6] { 4, 20, 60, 180, 320, 450 };
            DevelopableLand gangstersParadise = new DevelopableLand("Gangsters Paradise",
                                                60, Colour.Brown, rentTable);
            // mortgage property
            gangstersParadise.Mortgage();
            Assert.IsTrue(gangstersParadise.IsMortgaged());
            // mortgaged property sells for half price (£30)
            Assert.IsTrue(gangstersParadise.CanSellProperty());
            int sellingPrice = gangstersParadise.SellPropertyToBank();
            Assert.AreEqual(30, sellingPrice);
            // property is unowned and unmortgaged after selling
            Assert.IsNull(gangstersParadise.GetOwner());
            Assert.IsFalse(gangstersParadise.IsMortgaged());
        }

        [TestMethod]
        public void DevelopableLand_CalculateTotalValue()
        {
            int[] rentTable = new int[6] { 30, 60, 100, 150, 200, 320 };
            int price = 200;
            Colour group = Colour.Blue;
            DevelopableLand reyLane = new DevelopableLand("Rey Lane", price, group, rentTable);

            // total value = original price (unmortgaged, undeveloped)
            Assert.AreEqual(200, reyLane.CalculateTotalValue());
            // mortgage undeveloped property
            reyLane.Mortgage();
            Assert.IsTrue(reyLane.IsMortgaged());
            // total value = original price / 2
            Assert.AreEqual(100, reyLane.CalculateTotalValue());
            // unmortgaged property
            reyLane.Unmortgage();
            Assert.IsFalse(reyLane.IsMortgaged());
            // develop property with 3 houses
            for(int i = 0; i < 3; i++)
            {
                reyLane.Develop();
            }
            Assert.AreEqual(3, reyLane.GetHouses());
            // get correct house price
            int housePrice = reyLane.GetDevelopCost();
            Assert.AreEqual(50, housePrice);
            // total value = original price + (3 * houseprice)
            int correctTotal = 200 + (3 * 50);
            Assert.AreEqual(correctTotal, reyLane.CalculateTotalValue());

            // mortgage property
            reyLane.Mortgage();
            Assert.IsTrue(reyLane.IsMortgaged());

            //total value = (original price / 2) + (3 * houseprice)
            int correctTotal2 = (200 / 2) + (3 * housePrice);
            Assert.AreEqual(correctTotal2, reyLane.CalculateTotalValue());

        }

        [TestMethod]
        public void DevelopableLand_SellProperty()
        {
            int[] rentTable = new int[6] { 60, 70, 80, 90, 100, 200 };
            DevelopableLand ibisClose = new DevelopableLand("Ibis Close", 400, Colour.DeepBlue, rentTable);
            // sell property to bank
            Assert.AreEqual(400, ibisClose.SellPropertyToBank());

            // develop the property and attempt to sell it
            ibisClose.Develop();
            try
            {
                ibisClose.SellPropertyToBank();
            }
            catch (DevelopableLandException e)
            {
                Console.WriteLine(e.Message);
                Assert.AreEqual("Cannot sell the property while it is developed!", e.Message);
            }

        }

        [TestMethod]
        public void DevelopableLand_GetDevelopCost()
        {
            int[] rentTable = new int[6] { 60, 70, 80, 90, 100, 200 };
            DevelopableLand ibisClose = new DevelopableLand("Ibis Close", 400, Colour.DeepBlue, rentTable);
            // Deep Blue houses = £200
            Assert.AreEqual(Colour.DeepBlue, ibisClose.GetColourGroup());
            Assert.AreEqual(200, ibisClose.GetDevelopCost());

            DevelopableLand picardAvenue = new DevelopableLand("Picard Avenue", 260, Colour.Yellow, rentTable);
            // Yellow houses = £150
            Assert.AreEqual(Colour.Yellow, picardAvenue.GetColourGroup());
            Assert.AreEqual(150, picardAvenue.GetDevelopCost());

            DevelopableLand pennyLane = new DevelopableLand("Penny Lane", 260, Colour.Orange, rentTable);
            // Orange houses = £100
            Assert.AreEqual(Colour.Orange, pennyLane.GetColourGroup());
            Assert.AreEqual(100, pennyLane.GetDevelopCost());
        }
    }
}
