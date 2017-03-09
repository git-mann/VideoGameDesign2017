using UnityEngine;
using System.Collections;

public class planet : CelestialBody {
	public override void Start ()
	{
		star = GameObject.Find ("Star");
		if (transform.localScale.x < 10) {
			percentH = 0.05;
		} else {
			percentH = 0.9;
		}
		molH = percentH * transform.localScale.x;
	}

	public override void Update ()
	{
		transform.RotateAround (star.transform.position, Vector3.up, orbitSpeed * Time.deltaTime / Vector3.Distance (transform.position, star.transform.position) * 20);
		transform.Rotate(Vector3.up * Time.deltaTime / transform.localScale.x * 100);
	}
    public double getMol()
    {
        print(molH * percentH);
        return molH * percentH;
    }
}
