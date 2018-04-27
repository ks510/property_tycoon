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
            this.playerTakingTurn = null;
            this.auction = null;

            // randomly shuffle the pot luck cards into a pile
            List<PotLuck> shuffledPotLucks = Shuffle(gameData.GetPotLuckCards());
            this.potLuckPile = new Queue<PotLuck>(shuffledPotLucks);

            // randomly shuffle opportunity knocks cards into a pile
            List<OpportunityKnocks> shuffledOpKnocks = Shuffle(gameData.GetOpportunityKnocksCards());
            this.opportunityKnocksPile = new Queue<OpportunityKnocks>(shuffledOpKnocks);

            this.currentPlayer = -1;
        }

        // making a default property tycoon game for testing purposes
        public PropertyTycoon()
        {
            // InputParser should be called here to process spreadsheet and spit out GameData
            // object which is used to create the game instance
            this.playerTakingTurn = null;
            this.currentPlayer = -1;
            this.auction = null;
            this.potLuckPile = new Queue<PotLuck>();
            this.opportunityKnocksPile = new Queue<OpportunityKnocks>();

            // ****** BASICALLY ALL OF THIS CODE CAN BE REMOVED AFTER INPUT PARSER IS FINISHED *******

            // mock board.... add more spaces to fill board if needed... really need input parser. ....
            IBoardSpace[] spaces = new IBoardSpace[40];
            spaces[0] = new GoSpace();
            spaces[1] = new PropertySpace(new DevelopableLand("Crapper Street", 60, Colour.Brown, new int[] { 2, 10, 30, 90, 160, 250 }));
            spaces[2] = new InstructionSpace("Draw Pot Luck", new DrawCardAction(CardType.PotLuck));
            spaces[3] = new PropertySpace(new DevelopableLand("Gangsters Paradise", 60, Colour.Brown, new int[] { 4, 20, 60, 180, 320, 450 }));
            //.........................more spaces
            this.board = new Board(spaces);

            // add extra cards here if needed
            potLuckPile.Enqueue(new PotLuck("Receive £100 from Bitcoin sales", new ReceiveMoneyAction(100, Sender.Bank)));
            opportunityKnocksPile.Enqueue(new OpportunityKnocks("Pay £50 fine", new PayAction(50, Recipient.Bank)));
            // ****************************************************************************************

            // test players, should be determined from previous menu scenes
            HumanPlayer bob = new HumanPlayer("Bob", Token.Boot);
            HumanPlayer sarah = new HumanPlayer("Sarah", Token.Smartphone);
            this.players = new IPlayer[] { bob, sarah };

        }

        // TODO: how to cordinate player turns????
        public void NewTurn()
        {
            currentPlayer = currentPlayer + 1;
            playerTakingTurn = players[currentPlayer];
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

    }


    public class PropertyTycoonException : Exception
    {
        public PropertyTycoonException(string message) : base(message)
        {

        }
    }
}
