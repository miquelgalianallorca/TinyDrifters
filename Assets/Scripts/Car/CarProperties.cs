using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car")]
public class CarProperties : ScriptableObject
{
    public string carName = "CPU";
    public float accelerationForce = 100f;
    public float maxSpeed = 30f;
    public float rotationSpeed = 280f;
    public float driftFactor = 1f;
    public float waypointOffset = 0f;
    public float brakeProbability = 0f;
}
