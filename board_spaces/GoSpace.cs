using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents the "Go" space on the Property Tycoon board. This space
    /// does not store anything as the receipt of £200 for each player 
    /// that passes this space is managed by the PropertyTycoon class.
    /// </summary>
    public class GoSpace : IBoardSpace
    {
        /// <summary>
        /// Constructor for a "Go" space.
        /// </summary>
        public GoSpace()
        {
            
        }
    }
}
