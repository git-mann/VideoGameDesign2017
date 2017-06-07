using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Events;





public class loadData : MonoBehaviour {
    public UnityEvent onSunSizedChanged;
    public static loadData data;
    public int saveId;
    private void Awake()
    {
        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(this);
            Debug.Log("hello");
        }else
        {
            Destroy(this);
        }
        saveId = "hello".GetHashCode();
        Debug.Log(saveId);
    }

    /*
    public static loadData data;
    public string sceneName = "";
    public int secX, secZ;
    public int seed;
    public List<double> molH;
    public upgrade[] stationUpgrades;

    List<int> costH = new List<int>();
    List<string> upgrades = new List<string>();
    List<string> description = new List<string>();
    List<string> image = new List<string>();
    station station;
    public GameObject sun;

    private void Awake()
    {
        if(data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;

        }
        else if(data != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        this.Save();
    }
    
    private void Start()
    {
        this.initialize();
    }
    #region condense
    //this and expandMol are basically the inverse of each other
    //Condense mol will coppy all of the hydrogen to this object for saving while expand mol coppies them all to planets after loading
    private void condenseMol()
    {
         sun = GameObject.Find("Star");
        Debug.Log(sun);
        molH.Add(sun.GetComponent<star>().molH);
        GameObject[] plans = GameObject.FindGameObjectsWithTag("Planet");
        GameObject.Destroy(sun);
        if (plans.Length != 0)
        {
            for (int i = 0; i < plans.Length; i++)
            {
                molH.Add(plans[i].GetComponent<planet>().molH);
                GameObject.Destroy(plans[i]);
            }
        }
        
    }

    void condenseWODestroy()
    {
         sun = GameObject.FindGameObjectWithTag("Sun");
        molH.Add(sun.GetComponent<star>().molH);
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        if (planets.Length != 0)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                molH.Add(planets[i].GetComponent<planet>().molH);
            }
        }
        else
        {
            GameObject mainBase = GameObject.FindGameObjectWithTag("Base");
            Base home = new Base(mainBase.GetComponent<station>());
            saveBase(home);

        }

    }
    private void condenseBase()
    {
        GameObject mainBase = GameObject.FindGameObjectWithTag("Base");
        
        if (mainBase != null)
        {
            Base home = new Base(mainBase.GetComponent<station>());
            saveBase(home);
            GameObject.Destroy(mainBase);

        }
    }
    #endregion
    public void expandMol()
    {
         sun = GameObject.FindGameObjectWithTag("Sun");
        
        sun.GetComponent<star>().molH = molH[0];
        GameObject[] planetss = GameObject.FindGameObjectsWithTag("Planet");
        this.molH.RemoveAt(0);
        if ( planetss.Length > 0)
        {
            Debug.Log(planetss.Length);
            for (int i = 1; i < planetss.Length; i++)
            {
                planetss[i].GetComponent<planet>().molH = molH[i];
            }
        }
    }

    #region save
   public void saveSector()
    {
        condenseMol();
        
        condenseBase();
        SceneLoader scene = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

        this.seed = scene.seed;
        string fileName = "X" + secX.ToString() + "Z" + secZ.ToString() + ".dat";
        
        BinaryFormatter bf = new BinaryFormatter();
        if(!Directory.Exists(Application.persistentDataPath + sceneName + "/Sectors/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath +"/"+ sceneName + "/Sectors/");
        }
        Debug.Log(Application.persistentDataPath + "/"+ sceneName + "/Sectors/");
        FileStream output = File.Open(Application.persistentDataPath  +"/"+ sceneName + "/Sectors/" + fileName, FileMode.OpenOrCreate);
        SaveSector save = new SaveSector(seed, molH);
        bf.Serialize(output, save);
        output.Close();
    }
   void saveSectorWithoutDestory()
    {
        condenseWODestroy();
        SceneLoader scene = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

        this.seed = scene.seed;
        string fileName = "X" + secX.ToString() + "Z" + secZ.ToString() + ".dat";

        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath +"/"+ sceneName + "/Sectors/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + sceneName + "/Sectors/");
        }
        Debug.Log(Application.persistentDataPath + "/" + sceneName + "/Sectors/");
        FileStream output = File.Open(Application.persistentDataPath + "/" + sceneName + "/Sectors/" + fileName, FileMode.OpenOrCreate);
        SaveSector save = new SaveSector(seed, molH);
        bf.Serialize(output, save);
        output.Close();
    }
    void saveResume()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Resume resume = new Resume();

        resume.SceneName = this.sceneName;

        FileStream output = File.Open(Application.persistentDataPath + "/resume.dat", FileMode.OpenOrCreate);
        bf.Serialize(output, resume);
        output.Close();
        Debug.Log("output resume");
    }    
    void saveScene()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Scene scene = new Scene();
        scene.XSec = secX;
        scene.zSec = secZ;
        if (!Directory.Exists(Application.persistentDataPath +"/" + sceneName + "/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + sceneName + "/");
        }
        FileStream output = File.Open(Application.persistentDataPath + "/" + sceneName + "/scene.dat", FileMode.OpenOrCreate);
        bf.Serialize(output, scene);
        output.Close();
    }
    private void saveShip()
    {
        Controller control = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        BinaryFormatter bf = new BinaryFormatter();
        Ship ship = new Ship(control);
        if (!Directory.Exists(Application.persistentDataPath + "/"+ sceneName + "/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + sceneName + "/");
        }
        FileStream output = File.Open(Application.persistentDataPath + "/" + sceneName + "/ship.dat", FileMode.OpenOrCreate);
        bf.Serialize(output, ship);
        output.Close();
    }
    private void saveBase(Base obje)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + sceneName + "/Sectors/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + sceneName + "/Sectors/");
        }
        FileStream output = File.Open(Application.persistentDataPath + "/" + sceneName + "/Sectors/base.dat", FileMode.OpenOrCreate);
        bf.Serialize(output, obje);
        output.Close();
    }

    public void Save()
    {
        this.saveResume();
        this.saveSectorWithoutDestory();
        this.saveScene();
    }
  public void saveClient(string name)
    {
        Debug.Log(name);
        
    }
    #endregion



    #region load
    bool findResume()
    {
        if (File.Exists(Application.persistentDataPath + "/resume.dat")) { 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream input = File.Open(Application.persistentDataPath + "/resume.dat", FileMode.Open);
        Resume sector = (Resume)bf.Deserialize(input);
        this.sceneName = sector.SceneName;
        return true;
    }else{
    return false;
    }
    }
    void loadScene()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream input = File.Open(Application.persistentDataPath + "/" + sceneName + "/scene.dat", FileMode.OpenOrCreate);
        Scene sector = (Scene)bf.Deserialize(input);
        this.secX = sector.XSec;
        this.secZ = sector.zSec;
    }
    private void loadShip()
    {
        Controller ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream input = File.Open(Application.persistentDataPath + "/" + sceneName + "/ship.dat", FileMode.OpenOrCreate);
        Ship sector = (Ship)bf.Deserialize(input);
        ship.hydrogen = sector.molH ;
        ship.upgrades = sector.upgrades;
        ship.transform.position =new Vector3(sector.posX, ship.transform.position.y,sector.posZ);

    }
    private void loadBase(SceneLoader scene)
    {
        station = scene.loadBase().GetComponent<station>();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream input = File.Open(Application.persistentDataPath + "/" + sceneName + "/Sectors/base.dat", FileMode.OpenOrCreate);
        Base sector = (Base)bf.Deserialize(input);
        station.molH = sector.molH;
        station.upgradesUsed = sector.upgrades;
        station.upgrades = sector.upgradeVals;
    }
    public void loadUpgrades()
    {
        station = GameObject.FindGameObjectWithTag("Base").GetComponent<station>();
        if (station.upgradesUsed.Contains(0))
        {
            GameObject.Find("pause").GetComponent<menu>().setHangerActive();
        }
        if (station.upgradesUsed.Contains(1))
        {
            GameObject.Find("pause").GetComponent<menu>().setStationActive();

        }
    }
    public void load()
    {
        SceneLoader scene = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();


        string fileName = "X" + secX.ToString() + "Z" + secZ.ToString() + ".dat";


        if (File.Exists(Application.persistentDataPath + "/"+ sceneName + "/Sectors/" + fileName))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream input = File.Open(Application.persistentDataPath +"/"+  sceneName + "/Sectors/" + fileName, FileMode.OpenOrCreate);
            SaveSector sector = (SaveSector)bf.Deserialize(input);
            if(secX !=0 || secZ != 0)
            {
                this.seed = sector.seed;
                this.molH = sector.molH;
                scene.loadScene(seed);
                StartCoroutine(loadAfterFrame());
            }
            else
            {
                this.molH = sector.molH;
                loadBase(scene);
                StartCoroutine(loadAfterFrame());

            }
        }
        else
        {
            scene.generateWithoutSeed();
        }
    }
    IEnumerator loadAfterFrame()
    {
        yield return 0;
        expandMol();
    }
    public bool loadResume()
    {
        if (!File.Exists(Application.persistentDataPath + "/resume.dat"))
            return false;
            if (findResume()) { 

            loadSceneName();
        return true;
    }
        else
        {
            return false;
        }
       }

    public void loadSceneName()
    {
        loadScene();
        load();
        loadShip();
    }

    public void setSun()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        Debug.Log("Find sun");
    }
    void initialize()
    {
        string temp = "";
        int count = 1;
        TextAsset text = Resources.Load<TextAsset>("Upgrades/Station/station"); 
        Debug.Log(text.text);
        for(int j = 0; j < text.text.Length; j++)
        {
            if (text.text[j].Equals('\r'))
            {
                switch (count)
                {
                    case 1:
                        upgrades.Add(temp);
                        count++;
                        break;
                    case 2:
                        description.Add(temp);
                        count++;
                        break;
                    case 3:
                        costH.Add(Int32.Parse(temp));
                        count++;
                        break;
                    case 4:
                        image.Add(temp);
                        count = 1;
                        break;    
                }
                
                temp = "";
                
                j++;
                
            }
            else if (text.text[j].Equals('&'))
            {
                break;
            }else
            {
                temp += text.text[j];
            }
        }
        int amt = upgrades.ToArray().Length;
        stationUpgrades = new upgrade[amt];
        for(int a = 0; a < amt; a++)
        {
            stationUpgrades[a] = new upgrade();
            stationUpgrades[a].cost = costH[a];
            stationUpgrades[a].description = description[a];
            stationUpgrades[a].Img = image[a];
            stationUpgrades[a].upgradeName = upgrades[a];
        }
    } 
    #endregion
    */
}
/*
[Serializable]
class SaveSector
{
    public int seed;

    public List<double> molH;

    public SaveSector(int seed,  List<double> molH)
    {
        this.seed = seed;
        this.molH = molH;
    }
}
[Serializable]
class Base
{
    public List<int> upgrades;
    public double molH;
    public int[] upgradeVals;
    public Base(station stat)
    {
        this.upgradeVals = stat.upgrades;
        this.upgrades = stat.upgradesUsed;
        this.molH = stat.molH;
    }
}

[Serializable]
class Scene
{
    public int XSec;
    public int zSec;
}
[Serializable]
class Ship
{
    public double molH;
    public int[] upgrades;
    public float posX, posZ;
    public Ship(Controller control)
    {
        this.molH = control.hydrogen;
        this.upgrades = control.upgrades;
        this.posX = control.transform.position.x;
        this.posZ = control.transform.position.z;
    }
}
[Serializable]
class Resume
{
    public string SceneName;
}
public class upgrade
{
    public string upgradeName;
    public int cost;
    public string description;
    private string img;

    public string Img
    {
        get { return img; }
        set { img = "Upgrades/Station/Images/"+ value; }
    }

}
*/