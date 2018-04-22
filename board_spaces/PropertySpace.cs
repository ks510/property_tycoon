using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents a "property" space on the Property Tycoon board.
    /// </summary>
    public class PropertySpace : IBoardSpace
    {
        private IProperty property;

        /// <summary>
        /// Constructor for a property space on the board.
        /// </summary>
        /// <param name="property">Property on this board space containing 
        /// all information about this property.</param>
        public PropertySpace(IProperty property)
        {
            this.property = property;
        }

        /// <summary>
        /// Return the property on this board space.
        /// </summary>
        /// <returns>Property</returns>
        public IProperty GetProperty()
        {
            return this.property;
        }

    }
}
