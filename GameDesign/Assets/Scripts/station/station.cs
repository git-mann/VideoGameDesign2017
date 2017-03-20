using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class station : MonoBehaviour {
    public double molH = 0;
    double maxH = 500;
    public List<int> upgradesUsed;
    SoundOscillator so;
    // Use this for initialization
    void Start () {
        so = this.GetComponent<SoundOscillator>();
        so.frequency = 2525f;
        InvokeRepeating("beep", 0f, 5f);
        InvokeRepeating("unbeep", 0.25f, 5f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up,  Time.deltaTime/ 10);
        
    }
   
    void beep()
    {
        so.gain = 0.005f;
    }
    void unbeep()
    {
        so.gain = 0f;
    }

    public void transfer()
    {
        molH += .1;
    }
    public bool checkSpace()
    {
        if(molH < maxH)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
