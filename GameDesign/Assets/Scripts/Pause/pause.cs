using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class pause : MonoBehaviour {
    public GameObject menu, pauseMenu, settingsMenu;
    public Slider masterSlider, musicSlider, soundsSlider;
    public AudioMixer mixer;

    private bool setting;
    private float masterVolume, musicVolume, soundsVolume;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            menu.SetActive(true);
            pauseMenu.SetActive(true);
            Time.timeScale = .001f;
        }
    }
    public void back()
    {
        settingsMenu.SetActive(false);
        menu.SetActive(true);
        pauseMenu.SetActive(true);
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
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void settings()
    {
        setting = true;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        mixer.GetFloat("MasterVolume", out masterVolume);
        mixer.GetFloat("MusicVolume", out musicVolume);
        mixer.GetFloat("SoundsVolume", out soundsVolume);
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        soundsSlider.value = soundsVolume;
    }
    public void save()
    {
        loadData.data.Save();
    }
    public void updateMaster(float value)
    {
        mixer.SetFloat("MasterVolume", value);
    }
    public void updateMusic(float value)
    {
        mixer.SetFloat("MusicVolume", value);
    }
    public void updateSounds(float value)
    {
        mixer.SetFloat("SoundsVolume", value);
    }
}
