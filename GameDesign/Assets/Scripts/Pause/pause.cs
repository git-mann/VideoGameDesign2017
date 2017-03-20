using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pause : MonoBehaviour {
    public GameObject menu;
    public AudioMixer mixer;

    public float masterVolume;
    public float soundsVolume;
    public float musicVolume;
    private bool setting;

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
        setting = true;
    }
    public void save()
    {
        loadData.data.Save();
    }
    private void OnGUI()
    {
        if (setting)
        {
            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 40, 220, 20), "Master Volume");
            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2, 220, 20), "Music Volume");
            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 40, 220, 20), "Sounds Volume");
            masterVolume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 20, 220, 20), masterVolume, -80f, 20f);
            musicVolume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 20, 220, 20), musicVolume, -80f, 20f);
            soundsVolume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 60, 220, 20), soundsVolume, -80f, 20f);

            mixer.SetFloat("MasterVolume", masterVolume);
            mixer.SetFloat("MusicVolume", musicVolume);
            mixer.SetFloat("SoundsVolume", soundsVolume);

            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 220, 90, 50), "Done"))
            {
                setting = false;
            }
        }
    }
}
