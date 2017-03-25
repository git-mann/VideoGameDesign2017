using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {
    Vector2 scrollPosition = Vector2.zero;
    int numSel;
    public GUIStyle background;
    string[] upgrades;
    public GameObject hanger, station;

    string description;
    
    int cost;
    string img;
    Texture2D texTemp;

    station Stat;
    public static bool activated = false;
    bool subMenu;
	// Use this for initialization
	void Start () {
        texTemp = new Texture2D(64, 32);
        //I need to delete this before I actually use this menu will nt show anything as of right now
        upgrades = new string[loadData.data.stationUpgrades.Length];
        for (int i = 0; i < loadData.data.stationUpgrades.Length; i++) {
            upgrades[i] = loadData.data.stationUpgrades[i].upgradeName;
        }
        Stat = gameObject.GetComponent<station>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        if (activated)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 440), background);
            //scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 110, Screen.height / 2, 220, 200), scrollPosition, new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100 * upgrades.Length));
            numSel = (GUI.SelectionGrid(new Rect(50, 50, 200, 75 * upgrades.Length), numSel, upgrades, 1));
            //GUI.EndScrollView();
            if (GUI.Button(new Rect(40, 300, 90, 50), "Select"))
            {
                activated = false;
                subMenu = true;
                WWW www = new WWW(loadData.data.stationUpgrades[numSel].Img);
                www.LoadImageIntoTexture(texTemp);
                description = "select";
            }
            if (GUI.Button(new Rect(170 , 300, 90, 50), "Cancel"))
            {
                activated = false;
            }
            GUI.EndGroup();
        }
        if (subMenu)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 440), background);
            GUI.Box(new Rect(50, 50, 200, 50), loadData.data.stationUpgrades[numSel].upgradeName);
            GUI.Box(new Rect(50, 125, 200, 50), loadData.data.stationUpgrades[numSel].description);
            GUI.Box(new Rect(50, 200, 200, 50), "Cost: " + loadData.data.stationUpgrades[numSel].cost);
            GUI.Box(new Rect(50, 275, 200, 50), texTemp);
            if (GUI.Button(new Rect(40, 350, 90, 50), description))
            {
                if (Stat.molH >= loadData.data.stationUpgrades[numSel].cost)
                {
                    switch (numSel)
                    {
                        case 0:
                            setHangerActive();
                            break;
                        case 1:
                            setStationActive();
                            break;
                    }
                    Stat.upgradesUsed.Add(numSel);
                    subMenu = false;
                }else
                {
                    description = "Not enough \r\n hydrogen";
                }
            }
            if (GUI.Button(new Rect(170, 350, 90, 50), "Cancel"))
            {
                subMenu = false;
            }
            GUI.EndGroup();
        }
    }
    public void setHangerActive()
    {
        hanger.SetActive(true);

    }
    public void setStationActive()
    {
        station.SetActive(true);

    }

}
