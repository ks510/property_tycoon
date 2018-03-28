using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class PotLuck : AbstractCard
    {
        private string cardName;

        public PotLuck(string description, IAction action) : base(description,action)
        {
            this.cardName = "Pot Luck";
        }

        public override string GetCardName()
        {
            return this.cardName;
        }
    }
}
