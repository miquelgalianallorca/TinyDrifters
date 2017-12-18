using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CAR
 * Physics based car controller
*/

public class Car : MonoBehaviour {

	public float accelerationForce = 100;
	public float rotationForce = 100;
	public float maxSpeed = 20;

	public Transform carCenter;

	Rigidbody rigidBody;
	bool isGrounded = false;
	float distToGround = 0.1f;

	void Start () {
		rigidBody = GetComponent<Rigidbody> (); 
		distToGround = carCenter.position.y;
	}
	
	void FixedUpdate () {
		isGrounded = Physics.Raycast (carCenter.position, -transform.up, distToGround + 0.1f);
		//Debug.DrawRay(carCenter.position, -transform.up * distToGround, Color.red);
		//Debug.Log ("isGrounded: " + isGrounded);

		// Force based movement
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical   = Input.GetAxis ("Vertical");
		if (isGrounded) {
			rigidBody.AddTorque (transform.up * rotationForce * horizontal, ForceMode.Acceleration);
			rigidBody.AddForce (transform.forward * accelerationForce * vertical, ForceMode.Acceleration);
			rigidBody.velocity = Vector3.ClampMagnitude (rigidBody.velocity, maxSpeed);
		}
	}

}
