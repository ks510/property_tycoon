using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
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
