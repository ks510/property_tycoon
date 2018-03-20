using System;
namespace ConsoleApp1
{
    public class Utility : I_Property
    {

        Player owner;
        String name;
        int price;
        int[] multiplier;
        Boolean mortgage;
        private string v;

        public Utility(String n)
        {
            owner = null;
            name = n;
            price = 150;
            multiplier = new int[2] {4, 10};
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
                return multiplier[owner.GetUtilities() - 1];
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
                return price;
            }
            else
            {
                return price / 2;
            }
        }
    }
}