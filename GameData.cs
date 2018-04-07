using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyTycoonProject
{
    public class GameData
    {
        private List<PotLuck> potLuckCards;
        private List<OpportunityKnocks> opportunityKnocksCards;
        private IBoardSpace[] boardSpaces;
        private int spaces;

        /// <summary>
        /// Constructor a container object for the game data
        /// </summary>
        public GameData()
        {
            potLuckCards = new List<PotLuck>();
            opportunityKnocksCards = new List<OpportunityKnocks>();
            boardSpaces = new IBoardSpace[40];
            spaces = 0;
        }

        /// <summary>
        /// Add the given card to the correct card pile for the game (Pot Luck or Opportunity Knocks)
        /// </summary>
        /// <param name="card">Card to add to game</param>
        public void AddCard(AbstractCard card)
        {
            // check which type of card this is and add to correct list
            if (card.GetType() == typeof(PotLuck))
            {
                potLuckCards.Add((PotLuck)card);
            }
            else
            {
                opportunityKnocksCards.Add((OpportunityKnocks)card);
            }
        }

        /// <summary>
        /// Add the given board space to the game data
        /// </summary>
        /// <param name="space">Board space</param>
        public void AddBoardSpace(IBoardSpace space)
        {
            //add board space to the next space in the board array if there is space
            if (!CheckFullSpaces())
            {
                boardSpaces[spaces] = space;
                IncrementSpaces();
            }

        }

        /// <summary>
        /// Return the list of Pot Luck cards that will be added to the game
        /// </summary>
        /// <returns>Pot Luck card list</returns>
        public List<PotLuck> GetPotLuckCards()
        {
            return potLuckCards;
        }

        /// <summary>
        /// Return the list of Opportunity Knocks cards that will be added to the game
        /// </summary>
        /// <returns>Opportunity Knocks card list</returns>
        public List<OpportunityKnocks> GetOpportunityKnocksCards()
        {
            return opportunityKnocksCards;
        }

        /// <summary>
        /// Return the array of all board spaces that will be added to the game
        /// </summary>
        /// <returns></returns>
        public IBoardSpace[] GetBoardSpaces()
        {
            return boardSpaces;
        }

        // increment the total number of board spaces that will be in the game
        private void IncrementSpaces()
        {
            spaces++;
        }

        /// <summary>
        /// Check if the board is full (40 spaces).
        /// </summary>
        /// <returns>True if board is full, false otherwise.</returns>
        public bool CheckFullSpaces()
        {
            return spaces == 40;
        }

        /// <summary>
        /// Return the total number of board spaces added to the board currently.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfSpaces()
        {
            return spaces;
        }

    }
}
