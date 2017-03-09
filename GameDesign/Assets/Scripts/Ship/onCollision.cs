using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour {
    bool spaceLeft = true, draining = false, hLeft = true;
    CelestialBody plan;
    public GameObject ship;
    Controller con;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.tag.Equals("Planet"))
        {
            plan = other.GetComponent<planet>();
            draining = true;
        }else if (other.name.Equals("Star"))
        {
            plan = other.GetComponent<star>();
            draining = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        draining = false;
    }
    private void Start()
    {
        con = ship.GetComponent<Controller>();
    }
    private void Update()
    {
        if (hLeft && spaceLeft && draining && Input.GetKey(KeyCode.Space) )
        {
            hLeft = plan.reduceHydrogen();
            spaceLeft = con.vacuum();
        }
    }
}
