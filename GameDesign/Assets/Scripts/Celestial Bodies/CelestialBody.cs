using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CelestialBody : NetworkBehaviour {
    
	public GameObject star;
	public int orbitSpeed, temperature, starClass;
    public double percentH;
    [SyncVar]
    public double molH;
	public Material[] mats;

	public virtual void Start ()
	{
       
	}

    public virtual void Update()
    {
    }
    public bool reduceHydrogen()
    {
        if (molH < 20 && this.tag == "Sun")
        {
            Debug.Log("Out");

            MeshRenderer rend = transform.gameObject.GetComponent<MeshRenderer>();
            rend.material = mats[7];
        }

        if (molH >= 0)
        {

            return true;
        }else
        {
            return false;
        }
    }
    
}
