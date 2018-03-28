using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class ColourTest
    {
        /// <summary>
        /// Test that all 8 colour groups have been included.
        /// </summary>
        [TestMethod]
        public void AllColoursIncluded()
        {
            // 8 colour groups in total
            int totalColours = Enum.GetValues(typeof(Colour)).Length;
            Assert.AreEqual(8, totalColours);
        }

        /// <summary>
        /// Test each colour group has the correct improvement cost stored.
        /// </summary>
        [TestMethod]
        public void TestCorrectImprovementCosts()
        {
            // Brown group
            int brownCost = (int)Colour.Brown;
            Assert.AreEqual(50, brownCost);
            // Blue group
            int blueCost = (int)Colour.Blue;
            Assert.AreEqual(50, blueCost);
            // Purple group
            int purpleCost = (int)Colour.Purple;
            Assert.AreEqual(100, purpleCost);
            // Orange group
            int orangeCost = (int)Colour.Orange;
            Assert.AreEqual(100, orangeCost);
            // Red group
            int redCost = (int)Colour.Red;
            Assert.AreEqual(150, redCost);
            // Yellow group
            int yellowCost = (int)Colour.Yellow;
            Assert.AreEqual(150, yellowCost);
            // Green group
            int greenCost = (int)Colour.Green;
            Assert.AreEqual(200, greenCost);
            // Deep Blue group
            int deepBlueCost = (int)Colour.DeepBlue;
            Assert.AreEqual(200, deepBlueCost);
        }
    }
}
