using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadData : MonoBehaviour {
    public static loadData data;
    public string sceneName = "";
    public int secX, secZ;
    
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
    

}
