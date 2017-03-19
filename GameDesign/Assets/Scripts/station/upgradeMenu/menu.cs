using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {
    int scrollPosition;
    int numSel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        /*
        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 110, Screen.height / 2, 220, 200), scrollPosition, new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100 * hello.Length));
        numSel = (GUI.SelectionGrid(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100 * hello.Length), numSel, hello, 1));
        GUI.EndScrollView();
        if (GUI.Button(new Rect(Screen.width / 2 - 145, Screen.height / 2 + 220, 90, 50), "Load Scene"))
        {
            
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 220, 90, 50), "Cancel"))
        {
            popSaves = false;
            con.SetActive(false);
        }
        */
    }
}
