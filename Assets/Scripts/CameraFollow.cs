using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Vector3 offset;
	public Transform target;

	void Start () {
		if (target) offset = transform.position - target.position;
	}
	
	void Update () {
		if (target) transform.position = target.position + offset;
	}
}
