using System;

public class GameData
{
    private List<PotLuck> potLuckCards;
    private List<OpportunityKnocks> opportunityKnocksCards;
    private IGameSpace[] boardSpaces;
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

    public void AddGameSpace(IGameSpace space)
    {
        //add gamespace to the next space in the board array
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

    public IGameSpace[] GetBoardSpaces()
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
