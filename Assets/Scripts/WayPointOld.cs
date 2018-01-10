using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointOld : MonoBehaviour {

	public int totalLaps = 3;
	public int currentLaps = 0;
	private bool playerEnteredFromBehind;
	private bool playerEnteredFromFront;
	public bool wayPointDone = false;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			float angle = Vector3.Angle (other.attachedRigidbody.velocity, transform.forward);

			if (angle > 90) {
				playerEnteredFromFront = true;
				playerEnteredFromBehind = false;
			} else {
				playerEnteredFromFront = false;
				playerEnteredFromBehind = true;
			}

		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Player")) {
			float angle = Vector3.Angle (other.attachedRigidbody.velocity, transform.forward);
			print (angle);
			if (playerEnteredFromBehind && angle < 90) {
				currentLaps++;
			} else if (playerEnteredFromFront && angle > 90) {
				currentLaps--;
			}

			wayPointDone = currentLaps >= totalLaps;
			playerEnteredFromFront = false;
			playerEnteredFromBehind = false;
		}
	}
}
