using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour {
    bool spaceLeft = true, draining = false, hLeft = true;
    CelestialBody plan;
    public GameObject ship;
    public GameObject thing;
    Controller con;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        thing = other.gameObject;
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
        thing = loadData.data.sun;
        if(con.getHydrogen()< Controller.maxH)
        {
            spaceLeft = true;
        }
        if (hLeft && spaceLeft && draining && Input.GetKey(KeyCode.Space))
        {
            hLeft = plan.reduceHydrogen();
            spaceLeft = con.vacuum();
            thing.GetComponent<ParticleSystem>().Play();
        } else if (Input.GetAxis("Vacuum")== 0)
        {
            thing.GetComponent<ParticleSystem>().Pause();
            thing.GetComponent<ParticleSystem>().Clear();

        }
    }
}
