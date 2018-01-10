using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int waypointNum;

    void OnTriggerEnter(Collider other)
    {
        Car car = other.gameObject.GetComponent<Car>();
        if (car)
        {
            if (waypointNum == car.nextWaypoint)
            {
                car.NextWaypoint();
            }
            else if (waypointNum != car.currentWaypoint)
            {
                car.Respawn(Vector3.zero);
            }
        }
    }
}
