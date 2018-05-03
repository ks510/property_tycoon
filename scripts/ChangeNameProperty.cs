using UnityEngine;
using System.Collections;
using PropertyTycoonLibrary;

public class ChangeNameProperty : MonoBehaviour {

    GameObject controller;
    ControllerScript controllerScript;
    int spaceID = 1;

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("GameController");
        controllerScript = controller.GetComponent<ControllerScript>();
        PropertyTycoon game = controllerScript.game;
        PropertySpace space = (PropertySpace)game.GetBoardSpace(spaceID);
        IProperty property = space.GetProperty();
        this.GetComponent<UnityEngine.UI.Text>().text = property.GetPropertyName();
    }
	
	// Update is called once per frame
	void Update () {

    }
}
