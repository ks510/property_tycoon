

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents a board space with instructions to be followed
    /// e.g. Super Tax £200.
    /// </summary>
    public class InstructionSpace : IBoardSpace
    {
        private IAction instruction;
        private string description;

        /// <summary>
        /// Constructor for an instruction board space.
        /// </summary>
        /// <param name="instruction">instruction to follow on landing</param>
        /// <param name="description">Description for the board space</param>
        public InstructionSpace(string description, IAction instruction)
        {
            this.instruction = instruction;
            this.description = description;
        }

        /// <summary>
        /// Return the instruction and relevant information on this board space
        /// </summary>
        /// <returns>instruction</returns>
        public IAction GetInstruction()
        {
            return this.instruction;
        }

        public string GetDescription()
        {
            return this.description;
        }
    }
}
