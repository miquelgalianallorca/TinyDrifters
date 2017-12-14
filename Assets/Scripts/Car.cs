using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	Rigidbody rigidBody;
	public float accelerationForce = 100;
	public float rotationForce = 100;

	void Start () {
		rigidBody = GetComponent<Rigidbody> (); 
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space))
			rigidBody.AddForce (transform.forward * accelerationForce, ForceMode.Acceleration);

		float horizontal = Input.GetAxis ("Horizontal");
		float vertical   = Input.GetAxis ("Vertical");

		rigidBody.AddTorque (transform.up * rotationForce * horizontal);
	}
}
