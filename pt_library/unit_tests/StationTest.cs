using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class StationTest
    {
        [TestMethod]
        public void InitialiseStationWithCorrectData()
        {
            int price = 100;
            string stationName = "Hove Station";
            Station station = new Station(stationName, price);

            // correct price
            Assert.AreEqual(100, station.GetPrice());
            // correct station name
            Assert.AreEqual("Hove Station", station.GetPropertyName());

            // correct rent table initialised
            int[] rentTable = new int[] { 25, 50, 100, 200 };
            var actualRentTable = station.GetRentTable();
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(rentTable[i], actualRentTable[i]);
            }

            // check initial owner null
            Assert.IsNull(station.GetOwner());
            // check initialise unmortgaged
            Assert.IsFalse(station.IsMortgaged());
            // implements IProperty interface
            Assert.IsTrue(station is IProperty);

        }

        [TestMethod]
        public void Station_CorrectGetterMethods()
        {
            Station station = new Station("Falmer Station", 200);

            // TODO: correct rent returned based on number of stations owned

            // TODO: correct current owner player returned

            // correct price returned
            Assert.AreEqual(200, station.GetPrice());

            // total value simply returns price of station
            Assert.AreEqual(200, station.CalculateTotalValue());

            // station is undevelopable so can always be sold
            Assert.IsTrue(station.CanSellProperty());

            // correct station name returned
            Assert.AreEqual("Falmer Station", station.GetPropertyName());

            // stations are undevelopable
            Assert.IsFalse(station.IsDevelopable());

        }

        [TestMethod]
        public void Station_CorrectRentReturned()
        {
            // player buys 1 station
            Station station1 = new Station("Brighton Station", 200);
            Station station2 = new Station("Brighton Station", 200);
            Station station3 = new Station("Brighton Station", 200);
            Station station4 = new Station("Brighton Station", 200);
            Assert.AreEqual(0, station1.GetRent()); // unowned station rent = 0

            IPlayer player = new HumanPlayer("Bob", 0, Token.Cat);

            // bob buys 1 station, rent = £25
            player.BuyProperty(station1);
            Assert.AreEqual(25, station1.GetRent());

            // bob goes to jail, rent = £0
            player.GoToJail(31);
            Assert.AreEqual(0, station1.GetRent());

            // bob released from jail and buys another station
            player.ReleaseFromJail();
            player.BuyProperty(station2);
            Assert.AreEqual(50, station1.GetRent());
            Assert.AreEqual(50, station2.GetRent());

            // bob buys the remaining stations, rent = £200
            player.BuyProperty(station3);
            player.BuyProperty(station4);
            // maximum rent on each station = £200
            Assert.AreEqual(200, station1.GetRent());
            Assert.AreEqual(200, station2.GetRent());
            Assert.AreEqual(200, station3.GetRent());
            Assert.AreEqual(200, station4.GetRent());

            // bob mortgages a station, rent = £0
            player.Mortgage(station3);
            Assert.AreEqual(0, station3.GetRent());

            // bob sells the mortgaged staton and one other station
            // he only owns 2 stations now, rent = £50
            player.SellProperty(station3);
            player.SellProperty(station4);
            Assert.AreEqual(50, station1.GetRent());
            Assert.AreEqual(50, station2.GetRent());

        }

        [TestMethod]
        public void SellStation_Unmortgaged()
        {
            Station station = new Station("Lewes Station", 200);
            // station can always be sold
            Assert.IsTrue(station.CanSellProperty());

            // unmortgaged property sells for original price value (£200)
            int sellingPrice = station.SellPropertyToBank();
            Assert.AreEqual(200, sellingPrice);
            // station is unowned after selling
            Assert.IsNull(station.GetOwner());
        }

        [TestMethod]
        public void SellStation_Mortgaged()
        {
            Station station = new Station("Lewes Station", 200);
            station.Mortgage();
            // mortgage station
            Assert.IsTrue(station.IsMortgaged());

            int sellingPrice = station.SellPropertyToBank();
            // mortgaged station sells for half of original price value
            Assert.AreEqual(200 / 2, sellingPrice);
            // station is unowned after selling
            Assert.IsNull(station.GetOwner());
            // station set to unmortgaged after sale
            Assert.IsFalse(station.IsMortgaged());
        }


    }
}
