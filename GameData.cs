using System;

public class GameData
{
    private List<PotLuck> potLuckCards;
    private List<OpportunityKnocks> opportunityKnocksCards;
    private IBoardSpace[] boardSpaces;
    private int spaces;

	public GameData()
	{
        potLuckCards = new List<PotLuck>();
        opportunityKnocksCards = new List<OpportunityKnocks>();
        boardSpaces = new IBoardSpace[40];
        int spaces = 0;
	}
    
    
    public void AddPotLuckCard(PotLuck card)
    {
        //add pot luck card to list
        potLuckCards.Add(card);
    }

    public void AddOpportunityKnocksCard(OpportunityKnocks card)
    {
        //add pot luck card to list
        opportunityKnocksCards.Add(card);
    }

    public void AddGameSpace(IBoardSpace space)
    {
        //add board space to the next space in the board array
        if (checkSpaces())
        {
            boardSpaces[spaces] = space;
            IncrementSpaces();
        }
        
    }

    public List<PotLuck> GetPotLuckCards()
    {
        return potLuckCards;
    }

    public List<OpportunityKnocks> GetOpportunityKnocks()
    {
        return opportunityKnocksCards;
    }

    public IBoardSpace[] GetBoardSpaces()
    {
        return boardSpaces;
    }

    private int IncrementSpaces()
    {
        spaces++;
    }

    private bool checkSpaces()
    {
        return spaces == 40;
    }

}
