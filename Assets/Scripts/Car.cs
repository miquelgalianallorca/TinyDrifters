using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	Rigidbody rigidBody;
	public float accelerationForce = 100;
	public float rotationForce = 100;
	public float maxSpeed = 20;

	void Start () {
		rigidBody = GetComponent<Rigidbody> (); 
	}
	
	void FixedUpdate () {
		// Force based movement
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical   = Input.GetAxis ("Vertical");
		rigidBody.AddTorque (transform.up * rotationForce * horizontal);
		rigidBody.AddForce (transform.forward * accelerationForce * vertical, ForceMode.Acceleration);

		rigidBody.velocity = Vector3.ClampMagnitude (rigidBody.velocity, maxSpeed);
	}
}
