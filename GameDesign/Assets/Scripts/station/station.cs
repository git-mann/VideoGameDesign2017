using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class station : MonoBehaviour {
    public double molH;
    public List<int> upgradesUsed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up,  Time.deltaTime/ 10);
	}
}
