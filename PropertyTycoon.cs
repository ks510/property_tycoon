using System;
using System.Collections.Generic;

namespace PropertyTycoonProject
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
        private Auction auction;
        private Queue<PotLuck> potLuckPile;
        private Queue<OpportunityKnocks> opportunityKnocksPile;
        
        /// <summary>
        /// Constructor for a Property Tycoon game which sets up the game board,
        /// shuffles card piles and adds players to the game.
        /// </summary>
        /// <param name="gameData">Container for all game data</param>
        /// <param name="numOfPlayers">Number of players in this game (2-6)</param>
        public PropertyTycoon(GameData gameData, int numOfPlayers)
        {
            this.board = new Board(gameData.GetBoardSpaces());
            // check that a valid number of players is chosen
            if (numOfPlayers > 1 && numOfPlayers <= 6)
            {
                this.players = new IPlayer[numOfPlayers];
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
    }


    public class PropertyTycoonException : Exception
    {
        public PropertyTycoonException(string message) : base(message)
        {

        }
    }
}
