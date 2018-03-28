using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class AbstractCardTest
    {
        //check if Pot Luck card created properly (Go To Jail card)
        [TestMethod]
        public void CreatePotLuckCard()
        {
            GoToJailAction action = new GoToJailAction();
            PotLuck card = new PotLuck("Go To Jail!", action);

            // correct description
            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);
            // correct jail action
            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);
            // correct card name
            var cardName = card.GetCardName();
            Assert.AreEqual("Pot Luck", cardName);
            // correct string representation "Card Name: Description"
            var cardString = card.ToString();
            Assert.AreEqual("Pot Luck: Go To Jail!", cardString);
        }

        //check if Pot Luck card created properly and inherits from AbstractCard (Go To Jail Card)
        [TestMethod]
        public void CreatePotLuckCard_AbstractCardType()
        {
            GoToJailAction action = new GoToJailAction();
            AbstractCard card = new PotLuck("Go To Jail!", action);

            // correct description
            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);
            // correct jail action
            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);
            // correct card name
            var cardName = card.GetCardName();
            Assert.AreEqual("Pot Luck", cardName);
            // correct string representation "Card Name: Description"
            var cardString = card.ToString();
            Assert.AreEqual("Pot Luck: Go To Jail!", cardString);
        }

        //check if Opportunity Knocks card created properly (Go To Jail card)
        [TestMethod]
        public void CreateOpportunityKnocksCard()
        {
            GoToJailAction action = new GoToJailAction();
            OpportunityKnocks card = new OpportunityKnocks("Go To Jail!", action);

            // correct description
            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);
            // correct jail action
            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);
            // correct card name
            var cardName = card.GetCardName();
            Assert.AreEqual("Opportunity Knocks", cardName);
            // correct string representation "Card Name: Description"
            var cardString = card.ToString();
            Assert.AreEqual("Opportunity Knocks: Go To Jail!", cardString);

        }

        //check if Opportunity Knocks card inherits from AbstractCard (Go To Jail Card)
        [TestMethod]
        public void CreateOpportunityKnocksCard_AbstractCardType()
        {
            GoToJailAction action = new GoToJailAction();
            AbstractCard card = new OpportunityKnocks("Go To Jail!", action);
            // correct description
            var description = card.GetDescription();
            Assert.AreEqual("Go To Jail!", description);
            // correct jail action
            var cardAction = card.GetAction();
            Assert.AreEqual(action, cardAction);
            // correct card name
            var cardName = card.GetCardName();
            Assert.AreEqual("Opportunity Knocks", cardName);
            // correct string representation "Card Name: Description"
            var cardString = card.ToString();
            Assert.AreEqual("Opportunity Knocks: Go To Jail!", cardString);
        }
    }
}
