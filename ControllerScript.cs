using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PropertyTycoonLibrary;

/// <summary>
/// Stores the game object and logic!!!! This is where GUI is connected to backend.
/// </summary>
public class ControllerScript : MonoBehaviour {

    // Use GetComponent<ControllerScript>() to access this game variable from other scripts
    public PropertyTycoon game;

    void Start () {
        // InputParser should be called here to process spreadsheet and spit out GameData
        // object which is used to create the game instance

        // ****** BASICALLY ALL OF THIS CODE CAN BE REMOVED AFTER INPUT PARSER IS FINISHED *******
        GameData data = new GameData();

        // mock board.... really need input parser. ....
        IBoardSpace[] spaces = new IBoardSpace[40];
        spaces[0] = new GoSpace();
        spaces[1] = new PropertySpace(new DevelopableLand("Crapper Street", 60, Colour.Brown, new int[] { 2, 10, 30, 90, 160, 250 }));
        spaces[2] = new InstructionSpace(new DrawCardAction(CardType.PotLuck));
        spaces[3] = new PropertySpace(new DevelopableLand("Gangsters Paradise", 60, Colour.Brown, new int[] { 4, 20, 60, 180, 320, 450 }));
        //.........................more spaces

        // add extra cards here if needed
        data.AddCard(new PotLuck("Receive £100 from Bitcoin sales", new ReceiveMoneyAction(100, Sender.Bank)));
        data.AddCard(new OpportunityKnocks("Pay £50 fine", new PayAction(50, Recipient.Bank)));
        // ****************************************************************************************

        // test players, should be determined from previous menu scenes
        HumanPlayer bob = new HumanPlayer("Bob", Token.Boot);
        HumanPlayer sarah = new HumanPlayer("Sarah", Token.Smartphone);
        IPlayer[] players = new IPlayer[] { bob, sarah };


        // create the game
		game = new PropertyTycoon(data, players);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
