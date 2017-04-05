using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {
    Vector2 scrollPosition = Vector2.zero;
    int numSel;
    public GUIStyle background;
    string[] upgrades;
    public GameObject hanger, station;
    public  GameObject hangerGui, stationGui;
    public ArrayStatUpgrade[] statUpgradeBars = new ArrayStatUpgrade[2];
    public ArrayStatUpgrade[] shipUpgradeBars = new ArrayStatUpgrade[3];
    string description;
    int cost;
    string img;
    Texture2D texTemp;

    station Stat;
    Controller ship;
    public static bool activated = false;
    bool subMenu;
	// Use this for initialization
	void Start () {
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        Stat = GameObject.FindGameObjectWithTag("Base").GetComponent<station>();
        hanger = Stat.hanger;
        station = Stat.science;
        texTemp = new Texture2D(64, 32);
        //I need to delete this before I actually use this menu will nt show anything as of right now
        upgrades = new string[loadData.data.stationUpgrades.Length];
        for (int i = 0; i < loadData.data.stationUpgrades.Length; i++) {
            upgrades[i] = loadData.data.stationUpgrades[i].upgradeName;
        }
        loadData.data.loadUpgrades();
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

                texTemp = Resources.Load(loadData.data.stationUpgrades[numSel].Img) as Texture2D;
                Debug.Log(Resources.Load(loadData.data.stationUpgrades[numSel].Img));
                Debug.Log(texTemp);
                Debug.Log(loadData.data.stationUpgrades[numSel].Img);
                description = "Build";
            }
            if (GUI.Button(new Rect(170 , 300, 90, 50), "Cancel"))
            {
                activated = false;
            }
            GUI.EndGroup();
        }
        if (subMenu)
        {
            if (!Stat.GetComponent<station>().upgradesUsed.Contains(numSel))
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
                        Stat.molH -= loadData.data.stationUpgrades[numSel].cost;

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
                    }
                    else
                    {
                        description = "Not enough \r\n hydrogen";
                    }
                }
                if (GUI.Button(new Rect(170, 350, 90, 50), "Cancel"))
                {
                    subMenu = false;
                }
                GUI.EndGroup();
            }else if(!hangerGui.activeInHierarchy || !stationGui.activeInHierarchy)
            {
                
               
                switch (numSel)
                {
                    
                    case 0:
                        if (ship.upgrades[(int)IEnum.ShipUpgrades.speed] != 4)
                            shipUpgradeBars[(int)IEnum.ShipUpgrades.speed].statUpgrade[4].GetComponent<UnityEngine.UI.Text>().text = IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.speed]].ToString();
                        if (ship.upgrades[(int)IEnum.ShipUpgrades.capacity] != 4)
                            shipUpgradeBars[(int)IEnum.ShipUpgrades.capacity].statUpgrade[4].GetComponent<UnityEngine.UI.Text>().text = IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.capacity]].ToString();
                        if (ship.upgrades[(int)IEnum.ShipUpgrades.effeciency] != 4)
                            shipUpgradeBars[(int)IEnum.ShipUpgrades.effeciency].statUpgrade[4].GetComponent<UnityEngine.UI.Text>().text = IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.effeciency]].ToString();
                        for(int i = 0; i < ship.upgrades.Length; i++)
                        {
                            setShipUpgradeColor(i, ship.upgrades[i]);
                        }
                        hangerGui.SetActive(true);
                        break;
                    case 1:
                        if (Stat.upgrades[(int)IEnum.StatUpgrades.maxH] != 4)
                            statUpgradeBars[(int)IEnum.StatUpgrades.maxH].statUpgrade[4].GetComponent<UnityEngine.UI.Text>().text = IEnum.Costs[Stat.upgrades[(int)IEnum.StatUpgrades.maxH]].ToString();
                        if (Stat.upgrades[(int)IEnum.StatUpgrades.loss] != 4)
                            statUpgradeBars[(int)IEnum.StatUpgrades.loss].statUpgrade[4].GetComponent<UnityEngine.UI.Text>().text = IEnum.Costs[Stat.upgrades[(int)IEnum.StatUpgrades.loss]].ToString();
                        for (int i = 0; i < Stat.upgrades.Length; i++)
                        {
                            setStatUpgradeColor(i, Stat.upgrades[i]);
                        }
                        stationGui.SetActive(true);
                        break;
                }

                

            }
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
   public void exit()
    {
        subMenu = false;
        hangerGui.SetActive(false);
        stationGui.SetActive(false);
        Stat.calculateValues();
        ship.calculateValues();
    }
    #region upgrades
    public void upgradeStatStorage()
    {
        if(Stat.upgrades[(int)IEnum.StatUpgrades.maxH] == 4)
        {
            statUpgradeBars[(int)IEnum.StatUpgrades.maxH].statUpgrade[4].GetComponent<Text>().text = "At max level";
        }else if(Stat.molH >= IEnum.Costs[Stat.upgrades[(int)IEnum.StatUpgrades.maxH]])
        {
            Stat.molH -= IEnum.Costs[Stat.upgrades[(int)IEnum.StatUpgrades.loss]];
            Stat.upgrades[(int)IEnum.StatUpgrades.maxH]++;
        }else
        {
            statUpgradeBars[(int)IEnum.StatUpgrades.maxH].statUpgrade[4].GetComponent<Text>().text = "Not Enough Hydrogen";

        }
        setStatUpgradeColor((int)IEnum.StatUpgrades.loss, Stat.upgrades[(int)IEnum.StatUpgrades.loss]);
    }
    public void upgradeStatLoss()
    {
        if (Stat.upgrades[(int)IEnum.StatUpgrades.loss] == 4)
        {
            statUpgradeBars[(int)IEnum.StatUpgrades.loss].statUpgrade[4].GetComponent<Text>().text = "At max level";
        }
        else if (Stat.molH >= IEnum.Costs[Stat.upgrades[(int)IEnum.StatUpgrades.loss]])
        {
            Stat.molH -= IEnum.Costs[Stat.upgrades[(int)IEnum.StatUpgrades.loss]];
            Stat.upgrades[(int)IEnum.StatUpgrades.loss]++;
        }
        else
        {
            statUpgradeBars[(int)IEnum.StatUpgrades.loss].statUpgrade[4].GetComponent<Text>().text = "Not Enough Hydrogen";

        }
        setStatUpgradeColor((int)IEnum.StatUpgrades.loss, Stat.upgrades[(int)IEnum.StatUpgrades.loss]);
    }
    public void upgradeShipSpeed()
    {
        if (ship.upgrades[(int)IEnum.ShipUpgrades.speed] == 4)
        {
            shipUpgradeBars[(int)IEnum.ShipUpgrades.speed].statUpgrade[4].GetComponent<Text>().text = "At max level";
        }
        else if (Stat.molH >= IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.speed]])
        {
            Stat.molH -= IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.speed]];
            ship.upgrades[(int)IEnum.ShipUpgrades.speed]++;
        }else
        {
            shipUpgradeBars[(int)IEnum.ShipUpgrades.speed].statUpgrade[4].GetComponent<Text>().text = "Not Enough Hydrogen";
        }
        setShipUpgradeColor((int)IEnum.ShipUpgrades.speed, ship.upgrades[(int)IEnum.ShipUpgrades.speed]);
    }
    public void upgradeShipCapacity()
    {
        if (ship.upgrades[(int)IEnum.ShipUpgrades.capacity] == 4)
        {
            shipUpgradeBars[(int)IEnum.ShipUpgrades.capacity].statUpgrade[4].GetComponent<Text>().text = "At max level";
        }
        else if (Stat.molH >= IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.capacity]])
        {
            Stat.molH -= IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.capacity]];
            ship.upgrades[(int)IEnum.ShipUpgrades.capacity]++;
        }
        else
        {
            shipUpgradeBars[(int)IEnum.ShipUpgrades.capacity].statUpgrade[4].GetComponent<Text>().text = "Not Enough Hydrogen";
        }
        setShipUpgradeColor((int)IEnum.ShipUpgrades.capacity, ship.upgrades[(int)IEnum.ShipUpgrades.capacity]);
    }
    public void upgradeShipEffeciency()
    {
        if (ship.upgrades[(int)IEnum.ShipUpgrades.effeciency] == 4)
        {
            shipUpgradeBars[(int)IEnum.ShipUpgrades.effeciency].statUpgrade[4].GetComponent<Text>().text = "At max level";
        }
        else if (Stat.molH >= IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.effeciency]])
        {
            Stat.molH -= IEnum.Costs[ship.upgrades[(int)IEnum.ShipUpgrades.effeciency]];
            ship.upgrades[(int)IEnum.ShipUpgrades.effeciency]++;
        }
        else
        {
            shipUpgradeBars[(int)IEnum.ShipUpgrades.effeciency].statUpgrade[4].GetComponent<Text>().text = "Not Enough Hydrogen";
        }
        setShipUpgradeColor((int)IEnum.ShipUpgrades.effeciency, ship.upgrades[(int)IEnum.ShipUpgrades.effeciency]);
    }
#endregion
    public void setStatUpgradeColor(int index, int level)
    {
        
        if(level != 0)
        statUpgradeBars[index].statUpgrade[level-1].GetComponent<Image>().color = Color.green;
        Debug.Log("starting");
        if(level > 0)
        {
            setStatUpgradeColor(index, level - 1);
        }
    }
    public void setShipUpgradeColor(int index, int level)
    {
        
        if(level != 0)
        shipUpgradeBars[index].statUpgrade[level-1].GetComponent<Image>().color = Color.green;
        if (level > 0)
        {
            setShipUpgradeColor(index, level - 1);
        }
    }

}
[System.Serializable]
public class ArrayStatUpgrade{
    public GameObject[] statUpgrade = new GameObject[5]; 
}
