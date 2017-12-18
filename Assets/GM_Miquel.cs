using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GAME MASTER - MIQUEL
 * For testing purposes only
*/

public class GM_Miquel : MonoBehaviour {

	public GameObject carPrefab;
	public CameraFollow cameraFollow;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject car = Instantiate (carPrefab) as GameObject;
			cameraFollow.target = car.transform;
		}
	}
}
