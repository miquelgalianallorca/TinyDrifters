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
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, car.rotationSpeed * Time.deltaTime * horizontal);
    }

    void FixedUpdate()
    {
        //Car movement
        float vertical = Input.GetAxis("Vertical");
        car.Accelerate(vertical);
    }
}
