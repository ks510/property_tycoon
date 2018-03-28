using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public abstract class AbstractCard
    {
        private string description;
        private IAction action;

        public AbstractCard(string description, IAction action)
        {
            this.description = description;
            this.action = action;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public IAction GetAction()
        {
            return this.action;
        }

        public abstract string GetCardName();
    }
}
