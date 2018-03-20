using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    interface I_Property
    {
        int GetRent();
        int GetOwner();
        void SetOwner(IPlayer p);
        int GetPrice();
        int CalculateValue();
        bool IsMortgaged();
        void ToggleMortgaged();
        bool IsDevelopable();
        Colour GetGroup();
    }
}