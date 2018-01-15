using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{

    Car car;
	public Transform skidMarkRightPos;
	public Transform skidMarkLeftPos;
    public TrailRenderer skidMarkPrefab;
	public TrailRenderer skidMarkRedPrefab;
	private TrailRenderer activeSkidMark;
    TrailRenderer skidMarkRight;
    TrailRenderer skidMarkLeft;

	public Transform boostTrailPos;
	public TrailRenderer boostTrailPrefab;
	TrailRenderer boostTrail;
    
    float driftTime = 0;

    bool driftBoost = false;
    bool boost = false;

    //Input names
    public string horizontalAxis;
    public string verticalAxis;
    public string drift;
    public string respawn;

    void Start()
    {
        car = GetComponent<Car>();
    }

    void Update()
    {
        //Car steering
        float horizontal= Input.GetAxis(horizontalAxis);

        transform.Rotate(Vector3.up, car.rotationSpeed * Time.deltaTime * horizontal);
        
        if (Input.GetAxis(drift) != 0)
        {
            car.driftFactor = 1f;
            car.rotationSpeed = car.stats.rotationSpeed;

            if (driftTime == 0) {
				UpdateSkidMark (skidMarkPrefab);

            } else if (driftTime > 2f && driftTime < 2.5f) {
				UpdateSkidMark (skidMarkRedPrefab);
                driftBoost = true;
            } else {
				UpdateSkidMark (skidMarkPrefab);
				driftBoost = false;
            }

            driftTime += Time.deltaTime;
        }
        else
        {
            car.driftFactor = car.stats.driftFactor;
            car.rotationSpeed = car.stats.rotationSpeed * 0.5f;
            driftTime = 0;

            if (driftBoost)
            {
				boostTrail = Instantiate (boostTrailPrefab, boostTrailPos.position, boostTrailPos.rotation) as TrailRenderer;
				boostTrail.gameObject.transform.parent = boostTrailPos;
				StartCoroutine (FreeBoostTrail ());

                boost = true;
                driftBoost = false;
            }
           
			ClearDrifts ();
        }
        if (Input.GetAxis(respawn) != 0)
        {
            car.Respawn(Vector3.zero);
        }
    }

    void FixedUpdate()
    {
        //Car movement
        float vertical = Input.GetAxis(verticalAxis);
        car.Accelerate(vertical);
        if (boost)
        {
            car.Boost(700f);
            boost = false;
        }
    }

	void UpdateSkidMark(TrailRenderer trailPrefab) {
		if (trailPrefab != activeSkidMark) {
			ClearDrifts ();
			activeSkidMark = trailPrefab;
			skidMarkRight = Instantiate (activeSkidMark, skidMarkRightPos.position, skidMarkRightPos.rotation) as TrailRenderer;
			skidMarkRight.gameObject.transform.parent = skidMarkRightPos.transform;
			skidMarkLeft = Instantiate (activeSkidMark, skidMarkLeftPos.position, skidMarkLeftPos.rotation) as TrailRenderer;
			skidMarkLeft.gameObject.transform.parent = skidMarkLeftPos.transform;
		}
	}

	void ClearDrifts() {
		activeSkidMark = null;
		if (skidMarkRight) {
			skidMarkRight.gameObject.transform.parent = null;
			skidMarkRight.autodestruct = true;
		}
		if (skidMarkLeft) {
			skidMarkLeft.gameObject.transform.parent = null;
			skidMarkLeft.autodestruct = true;
		}
	}

    public void DestroyTrails()
    {
        if (skidMarkRight)
        {
            Destroy(skidMarkRight);
        }
        if (skidMarkLeft)
        {
            Destroy(skidMarkLeft);
        }
        if (boostTrail)
        {
            Destroy(boostTrail);
        }
    }

    IEnumerator FreeBoostTrail() {
		
		yield return new WaitForSeconds (1f);
        if (boostTrail)
        {
            boostTrail.gameObject.transform.parent = null;
        }
	}

}
