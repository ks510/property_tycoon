using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class OpportunityKnocks : AbstractCard
    {
        private string cardName;

        public OpportunityKnocks(string description, IAction action) : base(description, action)
        {
            this.cardName = "Opportunity Knocks";
        }

        public override string GetCardName()
        {
            return this.cardName;
        }
    }
}
