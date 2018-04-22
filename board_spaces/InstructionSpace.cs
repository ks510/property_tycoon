using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    /// <summary>
    /// Represents a board space with instructions to be followed
    /// e.g. Super Tax £200.
    /// </summary>
    public class InstructionSpace : IBoardSpace
    {
        private IAction instruction;

        /// <summary>
        /// Constructor for an instruction board space.
        /// </summary>
        /// <param name="instruction">instruction to follow on landing</param>
        public InstructionSpace(IAction instruction)
        {
            this.instruction = instruction;
        }

        /// <summary>
        /// Return the instruction and relevant information on this board space
        /// </summary>
        /// <returns>instruction</returns>
        public IAction GetInstruction()
        {
            return this.instruction;
        }
    }
}
