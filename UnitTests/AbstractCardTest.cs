using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class AbstractCardTest
    {
        //check if Pot Luck card created properly
        [TestMethod]
        public void CreatePotLuckCard()
        {
            GoToJailAction action = new GoToJailAction();
            PotLuck card = new PotLuck("Go To Jail!", action);

            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);

            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);

            var cardName = card.GetCardName();
            Assert.AreEqual("Pot Luck", cardName);
        }

        //check if Pot Luck card created properly and inherits from AbstractCard
        [TestMethod]
        public void CreatePotLuckCard_AbstractCardType()
        {
            GoToJailAction action = new GoToJailAction();
            AbstractCard card = new PotLuck("Go To Jail!", action);

            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);

            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);

            var cardName = card.GetCardName();
            Assert.AreEqual("Pot Luck", cardName);
        }

        //check if Opportunity Knocks card created properly
        [TestMethod]
        public void CreateOpportunityKnocksCard()
        {
            GoToJailAction action = new GoToJailAction();
            OpportunityKnocks card = new OpportunityKnocks("Go To Jail!", action);

            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);

            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);

            var cardName = card.GetCardName();
            Assert.AreEqual("Opportunity Knocks", cardName);

        }

        //check if Opportunity Knocks card inherits from AbstractCard
        [TestMethod]
        public void CreateOpportunityKnocksCard_AbstractCardType()
        {
            GoToJailAction action = new GoToJailAction();
            AbstractCard card = new OpportunityKnocks("Go To Jail!", action);

            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);

            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);

            var cardName = card.GetCardName();
            Assert.AreEqual("Opportunity Knocks", cardName);
        }
    }
}
