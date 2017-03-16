using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switcher : MonoBehaviour {
    
    public void resumeScene()
    {
        SceneManager.LoadScene("RandomBasedOnSeed");
    }
    public void newScene()
    {
        loadData.data.sceneName = "new";
        SceneManager.LoadScene("RandomBasedOnSeed");
    }
}
