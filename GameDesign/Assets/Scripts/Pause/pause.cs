using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
    public GameObject menu;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            menu.SetActive(true);
            Time.timeScale = .001f;
        }
    }
    public void saveExit()
    {
        loadData.data.Save();
        Application.Quit();
    }
    public void Resume()
    {
        Debug.Log("resume");
        menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void settings()
    {

    }
    public void save()
    {
        loadData.data.Save();
    }
}
