using System;

namespace ConsoleApp1
{
    public class Station : I_Property
    {

        Player owner;
        String name;
        int price;
        int[] rentTable;
        Boolean mortgage;

        public Station(String n)
        {
            owner = null;
            name = n;
            price = 200;
            rentTable = new int[4] { 25, 50, 100, 200 };
            mortgage = false;
        }
        int I_Property.GetRent()
        {
            if (owner.InJail())
            {
                return 0;
            }
            else if (mortgage)
            {
                return 0;
            }
            else
            {
                return rentTable[owner.GetStations() - 1];
            }
        }

        Player I_Property.GetOwner()
        {
            return owner;
        }

        void SetOwner(Player p)
        {
            owner = p;
        }

        int I_Property.GetPrice()
        {
            return price;
        }

        Boolean I_Property.IsMortgaged()
        {
            return mortgage;
        }

        void I_Property.ToggleMortgaged()
        {
            if (mortgage == false)
            {
                mortgage = true;
            }
            else
            {
                mortgage = false;
            }
        }

        int I_Property.CalculateValue()
        {
            if (mortgage == false)
            {
                return price;
            }
            else
            {
                return price / 2;
            }
        }

    }
}