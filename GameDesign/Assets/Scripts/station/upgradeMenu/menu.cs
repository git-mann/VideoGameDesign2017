using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {
    Vector2 scrollPosition = Vector2.zero;
    int numSel;
    List<string> preUpgrades = new List<string>();
    string[] upgrades;
    public static bool activated = false;
	// Use this for initialization
	void Start () {
        //I need to delete this before I actually use this menu
        upgrades = preUpgrades.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        if (activated)
        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 110, Screen.height / 2, 220, 200), scrollPosition, new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100 * upgrades.Length));
        numSel = (GUI.SelectionGrid(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100 * upgrades.Length), numSel, upgrades, 1));
        GUI.EndScrollView();
        if (GUI.Button(new Rect(Screen.width / 2 - 145, Screen.height / 2 + 220, 90, 50), "Load Scene"))
        {
            
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 220, 90, 50), "Cancel"))
        {
            
        }
        
    }
}
