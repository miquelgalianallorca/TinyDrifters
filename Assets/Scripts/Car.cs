using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CAR
 * Physics based car controller
*/

public class Car : MonoBehaviour {

	public float accelerationForce = 100;
	public float rotationSpeed = 100;
	public float maxSpeed = 20;

	public Transform carCenter;

	public Transform rightWheel;
	public Transform leftWheel;

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
		float horizontal = 0;
		float vertical   = 0;
		if (gameObject.tag == "Player") {
			horizontal = Input.GetAxis ("Horizontal");
			vertical   = Input.GetAxis ("Vertical");
		} else if (gameObject.tag == "Player2") {
			horizontal = Input.GetAxis ("Horizontal2");
			vertical   = Input.GetAxis ("Vertical2");
		}

		float rotValue = rotationSpeed * Time.deltaTime * horizontal;
		transform.Rotate (Vector3.up, rotValue);
		RotWheels (rotValue);
		if (isGrounded) {
			//rigidBody.AddTorque (transform.up * rotationForce * horizontal, ForceMode.Acceleration);
			rigidBody.AddForce (transform.forward * accelerationForce * vertical, ForceMode.Acceleration);
			rigidBody.velocity = Vector3.ClampMagnitude (rigidBody.velocity, maxSpeed);
		}

	}

	void RotWheels(float rotValue) {
		/*
		if (rightWheel) {
			float val = 0;
			if (rotValue > 0) val = Mathf.Clamp (rotValue, 0, 40);
			else val = Mathf.Clamp (rotValue, -40, 0);
			rightWheel.Rotate (Vector3.forward, val);
		}
		if (leftWheel) {
			float val = Mathf.Clamp (rotValue, 0, 40);
			leftWheel.Rotate (Vector3.forward, rotValue);
		}*/
	}
}
