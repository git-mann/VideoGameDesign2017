using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class star : CelestialBody {
    [SyncVar]
    Vector3 size;
    MeshRenderer rend;
    public Transform ParticleSystemObject;
    public double startingMolH;

    public SectorCenter sector;

    private void Awake()
    {
        loadData.data.onSunSizedChanged.AddListener(assignSize);
        loadData.data.onSunSizedChanged.AddListener(doScale);
    }
    public override void Start ()
	{
        
        
        base.Start();
      //  Debug.Log("Temperature is: " + temperature);
		if (temperature < 30.5) {
			starClass = 1;	
		} else if (temperature >= 30.5 && temperature < 50) { 
			starClass = 2;
		} else if (temperature >= 50 && temperature < 52.5) {
			starClass = 3;
		} else if (temperature >= 52.5 && temperature < 54) {
			starClass = 4;
		} else if (temperature >= 54 && temperature < 55) {
			starClass = 5;
		} else if (temperature >= 55 && temperature < 56.5) { 
			starClass = 6;
		} else if (temperature >= 56.5 && temperature < 60) { 
			starClass = 7;
		}
        loadData.data.onSunSizedChanged.Invoke();

        percentH = 0.9;
		molH = percentH * transform.localScale.x * starClass * 10;
        startingMolH = molH;
       
		Light light = transform.gameObject.AddComponent<Light>();
		light.type= LightType.Point;
		light.range = starClass * 1000;
		light.intensity = starClass;
		rend = transform.gameObject.GetComponent<MeshRenderer>();
       // Debug.Log(starClass);
        rend.material = mats[starClass - 1];
        ParticleSystemObject = this.transform.GetChild(0);
    }

    public bool reduceHydrogen(double drainRate)
    {
        if(molH < 20)
        {
            Debug.Log("Out");

            rend = transform.gameObject.GetComponent<MeshRenderer>();
            rend.material = mats[7];
        }
       // Debug.Log(molH);
        if (molH >= 0)
        {
            molH -= drainRate;

            return true;
        }
        else
        {
            return false;
        }
      
    }

    [Server]
    void assignSize()
    {
        size = gameObject.transform.localScale;
    }
    void doScale()
    {
        gameObject.transform.localScale = size;
    }
    public override void OnStartClient()
    {

        loadData.data.onSunSizedChanged.Invoke();
    }
    ~star(){
        Debug.Log("I am Dieing!!!!");
    }
}
