using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunLevelIndicator : MonoBehaviour {

    GameObject sun;
    Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	/*
	// Update is called once per frame
	void Update () {
        if (sun)
        {
            image.fillAmount = (float)(sun.GetComponent<star>().molH / sun.GetComponent<star>().startingMolH);
            image.color = sun.GetComponent<MeshRenderer>().material.color;
        }
        else
        {
            sun = GameObject.FindGameObjectWithTag("Sun");
            image.fillAmount = 1;
        }
	}
    */
}
