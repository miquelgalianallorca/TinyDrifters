using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Vector3 offset;
	public Transform target;
    public float smoothFactor = 5.0f;
    public float smoothTime = 0.3f;
    public float dist = 1;
    Vector3 velocity = Vector3.zero;

    void Start () {
		if (target) offset = transform.position - target.position;
	}


    void FixedUpdate()
    {
        if (target)
        {
           transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        }
    }

    public void SetCameraPosition(Transform newTarget)
    {
        transform.position = newTarget.position + offset;
        target = null;
    }

}
