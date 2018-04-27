using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PropertyTycoonLibrary;
using UnityEngine.UI;
using TMPro;

public class Space_1 : MonoBehaviour {


    public IBoardSpace space;
    private int spaceID = 1;

    // Use this for initialization
    void Start () {
        GameObject controller = GameObject.Find("GameController");
        ControllerScript controllerScript = controller.GetComponent<ControllerScript>();
        PropertyTycoon game = controllerScript.game;
        space = game.GetBoardSpace(spaceID);
        Debug.Log(space.GetType());

        // dynamic text based on type of board space
        // assumes you're using Text Mesh Pro UGUI component
        if (space.GetType() == typeof(GoSpace))
        {
            GetComponent<TextMeshProUGUI>().text = spaceID + "\n\nGo!";
            Debug.Log("Found a go space");
        }
        else if (space.GetType() == typeof(JailSpace))
        {
            GetComponent<TextMeshProUGUI>().text = "Just Visiting / Jail Space";
        }
        else if (space.GetType() == typeof(FreeParkingSpace))
        {
            GetComponent<TextMeshProUGUI>().text = "Free Parking";
        }
        else if (space.GetType() == typeof(PropertySpace))
        {
            // cast and get property object and display name
            PropertySpace propertySpace = (PropertySpace)space;
            IProperty property = propertySpace.GetProperty();
            GetComponent<TextMeshProUGUI>().text = spaceID + "\n\n\n" + property.GetPropertyName();
        }
        else if (space.GetType() == typeof(InstructionSpace))
        {
            // cast and get instruction object and display description
            InstructionSpace instructionSpace = (InstructionSpace)space;
            string description = instructionSpace.GetDescription();
            GetComponent<TextMeshProUGUI>().text = spaceID + "\n\n" + description;
        }

    }
	
	// Update is called once per frame
	void Update () {

    }
}
