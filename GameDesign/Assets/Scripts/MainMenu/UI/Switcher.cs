using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Switcher : MonoBehaviour {
    public GameObject content;
    bool popSaves;
    bool settings;
    string[] hello;
    public GameObject con;
    int numSel = -1;
    Vector2 scrollPosition = Vector2.zero;
    public float masterVolume = -30f;
    public float musicVolume = -30f;
    public float soundsVolume = -30f;
    public AudioMixer mixer;
    public AudioClip buttonOver;
    public AudioClip buttonPress;
    AudioSource audioSource;
    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
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
    public void openSettings()
    {
        con.SetActive(true);
        popSaves = false;
        settings = true;
    }

    public void populateSaves()
    {
        hello = System.IO.Directory.GetFileSystemEntries(Application.persistentDataPath + "/");
        for (int i = 0; i < hello.Length; i++)
        {
            hello[i] = hello[i].Substring(71);

        }

        List<string> temp = new List<string>(hello);
        temp.Remove("Unity");
        if (temp.Contains("resume.dat"))
            temp.Remove("resume.dat");
        hello = temp.ToArray();
        settings = false;
        popSaves = true;
        con.SetActive(true);
        numSel = -1;

    }
    public void onButtonOver()
    {
        if (!audioSource.isPlaying) { 
           audioSource.PlayOneShot(buttonOver, 0.1f);
        }
    }
    public void onButtonPress()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(buttonPress, 0.8f);
        }
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

        if (settings)
        {
            
            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 40, 220, 20), "Master Volume");
            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 , 220, 20), "Music Volume");
            GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 40, 220, 20), "Sounds Volume");
            masterVolume = Mathf.Round(GUI.HorizontalSlider(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 20, 220, 20), masterVolume, -80f, 20f));
            musicVolume = Mathf.Round(GUI.HorizontalSlider(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 20, 220, 20), musicVolume, -80f, 20f));
            soundsVolume = Mathf.Round(GUI.HorizontalSlider(new Rect(Screen.width / 2 - 110, Screen.height / 2 + 60, 220, 20), soundsVolume, -80f, 20f));

            mixer.SetFloat("MasterVolume", masterVolume);
            mixer.SetFloat("MusicVolume", musicVolume);
            mixer.SetFloat("SoundsVolume", soundsVolume);

            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 220, 90, 50), "Done"))
            {
                settings = false;
                con.SetActive(false);
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
