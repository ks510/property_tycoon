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
            //TODO: if player is in jail, rent = 0
            Station station = new Station("Brighton Station", 200);
            //IPlayer player = new HumanPlayer();
            //station.SetOwner(player)
            //player.GoToJail();
            //Assert.AreEqual(1, station.GetRent());

            // if station is mortgaged, rent = 0
            Station station2 = new Station("Falmer Station", 200);
            station.Mortgage();
            Assert.IsTrue(station.IsMortgaged());
            Assert.AreEqual(0, station.GetRent());

            //TODO: if player owns x stations, return correct rent:
            // 1 station = £25
            // 2 stations = £50
            // 3 stations = £100
            // 4 stations = £200
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
