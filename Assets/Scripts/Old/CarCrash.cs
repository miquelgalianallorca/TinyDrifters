using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * CAR CRASH
 * Checks if car flipped or ended up in bad position.
*/

public class CarCrash : MonoBehaviour {

	public float timeToDie = 0.5f;
	public Transform[] carParts;

	void OnTriggerEnter(Collider collider){
		Debug.Log ("CAR CRASHED");
		// Delete car
		Destroy (transform.parent.gameObject, timeToDie);
	}

}
