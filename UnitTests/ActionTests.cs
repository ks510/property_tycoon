using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void CreatePayAction()
        {
            int amount = 500;
            Recipient receiver = Recipient.Bank;
            PayAction pay = new PayAction(amount, receiver);

            // correct payment amount returned
            Assert.AreEqual(amount, pay.GetAmount());
            // correct recipient enum returned
            Assert.AreEqual(receiver, pay.GetPayTo());
            // IAction interface implemented
            Assert.IsTrue(pay is IAction);
        }

        [TestMethod]
        public void CreatePayRepairsAction()
        {
            int houseCost = 35;
            int hotelCost = 100;
            PayRepairsAction payRepairs = new PayRepairsAction(houseCost, hotelCost);

            // correct house repairs cost
            Assert.AreEqual(houseCost, payRepairs.GetHouseCost());
            // correct hotel repairs cost
            Assert.AreEqual(hotelCost, payRepairs.GetHotelCost());
            // IAction interface implemented
            Assert.IsTrue(payRepairs is IAction);

        }

        [TestMethod]
        public void ReceiveMoneyAction()
        {
            int amount = 40;
            Sender receiveFrom = Sender.FreeParking;
            ReceiveMoneyAction receiveMoney = new ReceiveMoneyAction(amount, receiveFrom);

            // correct amount stored
            Assert.AreEqual(amount, receiveMoney.GetAmount());
            // correct sender stored
            Assert.AreEqual(receiveFrom, receiveMoney.GetReceiveFrom());
            // IAction interface implemented
            Assert.IsTrue(receiveMoney is IAction);
        }

        [TestMethod]
        public void CreateMoveToAction()
        {
            int boardSpaceID = 30;
            bool clockwise = true;
            MoveToAction moveAction = new MoveToAction(boardSpaceID, clockwise);

            // correct board space ID stored
            Assert.AreEqual(boardSpaceID, moveAction.GetBoardSpaceID());
            // correct direction stored
            Assert.IsTrue(moveAction.MoveClockwise());
            // implements IAction interface
            Assert.IsTrue(moveAction is IAction);

        }

        [TestMethod]
        public void CreateMoveNSpacesAction()
        {
            int numberOfSpaces = 3;
            bool clockwise = false;
            MoveNSpacesAction moveNSpaces = new MoveNSpacesAction(numberOfSpaces, clockwise);

            // correct number of spaces stored
            Assert.AreEqual(numberOfSpaces, moveNSpaces.GetNumberOfSpaces());
            // correct direction stored
            Assert.IsFalse(moveNSpaces.MoveClockwise());
            // implements IAction interface
            Assert.IsTrue(moveNSpaces is IAction);
        }

        [TestMethod]
        public void CreateGoToJailAction()
        {
            int jailSpaceID = 31;
            GoToJailAction goToJail = new GoToJailAction(jailSpaceID);

            // correct jail space ID stored
            Assert.AreEqual(jailSpaceID, goToJail.GetJailSpaceID());
            // implements IAction interface
            Assert.IsTrue(goToJail is IAction);

        }
        
        [TestMethod]
        public void CreateReceiveJailFreeAction()
        {
            ReceiveJailFreeAction getJailFree = new ReceiveJailFreeAction();

            // implements IAction interface
            Assert.IsTrue(getJailFree is IAction);
        }

        [TestMethod]
        public void CreateDrawCardAction()
        {
            CardType card = CardType.PotLuck;
            DrawCardAction drawCard = new DrawCardAction(card);

            // correct card name stored
            Assert.AreEqual(card, drawCard.GetCardType());
            // implements IAction
            Assert.IsTrue(drawCard is IAction);

        }

        [TestMethod]
        public void CreateChoiceAction()
        {
            IAction drawCard = new DrawCardAction(CardType.PotLuck);
            IAction payBank = new PayAction(50, Recipient.Bank);
            ChoiceAction choiceCard = new ChoiceAction(drawCard, payBank);

            // correct IActions stored
            Assert.AreEqual(drawCard, choiceCard.GetChoice1());
            Assert.AreEqual(payBank, choiceCard.GetChoice2());

            // implements IAction
            Assert.IsTrue(choiceCard is IAction);
        }



    }
}
