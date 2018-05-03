using UnityEngine;
using System.Collections;
using PropertyTycoonLibrary;

public class ChangeColourProperty : MonoBehaviour {

    GameObject controller;
    ControllerScript controllerScript;
    int spaceID = 1; //change this and the script name I suppose...

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("GameController");
        controllerScript = controller.GetComponent<ControllerScript>();
        // don't check what space the current player is on because they don't need to be on the space to view the property info
        // instead we will hard code the board space number to each script and copy and paste...
        PropertyTycoon game = controllerScript.game;
        PropertySpace space = ((PropertySpace)game.GetBoardSpace(spaceID));
        IProperty property = space.GetProperty();
        if (property.IsDevelopable())
        {
            DevelopableLand devLand = (DevelopableLand)property;
            if (devLand.GetColourGroup() == Colour.Blue) { this.GetComponent<UnityEngine.UI.Image>().color = Color.cyan; }
            else if (devLand.GetColourGroup() == Colour.Brown) { this.GetComponent<UnityEngine.UI.Image>().color = new Color(165, 42, 42); }
            else if (devLand.GetColourGroup() == Colour.DeepBlue) { this.GetComponent<UnityEngine.UI.Image>().color = Color.blue; }
            else if (devLand.GetColourGroup() == Colour.Green) { this.GetComponent<UnityEngine.UI.Image>().color = Color.green; }
            else if (devLand.GetColourGroup() == Colour.Orange) { this.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 165, 0); }
            else if (devLand.GetColourGroup() == Colour.Purple) { this.GetComponent<UnityEngine.UI.Image>().color = new Color(160, 32, 240); }
            else if (devLand.GetColourGroup() == Colour.Red) { this.GetComponent<UnityEngine.UI.Image>().color = Color.red; }
            else if (devLand.GetColourGroup() == Colour.Yellow) { this.GetComponent<UnityEngine.UI.Image>().color = Color.yellow; }
        }
    
    }

    // Update is called once per frame
    void Update()
    {
    }
}
