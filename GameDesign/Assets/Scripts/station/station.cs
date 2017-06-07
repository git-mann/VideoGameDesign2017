using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class station : NetworkBehaviour {
    public double molH = 1500;
    double maxH = 1500;
    double loss = .05;
    public GameObject hanger, science;

    [SyncVar]
    public int slots;

    public int[] upgrades ;

    public List<int> upgradesUsed;
    SoundOscillator so;
    private void Awake()
    {
        upgrades = new int[2];
    }
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
        if(Time.timeScale < 0.5f)
        {
            so.gain = 0;
        }
    }
   
    void beep()
    {
        so.gain = 0.005f;
    }
    
    void unbeep()
    {
        so.gain = 0f;
    }
    /*
    public void transfer()
    {
        molH += (Controller.drainRate - loss);
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
    public void calculateValues()
    {
        loss =(Controller.drainRate/2) - ( upgrades[(int)IEnum.StatUpgrades.loss] * (Controller.drainRate/ 4));
        maxH = 1500 + (Mathf.Pow(upgrades[(int)IEnum.StatUpgrades.maxH], 2f) * 500);
    }
    */
}
