using System;
namespace ConsoleApp1
{
    public class DevelopableLand : I_Property
    {

        Player owner;
        String name;
        int price;
        int[] rentTable;
        Boolean mortgage;
        Colour group;
        int houses;
        int hotels;

        public DevelopableLand(String n, int p, int[] rT, Colour g)
        {
            owner = null;
            name = n;
            price = p;
            rentTable = rT;
            group = g;
            houses = 0;
            hotels = 0;
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
                return rentTable[houses - 1];
            }
        }

        Player I_Property.GetOwner()
        {
            return owner;
        }

        void I_Property.SetOwner(Player p)
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
                return price + ((int)group * houses);
            }
            else
            {
                return (price + ((int)group * houses)) / 2;
            }
        }

        void SetGroup(Colour g)
        {
            group = g;
        }

        Colour GetGroup()
        {
            return group;
        }

        void Improve()
        {
            houses += 1;
            if (houses > 4)
            {
                hotels = 1;
            }
        }

        int GetHouses()
        {
            return houses;
        }

        int GetHotels()
        {
            return hotels;
        }

        void SetHotels(int h)
        {
            hotels = h;
        }

        bool I_Property.IsDevelopable()
        {
            if (mortgage == false)
            {
                if (hotels < 1)
                {
                    return true;
                }
                return false;
            }
            return false;
    }

        Colour I_Property.GetGroup()
        {
            return group;
        }

}