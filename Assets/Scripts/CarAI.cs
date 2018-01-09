using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{

    Car car;
    Vector3 goal;

    void Start()
    {
        car = GetComponent<Car>();
        goal = car.NextWaypoint();
    }

    void Update()
    {
        //Car steering
        goal.y = transform.position.y;
        Vector3 targetDir = goal - transform.position;
        float step = car.rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Goal check
        if ((transform.position - goal).magnitude < 5)
        {
            goal = car.NextWaypoint();
        }
    }

    void FixedUpdate()
    {
        //Car movement
        car.Accelerate(1f);

        //Prueba
        ////Method 1
        //Vector3 targetDir = goal - transform.position;
        //float step = car.rotationSpeed * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //transform.rotation = Quaternion.LookRotation(newDir);

        ////Method2
        ////Vector3 targetDir = nodes[waypointIndex].position - transform.position;
        ////float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        ////Debug.Log("angle: " + angle);
        ////float rotAngle;
        ////if (Mathf.Abs(angle) > deltaAngle)
        ////{
        ////    if (angle > 0)
        ////    {
        ////        Debug.Log("mayor");
        ////        rotAngle = Time.deltaTime * rotationSpeed;
        ////        if (rotAngle > angle)
        ////        {
        ////            rotAngle = angle;
        ////        }


        ////    }
        ////    else
        ////    {
        ////        Debug.Log("menor");
        ////        rotAngle = Time.deltaTime * -rotationSpeed;
        ////        if (rotAngle < angle)
        ////        {
        ////            rotAngle = angle;
        ////        }
        ////    }
        ////    Debug.Log("rotAngle: " + rotAngle);
        ////    transform.Rotate(Vector3.up, rotAngle);
        ////}

        //bool isGrounded = true;
        ////Debug.DrawRay(carCenter.position, -transform.up * distToGround, Color.red);
        ////Debug.Log ("isGrounded: " + isGrounded);

        //// Force based movement
        //float horizontal = 0;
        //float vertical = 1;

        ////transform.Rotate(Vector3.up, rotValue);
        //if (isGrounded)
        //{
        //    //rigidBody.AddTorque (transform.up * rotationForce * horizontal, ForceMode.Acceleration);
        //    car.rigidBody.AddForce(transform.forward * car.accelerationForce * vertical, ForceMode.Acceleration);
        //    car.rigidBody.velocity = Vector3.ClampMagnitude(car.rigidBody.velocity, car.maxSpeed);
        //}
    }
}