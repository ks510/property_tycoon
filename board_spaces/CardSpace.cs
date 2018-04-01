using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTycoonProject
{
    public class CardSpace : IBoardSpace
    {
        private CardType cardType;

        public CardSpace(CardType cardType)
        {
            this.cardType = cardType;
        }

        public CardType GetCardType()
        {
            return this.cardType;
        }
    }
}
