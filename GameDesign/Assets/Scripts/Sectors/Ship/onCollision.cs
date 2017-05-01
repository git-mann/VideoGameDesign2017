using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Created by Kelby
 * Currently have tag checking in place for on collision enter
 * 
 * ToDo:  ***************************************************************************************
 * telling the star and planet to drain hydrogen
 * putting hydrogen into station
 * use events to check hydrogen
 * 
 * Comment rest of code
 * 
 * Notes:******************************************************************************************
 * Use as little code in the update as possible.
 * Do not use if statements in update. they are slower than switch and it runs every frame
 * 
 */
public class onCollision : MonoBehaviour {
    #region private
    star sun;
    station home;
    Controller con;
    planet planetObject;
    bool fullFuel = false;
    /*
     * using a switch statement I will get what the other object is. 1 = sun, 2 = station, 3 = planet.
     * I dont have to worry about player because the layers cannot collide
     */
    int otherObject = 0;
    #endregion
    private void Start()
    {
        con = gameObject.GetComponent<Controller>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //switch based upon the tag of the other collider
        switch (collision.tag)
        {
            //if it is a sun then 
            case "Sun":
                //set the sun varible equal it its component
                sun = collision.GetComponent<star>();
                //set the other object value to 1
                otherObject = 1;
                Debug.Log("sun");
                break;
            //if it is the station
            case "Base":
                //set the station variable equal to the station
                home = collision.GetComponent<station>();
                //set the index of the object equal to 2
                otherObject = 2;
                break;
            //if the tag is equal to planet
            case "Planet":
                //the planet object is set to the planet component
                planetObject = collision.GetComponent<planet>();
                ///the index is equal to 3
                otherObject = 3;
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        otherObject = 0;
    }

    private void Update()
    {
        //if the spacebar(by default) is held down 
        if (Input.GetAxis("Vacuum") != 0)
        {
            //start a coroutine that will drain hydrogen 10times a second isntead of with framerate
            StartCoroutine(drainHydrogen());
        }
        //twice a second I will check if the hydrogen is at its max
        StartCoroutine(checkHydrogen());
    }
    IEnumerator checkHydrogen()
    {
        //just a simple if statement
        if(con.fuel < con.maxFuel)
        {
            fullFuel = false;
        }
        else
        {
            fullFuel = true;
        }
        
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator drainHydrogen()
    {
        //switch to determine which object to drain hydrogen from
        switch (otherObject)
        {
                 //star
            case 1:
                //if sun.reduce hydrogen with the drainrate of the controller and the ship is not full
                if (sun.reduceHydrogen(con.drainRate) && !fullFuel)
                {
                    //add hydrogen to the ship
                    con.addHydrogen();
                }
               // Debug.Log("drain");
                break;
                //station will put hydrogen into station instead of into ship
            case 2:
                
                break;
                //planet
            case 3:
              
                break;

        }
        yield return new WaitForSeconds(.1f);
    }

    /*
    bool spaceLeft = true, draining = false, hLeft = true, empty = false, transferring = false;
    CelestialBody plan;
    public GameObject ship;
    public GameObject thing;
    station station;
    Controller con;
    public float rotationSpeed = 1f;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.tag.Equals("Player"))
        Physics.IgnoreCollision(GetComponent<Collider>(), other);
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

        if(con.getHydrogen()< con.getMaxHydrogen())
        {
            spaceLeft = true;
        }
        if (thing)
        {
            if (hLeft && spaceLeft && draining && Input.GetKey(KeyCode.Space))
            {
                hLeft = plan.reduceHydrogen();
                spaceLeft = con.vacuum();
                if (thing.tag == "Sun")
                {
                    Quaternion targetRotation = Quaternion.LookRotation(ship.transform.GetChild(2).transform.position - thing.transform.GetChild(0).transform.position);

                    // Smoothly rotate towards the target point.
                    thing.transform.GetChild(0).transform.rotation = Quaternion.Slerp(thing.transform.GetChild(0).transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    thing.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    thing.GetComponent<ParticleSystem>().Play();
                }

            }
            else if (transferring && station)
            {
                if (!empty && Input.GetAxis("Vacuum") != 0)
                {
                    if (station.checkSpace())
                    {
                        con.transfer();
                        station.transfer();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))

                        menu.activated = true;
                }
            }

            else
            {
                if (thing.tag == "Sun")
                {
                    thing.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Pause();
                    //  thing.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Clear();
                }
                else
                {
                    thing.GetComponent<ParticleSystem>().Pause();
                    thing.GetComponent<ParticleSystem>().Clear();

                }
            }
        }
        if(con.getHydrogen() <= 10)
        {
            empty = true;
        }
        else
        {
            empty = false;
        }

    }
    */
}
