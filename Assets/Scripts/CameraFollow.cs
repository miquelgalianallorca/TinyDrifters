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
    public bool changingTarget = false;

    void Start () {
		if (target) offset = transform.position - target.position;
	}
	
	//void Update () {
 //       if (target)
 //       {
 //           if (!changingTarget)
 //           {
 //               transform.position = target.position + offset;
 //           }
 //           else
 //           {
 //               transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
 //               if ((transform.position - (target.position + offset)).magnitude < dist)
 //               {
 //                   changingTarget = false;
 //               }
 //           }
 //           //transform.position = target.position + offset;
 //           //transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.smoothDeltaTime);
 //           //transform.position = Vector3.Slerp(transform.position, target.position + offset, smoothFactor * Time.smoothDeltaTime);
 //           //transform.position = Vector3.MoveTowards(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
 //           //transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
 //       }
        
	//}

    void FixedUpdate()
    {
        if (target)
        {
           transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        }

    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget.name != target.name)
        {
            target = newTarget;
            changingTarget = true;
        }
    }

    //IEnumerator Transition()
    //{
    //    float t = 0.0f;
    //    Vector3 startingPos = transform.position;
    //    while (t < 1.0f)
    //    {
    //        t += Time.deltaTime * (Time.timeScale / transitionDuration);


    //        transform.position = Vector3.Lerp(startingPos, target.position, t);
    //        yield return 0;
    //    }

    //}
}
