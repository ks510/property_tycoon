using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class InstructionSpace : IBoardSpace
    {
        private IAction instruction;

        public InstructionSpace(IAction instruction)
        {
            this.instruction = instruction;
        }

        public IAction GetInstruction()
        {
            return this.instruction;
        }
    }
}
