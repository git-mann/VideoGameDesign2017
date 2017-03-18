using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour {
    bool spaceLeft = true, draining = false, hLeft = true, empty = false, transferring = false;
    CelestialBody plan;
    public GameObject ship;
    public GameObject thing;
    station station;
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
        }else if (other.gameObject.tag.Equals("Base"))
        {
            draining = false;
            station = other.GetComponent<station>();
            transferring = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Planet") || other.gameObject.tag.Equals("Star"))
        {
            draining = false;
        }else if (other.gameObject.tag.Equals("Base"))
        {
            transferring = false;
        }
    }
    private void Start()
    {
        con = ship.GetComponent<Controller>();
    }
    private void Update()
    {
        thing = loadData.data.sun;
        if(con.getHydrogen()< con.getMaxHydrogen())
        {
            spaceLeft = true;
        }
        if (hLeft && spaceLeft && draining && Input.GetKey(KeyCode.Space))
        {
            hLeft = plan.reduceHydrogen();
            spaceLeft = con.vacuum();
            thing.GetComponent<ParticleSystem>().Play();
        } else if (station.checkSpace() &&!empty&&transferring && Input.GetAxis("Vacuum")!=0)
        {
            con.transfer();
            station.transfer();
        } else if (Input.GetAxis("Vacuum")== 0)
        {
            thing.GetComponent<ParticleSystem>().Pause();
            thing.GetComponent<ParticleSystem>().Clear();

        }
        if(con.getHydrogen() <= con.getMaxHydrogen() / 10)
        {
            empty = true;
        }
        else
        {
            empty = false;
        }

    }
}
