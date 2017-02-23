using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public GameObject star;
	public int orbitSpeed;

	// Use this for initialization
	void Start () {
		star = GameObject.Find("Star");
		if (transform.name == "Star"){
			Light starLight = transform.gameObject.AddComponent<Light>();
			starLight.type = LightType.Point;
			starLight.range = transform.localScale.x * 20;
			starLight.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.name != "Star") {
			transform.RotateAround (star.transform.position, Vector3.up, orbitSpeed * Time.deltaTime / Vector3.Distance (transform.position, star.transform.position) * 20);
		}
	}
}
