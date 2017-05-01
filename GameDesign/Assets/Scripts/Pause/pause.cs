using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Networking;

public class pause : MonoBehaviour {
    /*
    public GameObject menu, pauseMenu, settingsMenu;
    public Slider masterSlider, musicSlider, soundsSlider;
    public AudioMixer mixer;
    public AudioClip buttonOver;
    public AudioClip buttonPress;
    public Canvas canvas;
    AudioSource audioSource;
    public Controller ship;
    private bool setting;
    private float masterVolume, musicVolume, soundsVolume;

    // Use this for initialization
    void Start () {
        audioSource = this.GetComponent<AudioSource>();
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
        Debug.Log(ship);
        loadData.data.saveClient(ship.name);
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
    public void onButtonOver()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(buttonOver);
        }
    }
    public void onButtonPress()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(buttonPress);
        }
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
    */
}
