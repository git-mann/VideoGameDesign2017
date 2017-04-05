using UnityEngine;
using System.Collections;

public class star : CelestialBody {
    MeshRenderer rend;
    public Transform ParticleSystemObject;
    public double startingMolH;
	public override void Start ()
	{
        base.Start();
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

		percentH = 0.9;
		molH = percentH * transform.localScale.x * starClass * 10;
        startingMolH = molH;
       
		Light light = transform.gameObject.AddComponent<Light>();
		light.type= LightType.Point;
		light.range = starClass * 1000;
		light.intensity = starClass;
		rend = transform.gameObject.GetComponent<MeshRenderer>();
        rend.material = mats[starClass - 1];

        ParticleSystemObject = this.transform.GetChild(0);
    }
    public new  bool reduceHydrogen()
    {
        if(molH < 20)
        {
            Debug.Log("Out");

            rend = transform.gameObject.GetComponent<MeshRenderer>();
            rend.material = mats[7];
        }
        Debug.Log(molH);
        return base.reduceHydrogen();
    }
}
