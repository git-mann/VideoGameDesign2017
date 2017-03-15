using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour {
    public GameObject sun;
    public int distance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(sun.transform.position, Vector3.up, distance * Time.deltaTime);
	}
}
