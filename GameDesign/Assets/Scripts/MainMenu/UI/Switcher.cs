using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switcher : MonoBehaviour {
    public GameObject content;
    bool popSaves;
    string[] hello;
    public GameObject con;
    int numSel = -1;
    public void resumeScene()
    {
        SceneManager.LoadScene("RandomBasedOnSeed");
    }
    public void newScene()
    {
        loadData.data.sceneName = "new";
        SceneManager.LoadScene("RandomBasedOnSeed");
    }

    public void populateSaves()
    {
        hello = System.IO.Directory.GetFileSystemEntries(Application.persistentDataPath + "/");
        for(int i = 0; i < hello.Length; i++)
        {
            hello[i] = hello[i].Substring(71);
           
        }
        
        List<string> temp = new List<string>(hello);
        temp.Remove("Unity");
        hello = temp.ToArray();
        popSaves = true;
        con.SetActive(true);

        
    }
    private void OnGUI()
    {
       
        if(popSaves)
        numSel = (GUI.SelectionGrid(new Rect(Screen.width/2-100, Screen.height/2, 200, 200), -1, hello, 1));

    }
    private void Update()
    {
        if(numSel != -1)
        {
            Debug.Log(numSel);
            loadData.data.sceneName = hello[numSel];
            SceneManager.LoadScene("RandomBasedOnSeed");
        }
        Debug.Log(numSel);
    }
}
