using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyTycoonProject;

namespace PropertyTycoonTest
{
    [TestClass]
    public class BoardSpaceTest
    {
        [TestMethod]
        public void CreatePropertySpace()
        {
            Utility waters = new Utility("Edison Waters", 5);
            PropertySpace boardSpace = new PropertySpace(waters);

            // correct property stored
            Assert.AreEqual(waters, boardSpace.GetProperty());
            // implements IBoardSpace interface
            Assert.IsTrue(boardSpace is IBoardSpace);
        }

        [TestMethod]
        public void CreateGoSpace()
        {
            // go space does nothing, simply an empty object
            GoSpace go = new GoSpace();

            // implements IBoardSpace interface
            Assert.IsTrue(go is IBoardSpace);
        }

        [TestMethod]
        public void CreateJailSpace()
        {
            // jail space does nothing, simply an empty object
            JailSpace jailSpace = new JailSpace();

            // implements IBoardSpace interface
            Assert.IsTrue(jailSpace is IBoardSpace);

        }

        [TestMethod]
        public void CreateFreeParkingSpace()
        {
            FreeParkingSpace freeParking = new FreeParkingSpace();

            // implements IBoardSpace interface
            Assert.IsTrue(freeParking is IBoardSpace);
        }

        [TestMethod]
        public void CreateInstructionSpace()
        {
            PayAction pay200 = new PayAction(200, Recipient.Bank);
            InstructionSpace payTax = new InstructionSpace(pay200);

            // correct instruction stored
            Assert.AreEqual(pay200, payTax.GetInstruction());
            // implements IBoardSpace interface
            Assert.IsTrue(payTax is IBoardSpace);

        }
    }
}
