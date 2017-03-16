using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class loadData : MonoBehaviour {
    public static loadData data;
    public string sceneName = "";
    public int secX, secZ;
    public int seed;
    public List<double> molH;
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
    //this and expandMol are basically the inverse of each other
    //Condense mol will coppy all of the hydrogen to this object for saving while expand mol coppies them all to planets after loading
    private void condenseMol()
    {
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
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
    private void expandMol()
    {
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        
        sun.GetComponent<star>().molH = molH[0];
        GameObject[] plans = GameObject.FindGameObjectsWithTag("Planet");
        
        if (plans.Length != 0)
        {
            for (int i = 1; i < plans.Length; i++)
            {
                plans[i].GetComponent<planet>().molH = molH[i];
            }
        }
    }

    public void saveSector()
    {
        condenseMol();
        // if the sector is equal to 0,0 save the base
        if (secX == 0 && secZ == 0)
        {

        }
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

    public void load()
    {
        SceneLoader scene = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

        string fileName = "X" + secX.ToString() + "Z" + secZ.ToString() + ".dat";

        if (File.Exists(Application.persistentDataPath + "/"+ sceneName + "/Sectors/" + fileName))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream input = File.Open(Application.persistentDataPath +"/"+  sceneName + "/Sectors/" + fileName, FileMode.OpenOrCreate);
            SaveSector sector = (SaveSector)bf.Deserialize(input);
            if(secX !=0 && secZ != 0)
            {
                this.seed = sector.seed;
                this.molH = sector.molH;
                scene.loadScene(seed);
            }else
            {
                this.molH = sector.molH;
                scene.loadBase();
            }
            
        }
        else
        {
            scene.generateWithoutSeed();
        }
    }

}
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
class Base
{

}

