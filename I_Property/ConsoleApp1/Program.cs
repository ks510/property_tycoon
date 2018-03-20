using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Colour brown = Colour.Brown;
            Utility utility = new Utility("Edison Water");
            Station station = new Station("Brighton Station");
            int[] array = new int[6] { 2, 10, 30, 90, 160, 250 };
            DevelopableLand land = new DevelopableLand("Crapper Street", 60, array, brown);
        }
    }
}
