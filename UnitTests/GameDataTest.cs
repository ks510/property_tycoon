using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class GameDataTest
    {
        [TestMethod]
        public void CreateGameDataObject()
        {
            GameData data = new GameData();
            // check 2 lists for cards and 1 array for board spaces are initialised (empty)
            Assert.AreEqual(0, data.GetPotLuckCards().Count);
            Assert.AreEqual(0, data.GetOpportunityKnocksCards().Count);
            Assert.AreEqual(40, data.GetBoardSpaces().Length);
        }

        [TestMethod]
        public void GameData_AddCards()
        {
            GameData data = new GameData();
            // make 3 pot luck cards and add to game data list
            PotLuck potLuck1 = new PotLuck("Go To Jail", new GoToJailAction(31));
            PotLuck potLuck2 = new PotLuck("Bitcoin sales, recieve £20", new ReceiveMoneyAction(20, Sender.Bank));
            PotLuck potLuck3 = new PotLuck("Assessed for Repairs", new PayRepairsAction(10, 50));
            List<PotLuck> potLuckPile = new List<PotLuck>() { potLuck1, potLuck2, potLuck3 };

            OpportunityKnocks opKnock1 = new OpportunityKnocks("Go to Crapper street", new MoveToAction(20, true));
            OpportunityKnocks opKnock2 = new OpportunityKnocks("Pay £100 fine", new PayAction(100, Recipient.FreeParking));
            List<OpportunityKnocks> opKnockPile = new List<OpportunityKnocks>() { opKnock1, opKnock2 };

            // adding cards to the game data
            for (int i = 0; i < 3; i++)
            {
                data.AddCard(potLuckPile[i]);
                Assert.AreEqual(i+1, data.GetPotLuckCards().Count); // assert correct size
            }
            for (int i = 0; i < 2; i++)
            {
                data.AddCard(opKnockPile[i]);
                Assert.AreEqual(i + 1, data.GetOpportunityKnocksCards().Count); // assert correct size
            }

            // check all pot luck cards were added correctly
            for(int i = 0; i < 3; i++) 
            {
                Assert.AreEqual(potLuckPile[i], data.GetPotLuckCards()[i]);
            }
            // check all opportunity knocks cards were added correctly
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(opKnockPile[i], data.GetOpportunityKnocksCards()[i]);
            }
        }


    }
}
