using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelBL;
	public WheelCollider wheelBR;

	public float maxSteer = 45f;
	public float maxTorque = 50f;
	public float maxSpeed = 100f;
	float currentSpeed;

	private Rigidbody rigidBody;

	void Start() {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		Steer ();
		Drive ();
	}

	void Steer() {
		// Wheel turn
		float steer = Input.GetAxis("Horizontal") * maxSteer;
		wheelFL.steerAngle = steer;
		wheelFR.steerAngle = steer;
	}

	void Drive() {
		currentSpeed = 2f * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
		// Wheel advance and back
		if (currentSpeed < maxSpeed) {
			float torque = Input.GetAxis ("Vertical") * maxTorque;
			wheelFL.motorTorque = torque;
			wheelFR.motorTorque = torque;
			wheelBL.motorTorque = torque;
			wheelBR.motorTorque = torque;
		} else {
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
			wheelBL.motorTorque = 0;
			wheelBR.motorTorque = 0;
		}
	}
}
