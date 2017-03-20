using UnityEngine;
using System.Collections;

public class CelestialBody : MonoBehaviour {

	public GameObject star;
	public int orbitSpeed, temperature, starClass;
	public double percentH, molH;
	public Material[] mats;
    public Transform ship;

	public virtual void Start ()
	{
        ship = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public virtual void Update ()
	{

	}
    public bool reduceHydrogen()
    {
        if (molH >= 0)
        {
            molH -= .1;
            Debug.Log(molH);
            return true;
        }else
        {
            return false;
        }
    }
}
