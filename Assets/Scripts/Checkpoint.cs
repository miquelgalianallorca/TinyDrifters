using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int waypointNum;

    void OnTriggerEnter(Collider other)
    {
        Car car = other.gameObject.GetComponent<Car>();
        if (car)
        {
            if (waypointNum == car.nextCheckpoint)
            {
                car.CheckpointPassed();
            }
            else if (waypointNum != car.currentCheckpoint)
            {
                car.Respawn(Vector3.zero);
            }
        }
    }
}
