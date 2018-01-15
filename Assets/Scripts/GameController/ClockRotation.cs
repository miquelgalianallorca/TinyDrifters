using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotation : MonoBehaviour {

    public float rotationSpeed;
	void Update () {
        transform.Rotate(transform.up, Time.deltaTime * rotationSpeed);
	}
}
