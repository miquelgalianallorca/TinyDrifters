using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour {

	public WheelCollider wheelCollider;

	private Vector3 position = new Vector3();
	private Quaternion rotation = new Quaternion ();

	void Update () {
		wheelCollider.GetWorldPose (out position, out rotation);
		transform.position = position;
		//transform.rotation = rotation; //IN THEORY this is enough
		Vector3 rotEu = rotation.eulerAngles;
		//transform.rotation = Quaternion.Euler(new Vector3(-0, 0, 0));
		//Debug.Log (rotEu);
		transform.Rotate(0, rotEu.y, rotEu.z);
	}
}
