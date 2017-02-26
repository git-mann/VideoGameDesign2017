using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public double maxSpeed, hydrogen, fuelPerTime;
	public float  forceAmount, currentSpeed, thrust, turn, shipRotationSpeed, shipThrust, boostThrust;
	public bool allowMovement;
	public Rigidbody rb;


	// Use this for initialization
	void Start () {
		hydrogen = 50;
		rb = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		rb.mass = 1 + (float)(hydrogen/100);
		if (hydrogen > fuelPerTime) {
			allowMovement = true;
		} else {
			allowMovement = false;
		}

		if (Input.GetAxis ("Vacuum") == 1) {
			vacuum ();
		}
		currentSpeed = rb.velocity.magnitude;
		if (allowMovement) {
			if (Input.GetAxis ("Vertical") != 0) {
				thrust = Input.GetAxis ("Vertical") * shipThrust;
				if (Input.GetKey(KeyCode.LeftShift)) {
					thrust *= boostThrust;
				}
			}
			if (Input.GetAxis ("Horizontal") != 0) {
				turn = Input.GetAxis ("Horizontal") * shipRotationSpeed;
			}
		

			rb.AddForce(thrust * transform.forward * Time.deltaTime);
			rb.AddRelativeTorque(transform.up * turn * Time.deltaTime);
		}
	}

	void vacuum ()
	{
	}
}
