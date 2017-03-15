using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switcher : MonoBehaviour {
    string selectedScene ;
    string systemPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
    string loadPath;
    public void loadScene()
    {
        SceneManager.LoadScene("RandomBasedOnSeed");
    }
    public void newScene()
    {
        loadPath =  systemPath + "\\FinalFrontier\\Scene.txt";
        selectedScene = "new";
        try
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(loadPath);
            writer.Write(selectedScene);
            writer.Close();
        }
        catch (System.Exception)
        {
            System.IO.Directory.CreateDirectory(systemPath + "\\FinalFrontier");
            System.IO.File.Create(loadPath);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(loadPath);
            writer.Write(selectedScene);
            writer.Close();
        }

        SceneManager.LoadScene("RandomBasedOnSeed");
    }
}
