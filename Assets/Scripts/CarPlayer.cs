using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{

    Car car;

    void Start()
    {
        car = GetComponent<Car>();
    }

    void Update()
    {
        //Car steering
        float horizontal = 0;
        if (gameObject.tag == "Player")
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else if (gameObject.tag == "Player2")
        {
            horizontal = Input.GetAxis("Horizontal2");
        }

        transform.Rotate(Vector3.up, car.rotationSpeed * Time.deltaTime * horizontal);

        if (Input.GetKey(KeyCode.Space))
        {
            car.driftFactor = 1f;
            //car.rotationSpeed = 100;
        }
        else
        {
            car.driftFactor = 0.8f;
            //car.rotationSpeed = 35;
        }
    }

    void FixedUpdate()
    {
        //Car movement
        float vertical = 0;
        if (gameObject.tag == "Player")
        {
            vertical = Input.GetAxis("Vertical");
        }
        else if (gameObject.tag == "Player2")
        {
            vertical = Input.GetAxis("Vertical2");
        }
        car.Accelerate(vertical);
        
        //if (car.rigidBody.velocity.magnitude < car.maxSpeed)
        //{
        //    car.rigidBody.AddForce(transform.forward * 10 * vertical, ForceMode.Impulse);
        //}
        //car.rigidBody.velocity = car.ForwardVelocity() + car.RightVelocity() * 0.9f + transform.up * Vector3.Dot(car.rigidBody.velocity, transform.up);
    }
}
