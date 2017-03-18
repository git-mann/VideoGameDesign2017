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
    Vector2 scrollPosition = Vector2.zero;
    public void resumeScene()
    {
        loadData.data.sceneName = "resume";
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
        if(temp.Contains("resume.dat"))
        temp.Remove("resume.dat");
        hello = temp.ToArray();
        popSaves = true;
        con.SetActive(true);
        numSel = -1;
        
    }
    private void OnGUI()
    {

        if (popSaves)
        {
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 110, Screen.height / 2, 220, 200), scrollPosition, new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100*hello.Length));
            numSel = (GUI.SelectionGrid(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100*hello.Length), numSel, hello, 1));
            GUI.EndScrollView();
            if (GUI.Button(new Rect(Screen.width / 2 - 145, Screen.height / 2 + 220, 90, 50), "Load Scene"))
            {
                switchScene();
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 220, 90, 50), "Cancel"))
            {
                popSaves = false;
                con.SetActive(false);
            }
            if (GUI.Button(new Rect(Screen.width / 2  +55, Screen.height / 2 + 220, 90, 50), "Delete"))
            {
                deleteScene();
            }


        }

    }
    private void switchScene()
    {
        if (numSel != -1)
        {
            Debug.Log(numSel);
            loadData.data.sceneName = hello[numSel];
            SceneManager.LoadScene("RandomBasedOnSeed");
        }
        Debug.Log(numSel);
    }
    private void deleteScene()
    {
        Debug.Log(numSel);
        System.IO.Directory.Delete(Application.persistentDataPath + "/" + hello[numSel], true);
        deleteSceneAtIndex(numSel);
        
    }
    void deleteSceneAtIndex(int index)
    {
        List<string> temp = new List<string>(hello);
        temp.RemoveAt(index);
        hello = temp.ToArray();
    }
}
