using System;

namespace PropertyTycoonLibrary
{
    public class Dice
    {
        private static Random dice = new Random();

        public static int RollDice()
        {
            return dice.Next(1, 7);
        }

    }
}
