using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject.Assets
{
    interface IProperty
    {
        int GetRent();
        int GetOwner();
        void SetOwner(IPlayer p);
        int GetPrice();
        int CalculateValue();
        bool IsMortgaged();
        void SetMortgaged(bool mortgaged);
        bool IsDevelopable();
        Colour GetGroup();
    }
}
