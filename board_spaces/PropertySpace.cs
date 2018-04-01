using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class PropertySpace : IBoardSpace
    {
        private IProperty property;

        public PropertySpace(IProperty property)
        {
            this.property = property;
        }

        public IProperty GetProperty()
        {
            return this.property;
        }
    }
}
