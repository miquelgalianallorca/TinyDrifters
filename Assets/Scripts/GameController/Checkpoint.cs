using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkPointNum;

    void OnTriggerEnter(Collider other)
    {
        Car car = other.gameObject.GetComponent<Car>();
        if (car)
        {
            if (checkPointNum == car.nextCheckpoint)
            {
                car.CheckpointPassed();
            }
            else if (checkPointNum != car.currentCheckpoint)
            {
                car.Respawn(Vector3.zero);
            }
        }
    }
}
