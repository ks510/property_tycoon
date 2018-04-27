using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class DiceTest
    {
        [TestMethod]
        public void TestDiceRoll()
        {
            for (int i = 0; i < 10; i++)
            {
                int number = Dice.RollDice();
                Assert.IsTrue(number < 7 && number > 0, "Dice rolled out of range 1:6");
                Console.WriteLine("The dice rolled {0}", number);
            }
        }

        [TestMethod]
        public void TestDiceRoll2()
        {
            for (int i = 0; i < 10; i++)
            {
                int number = Dice.RollDice();
                Assert.IsTrue(number < 7 && number > 0, "Dice rolled out of range 1:6");
                Console.WriteLine("The dice rolled {0}", number);
            }
        }
    }
}
