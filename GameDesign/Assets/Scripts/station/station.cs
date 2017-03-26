using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class station : MonoBehaviour {
    public double molH = 0;
    double maxH = 500;
    double loss = .05;
    public GameObject hanger, science;

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

    public void transfer()
    {
        molH += (.1 - loss);
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
        loss =.05 -( upgrades[(int)IEnum.StatUpgrades.loss] * (.05 / 4));
        maxH = 500 + (Mathf.Pow(upgrades[(int)IEnum.StatUpgrades.maxH], 2f) * 500);
    }
}
