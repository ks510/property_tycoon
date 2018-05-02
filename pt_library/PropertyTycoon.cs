using System;
using System.Collections.Generic;

namespace PropertyTycoonLibrary
{
    /// <summary>
    /// Represents a Property Tycoon game in progress. Enforces the rules of property tycoon
    /// and manages the order of player turns and actions in the game.
    /// </summary>
    public class PropertyTycoon
    {
        private Board board;
        private IPlayer[] players;
        private IPlayer playerTakingTurn;
        private int currentPlayer;
        private Auction auction;
        private Queue<PotLuck> potLuckPile;
        private Queue<OpportunityKnocks> opportunityKnocksPile;

        
        /// <summary>
        /// Constructor for a Property Tycoon game which sets up the game board,
        /// shuffles card piles and adds players to the game.
        /// </summary>
        /// <param name="gameData">Container for all game data</param>
        /// <param name="numOfPlayers">Number of players in this game (2-6)</param>
        public PropertyTycoon(GameData gameData, IPlayer[] players)
        {
            this.board = new Board(gameData.GetBoardSpaces());
            // check that a valid number of players is chosen
            if (players.Length > 1 && players.Length <= 6)
            {
                this.players = players;
            }
            else { throw (new PropertyTycoonException("Game can only start with 2-6 players!")); }

            this.playerTakingTurn = players[0];
            this.currentPlayer = 0;
            this.auction = null;

            // randomly shuffle the pot luck cards into a pile
            List<PotLuck> shuffledPotLucks = Shuffle(gameData.GetPotLuckCards());
            this.potLuckPile = new Queue<PotLuck>(shuffledPotLucks);

            // randomly shuffle opportunity knocks cards into a pile
            List<OpportunityKnocks> shuffledOpKnocks = Shuffle(gameData.GetOpportunityKnocksCards());
            this.opportunityKnocksPile = new Queue<OpportunityKnocks>(shuffledOpKnocks);

            
        }

        // making a default property tycoon game with 6 players
        public PropertyTycoon()
        {


            // board hard coded from spreadsheet
            IBoardSpace[] spaces = new IBoardSpace[40];
            spaces[0] = new GoSpace();
            spaces[1] = new PropertySpace(new DevelopableLand("Crapper Street", 60, Colour.Brown, new int[] { 2, 10, 30, 90, 160, 250 }));
            spaces[2] = new InstructionSpace("Pot Luck", new DrawCardAction(CardType.PotLuck));
            spaces[3] = new PropertySpace(new DevelopableLand("Gangsters Paradise", 60, Colour.Brown, new int[] { 4, 20, 60, 180, 320, 450 }));
            spaces[4] = new InstructionSpace("Income Tax £200", new PayAction(200, Recipient.Bank));
            spaces[5] = new PropertySpace(new Station("Brighton Station", 200));
            spaces[6] = new PropertySpace(new DevelopableLand("Weeping Angel", 100, Colour.Blue, new int[] { 6, 30, 90, 270, 400, 550 }));
            spaces[7] = new InstructionSpace("Opportunity Knocks", new DrawCardAction(CardType.OpportunityKnocks));
            spaces[8] = new PropertySpace(new DevelopableLand("Potts Avenue", 100, Colour.Blue, new int[] { 6, 30, 90, 270, 400, 550 }));
            spaces[9] = new PropertySpace(new DevelopableLand("Nardole Drive", 120, Colour.Blue, new int[] { 8, 40, 100, 300, 450, 600 }));
            spaces[10] = new JailSpace();
            spaces[11] = new PropertySpace(new DevelopableLand("Skywalker Drive", 140, Colour.Purple, new int[] { 10, 50, 150, 450, 625, 750 }));
            spaces[12] = new PropertySpace(new Utility("Tesla Power Co", 150));
            spaces[13] = new PropertySpace(new DevelopableLand("Wookie Hole", 140, Colour.Purple, new int[] { 10, 50, 150, 450, 625, 750 }));
            spaces[14] = new PropertySpace(new DevelopableLand("Rey Lane", 160, Colour.Purple, new int[] { 12, 60, 180, 500, 700, 900 }));
            spaces[15] = new PropertySpace(new Station("Hove Station", 200));
            spaces[16] = new PropertySpace(new DevelopableLand("Cooper Drive", 200, Colour.Orange, new int[] { 14, 70, 200, 550, 750, 950 }));
            spaces[17] = new InstructionSpace("Pot Luck", new DrawCardAction(CardType.PotLuck));
            spaces[18] = new PropertySpace(new DevelopableLand("Wolowitz Street", 180, Colour.Orange, new int[] { 14, 70, 200, 550, 750, 950 }));
            spaces[19] = new PropertySpace(new DevelopableLand("Penny Lane", 200, Colour.Orange, new int[] { 16, 80, 220, 600, 800, 1000 }));
            spaces[20] = new FreeParkingSpace();
            spaces[21] = new PropertySpace(new DevelopableLand("Yue Fei Square", 220, Colour.Red, new int[] { 18, 90, 250, 700, 875, 1050 }));
            spaces[22] = new InstructionSpace("Opportunity Knocks", new DrawCardAction(CardType.OpportunityKnocks));
            spaces[23] = new PropertySpace(new DevelopableLand("Mulan Rouge", 220, Colour.Red, new int[] { 18, 90, 250, 700, 875, 1050 }));
            spaces[24] = new PropertySpace(new DevelopableLand("Han Xin Gardens", 240, Colour.Red, new int[] { 20, 100, 300, 750, 925, 1100 }));
            spaces[25] = new PropertySpace(new Station("Falmer Station", 200));
            spaces[26] = new PropertySpace(new DevelopableLand("Kirk Close", 260, Colour.Yellow, new int[] { 22, 110, 330, 800, 975, 1150 }));
            spaces[27] = new PropertySpace(new DevelopableLand("Picard Avenue", 260, Colour.Yellow, new int[] { 22, 110, 330, 800, 975, 1150 }));
            spaces[28] = new PropertySpace(new Utility("Edison Water", 150));
            spaces[29] = new PropertySpace(new DevelopableLand("Crusher Creek", 280, Colour.Yellow, new int[] { 22, 120, 360, 850, 1025, 1200 }));
            spaces[30] = new InstructionSpace("Go To Jail", new GoToJailAction(10));
            spaces[31] = new PropertySpace(new DevelopableLand("Sirat Mews", 300, Colour.Green, new int[] { 26, 130, 390, 900, 1100, 1275 }));
            spaces[32] = new PropertySpace(new DevelopableLand("Ghengis Crescent", 300, Colour.Green, new int[] { 26, 130, 390, 900, 1100, 1275 }));
            spaces[33] = new InstructionSpace("Pot Luck", new DrawCardAction(CardType.PotLuck));
            spaces[34] = new PropertySpace(new DevelopableLand("Ibis Close", 320, Colour.Green, new int[] { 28, 150, 450, 1000, 1200, 1400 }));
            spaces[35] = new PropertySpace(new Station("Lewes Station", 200));
            spaces[36] = new InstructionSpace("Opportunity Knocks", new DrawCardAction(CardType.OpportunityKnocks));
            spaces[37] = new PropertySpace(new DevelopableLand("Hawking Way", 350, Colour.DeepBlue, new int[] { 35, 175, 500, 1100, 1300, 1500 }));
            spaces[38] = new InstructionSpace("Super Tax £100", new PayAction(100, Recipient.Bank));
            spaces[39] = new PropertySpace(new DevelopableLand("Turing Heights", 400, Colour.DeepBlue, new int[] { 50, 200, 600, 1400, 1700, 2000 }));

            this.board = new Board(spaces);

            // ALL CARDS FROM SPREADSHEET
            List<PotLuck> potLuckCards = new List<PotLuck>
            {
                new PotLuck("You inherit £100", new ReceiveMoneyAction(100, Sender.Bank)),
                new PotLuck("You have won 2nd prize in a beauty contest, collect £20", new ReceiveMoneyAction(20, Sender.Bank)),
                new PotLuck("Go back to Crapper Street", new MoveToAction(1, false)),
                new PotLuck("Student loan refund. Collect £20", new ReceiveMoneyAction(20, Sender.Bank)),
                new PotLuck("Bank error in your favour. Collect £200", new ReceiveMoneyAction(200, Sender.Bank)),
                new PotLuck("Pay bill for text books of £100", new PayAction(100, Recipient.Bank)),
                new PotLuck("Mega late night taxi bill pay £50", new PayAction(50, Recipient.Bank)),
                new PotLuck("Advance to go", new MoveToAction(0, true)),
                new PotLuck("From sale of Bitcoin you get £50", new ReceiveMoneyAction(50, Sender.Bank)),
                new PotLuck("Pay a £10 fine or take opportunity knocks", new ChoiceAction(new PayAction(10, Recipient.FreeParking),
                                                                                        new DrawCardAction(CardType.OpportunityKnocks))),
                new PotLuck("Pay insurance fee of £50", new PayAction(50, Recipient.FreeParking)),
                new PotLuck("Savings bond matures, collect £100", new ReceiveMoneyAction(100, Sender.Bank)),
                new PotLuck("Go to jail. Do not pass GO, do not collect £200", new GoToJailAction(30)),
                new PotLuck("Received interest on shares of £25", new ReceiveMoneyAction(25, Sender.Bank)),
                new PotLuck("It's your birthday. Collect £10 from each player", new ReceiveMoneyAction(10, Sender.Players)),
                new PotLuck("Get out of jail free", new ReceiveJailFreeAction())
            };

            List<OpportunityKnocks> opKnocksCards = new List<OpportunityKnocks>()
            {
                new OpportunityKnocks("Bank pays you dividend of £50", new ReceiveMoneyAction(50, Sender.Bank)),
                new OpportunityKnocks("You have won a lip sync battle. Collect £100", new ReceiveMoneyAction(100, Sender.Bank)),
                new OpportunityKnocks("Advance to Turing Heights", new MoveToAction(39, true)),
                new OpportunityKnocks("Advance to Han Xin Gardens. If you pass GO, collect £200", new MoveToAction(25, true)),
                new OpportunityKnocks("Fined £15 for speeding", new PayAction(15, Recipient.FreeParking)),
                new OpportunityKnocks("Pay university fees of £150", new PayAction(150, Recipient.Bank)),
                new OpportunityKnocks("Take a trip to Hove station. If you pass GO collect £200", new MoveToAction(15, true)),
                new OpportunityKnocks("Loan matures, collect £150", new ReceiveMoneyAction(150, Sender.Bank)),
                new OpportunityKnocks("You are assessed for repairs, £40/house, £115/hotel", new PayRepairsAction(40, 115)),
                new OpportunityKnocks("Advance to GO", new MoveToAction(0, true)),
                new OpportunityKnocks("You are assessed for repairs, £25/house, £100/hotel", new PayRepairsAction(25,100)),
                new OpportunityKnocks("Go back 3 spaces", new MoveNSpacesAction(3, false)),
                new OpportunityKnocks("Advance to Skywalker Drive. If you pass GO collect £200", new MoveToAction(12, true)),
                new OpportunityKnocks("Go to jail. Do not pass GO, do not collect £200", new GoToJailAction(10)),
                new OpportunityKnocks("Drunk in charge of a skateboard. Fine £20", new PayAction(20, Recipient.FreeParking)),
                new OpportunityKnocks("Get out of jail free", new ReceiveJailFreeAction())
            };

            // randomly shuffle the pot luck cards into a pile
            List<PotLuck> shuffledPotLucks = Shuffle(potLuckCards);
            this.potLuckPile = new Queue<PotLuck>(shuffledPotLucks);

            // randomly shuffle opportunity knocks cards into a pile
            List<OpportunityKnocks> shuffledOpKnocks = Shuffle(opKnocksCards);
            this.opportunityKnocksPile = new Queue<OpportunityKnocks>(shuffledOpKnocks);

            // 6 test players
            this.players = new IPlayer[] 
            {
                new HumanPlayer("Bob", Token.Boot),
                new HumanPlayer("Sarah", Token.Smartphone),
                new HumanPlayer("Hannah", Token.Cat),
                new HumanPlayer("Sam", Token.Hatstand),
                new HumanPlayer("Daniel", Token.Spoon),
                new HumanPlayer("Polly", Token.Goblet)
            };

            this.currentPlayer = 0;
            this.playerTakingTurn = players[currentPlayer];
            this.auction = null;

        }


        public PropertyTycoon(IPlayer[] players) : this()
        {
            this.players = players;
            this.playerTakingTurn = players[currentPlayer];
        }


        // generic method for shuffling cards
        private List<T> Shuffle<T>(List<T> cards)
        {
            // use Random class to randomly pick cards to add to the new list
            // until there are no more cards left to shuffle
            Random rng = new Random();
            int index = 0;
            List<T> shuffled = new List<T>();
            while (cards.Count > 0)
            {
                index = rng.Next(0, cards.Count);
                shuffled.Add(cards[index]);
                cards.RemoveAt(index);
            }

            return shuffled;
        }

        /// <summary>
        /// Draw a card from the top of the Pot Luck pile. The card is placed at the bottom of the pile.
        /// </summary>
        /// <returns>Pot Luck card</returns>
        public PotLuck DrawPotLuck()
        {
            // draw a card from the top of the pile and place it at the bottom
            PotLuck card = potLuckPile.Dequeue();
            potLuckPile.Enqueue(card);

            return card;
        }

        /// <summary>
        /// Draw a card from the top of the Opportunity Knocks pile. The card is placed at the bottom of the pile.
        /// </summary>
        /// <returns>Opportunity Knocks card</returns>
        public OpportunityKnocks DrawOpportunityKnocks()
        {
            // draw a card from the top of the pile and place it at the bottom
            OpportunityKnocks card = opportunityKnocksPile.Dequeue();
            opportunityKnocksPile.Enqueue(card);

            return card;
        }

        public void CheckDoubleRolled(int dice1, int dice2)
        {
            // check if player rolled double and update player state
            if (dice1 == dice2)
            {
                playerTakingTurn.RolledDouble();
            }
        }

        /// <summary>
        /// Move the current player's token by the given spaces they rolled. If the player passes Go,
        /// they will receive £200 cash.
        /// </summary>
        /// <param name="spaces"></param>
        public void MoveToken(int spaces)
        {
            int oldSpace = playerTakingTurn.GetCurrentSpace();
            playerTakingTurn.MoveToken(spaces); // move token

            // if player is on or passed go, receive £200
            if (playerTakingTurn.GetCurrentSpace() <= oldSpace)
            {
                playerTakingTurn.ReceiveCash(200);
            }

        }

        public IBoardSpace GetBoardSpace(int boardSpaceID)
        {
            return this.board.GetSpace(boardSpaceID);
        }

        public IPlayer GetPlayer(int playerID)
        {
            return this.players[playerID];
        }

        public IPlayer GetCurrentPlayer()
        {
            return this.playerTakingTurn;
        }

        public void BuyProperty(IProperty property)
        {
            if (playerTakingTurn.PeekCash() < property.GetPrice())
            {
                throw new PropertyTycoonException("Insufficient cash to purchase property.");
            }
            playerTakingTurn.BuyProperty(property);
            property.SetOwner(playerTakingTurn);
        }

        public void SellProperty(IProperty property)
        {
            playerTakingTurn.SellProperty(property);
            property.SetOwner(null);
        }

        public void Mortgage(IProperty property)
        {
            playerTakingTurn.Mortgage(property);
            property.Mortgage();
        }

        public void Unmortgage(IProperty property)
        {
            playerTakingTurn.Unmortgage(property);
            property.Unmortgage();
        }

        public void Develop(DevelopableLand property)
        {
            // TODO: need to check for development difference
            playerTakingTurn.DevelopProperty(property);
        }

        public void Undevelop(DevelopableLand property)
        {
            playerTakingTurn.UndevelopProperty(property);
        }

        public void TradeProperty(IPlayer otherPlayer, List<IProperty> offer,
                                    List<IProperty> request)
        {
            playerTakingTurn.TradeProperties(offer, request);
            otherPlayer.TradeProperties(request, offer);
        }

        public void PayRent(IProperty property)
        {
            if (playerTakingTurn.PeekCash() < property.GetRent())
            {
                throw new PropertyTycoonException("Insufficient cash to pay rent.");

            } else
            {
                this.playerTakingTurn.DeductCash(property.GetRent());
            }
            
        }

        /// <summary>
        /// Check if the current game has finished (only one player remains).
        /// </summary>
        /// <returns>True if only one player is left (not bankrupt), false otherwise</returns>
        public bool HasGameFinished()
        {
            int remainingPlayers = 0;
            foreach (IPlayer player in players)
            {
                if (!player.IsBankrupt())
                {
                    remainingPlayers++;
                }
            }

            if (remainingPlayers > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void EndTurn()
        {
            this.currentPlayer = (currentPlayer + 1) % players.Length;
            this.playerTakingTurn = players[currentPlayer];
        }
        

    }


    public class PropertyTycoonException : Exception
    {
        public PropertyTycoonException(string message) : base(message)
        {

        }
    }
}
