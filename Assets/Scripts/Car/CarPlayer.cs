using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{

    Car car;
    public TrailRenderer skidMarkPrefab;
	public TrailRenderer skidMarkRedPrefab;
	public Transform skidMarkRightPos;
    public Transform skidMarkLeftPos;
    TrailRenderer skidMarkRight;
    TrailRenderer skidMarkLeft;
    public Material[] skidMarksMaterials;
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
            if (driftTime == 0)
            {
                skidMarkRight = Instantiate(skidMarkPrefab, skidMarkRightPos.position, skidMarkRightPos.rotation) as TrailRenderer;
                skidMarkRight.gameObject.transform.parent = skidMarkRightPos.transform;
                skidMarkLeft = Instantiate(skidMarkPrefab, skidMarkLeftPos.position, skidMarkLeftPos.rotation) as TrailRenderer;
                skidMarkLeft.gameObject.transform.parent = skidMarkLeftPos.transform;
            }
            else if (driftTime > 2f && driftTime < 2.5f)
            {
//                skidMarkRight.material = skidMarksMaterials[1];
//                skidMarkLeft.material = skidMarksMaterials[1];

				skidMarkRight.gameObject.transform.parent = null;
				skidMarkRight.autodestruct = true;
				skidMarkLeft.gameObject.transform.parent = null;
				skidMarkLeft.autodestruct = true;

				skidMarkRight = Instantiate(skidMarkRedPrefab, skidMarkRightPos.position, skidMarkRightPos.rotation) as TrailRenderer;
				skidMarkRight.gameObject.transform.parent = skidMarkRightPos.transform;
				skidMarkLeft = Instantiate(skidMarkRedPrefab, skidMarkLeftPos.position, skidMarkLeftPos.rotation) as TrailRenderer;
				skidMarkLeft.gameObject.transform.parent = skidMarkLeftPos.transform;

                driftBoost = true;
            }
            else
            {
				skidMarkRight.gameObject.transform.parent = null;
				skidMarkRight.autodestruct = true;
				skidMarkLeft.gameObject.transform.parent = null;
				skidMarkLeft.autodestruct = true;

				skidMarkRight = Instantiate(skidMarkPrefab, skidMarkRightPos.position, skidMarkRightPos.rotation) as TrailRenderer;
				skidMarkRight.gameObject.transform.parent = skidMarkRightPos.transform;
				skidMarkLeft = Instantiate(skidMarkPrefab, skidMarkLeftPos.position, skidMarkLeftPos.rotation) as TrailRenderer;
				skidMarkLeft.gameObject.transform.parent = skidMarkLeftPos.transform;

				//                skidMarkRight.material = skidMarksMaterials[0];
				//                skidMarkLeft.material = skidMarksMaterials[0];

				driftBoost = false;
            }
            driftTime += Time.deltaTime;

            // Instantiate(skidMarkPrefab, skidMarkLeft.position, skidMarkLeft.rotation).transform.parent = skidMarkLeft.transform;
            //car.rotationSpeed = 100;
        }
        else
        {
            if (driftBoost)
            {
                boost = true;
                driftBoost = false;
            }
            car.driftFactor = 0.8f;
            if (skidMarkRight)
            {
                skidMarkRight.gameObject.transform.parent = null;
                skidMarkRight.autodestruct = true;
            }
            if (skidMarkLeft)
            {
                skidMarkLeft.gameObject.transform.parent = null;
                skidMarkLeft.autodestruct = true;
            }
            driftTime = 0;
            
            //car.rotationSpeed = 35;
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

        //if (car.rigidBody.velocity.magnitude < car.maxSpeed)
        //{
        //    car.rigidBody.AddForce(transform.forward * 10 * vertical, ForceMode.Impulse);
        //}
        //car.rigidBody.velocity = car.ForwardVelocity() + car.RightVelocity() * 0.9f + transform.up * Vector3.Dot(car.rigidBody.velocity, transform.up);
    }
}
