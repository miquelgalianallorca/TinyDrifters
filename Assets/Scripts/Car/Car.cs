using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float accelerationForce = 10;
    public float maxSpeed = 30;
    public float rotationSpeed = 100;
    public GameController gameController;

    public float driftFactor = 0;
    public Rigidbody rigidBody;

    public int currentCheckpoint;
    public int nextCheckpoint;
    public int totalCheckpoints;
    public int lap;
    public float totalDistance;
    public int points = 0;
    public string carName;
    public Sprite icon;

    public float waypointOffset;

    public void Init(CarProperties properties)
    {
        accelerationForce = properties.accelerationForce;
        maxSpeed = properties.maxSpeed;
        rotationSpeed = properties.rotationSpeed;
        driftFactor = properties.driftFactor;
        carName = properties.carName;
        waypointOffset = properties.waypointOffset;
    }

	private CarSoundManager carSoundManager;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentCheckpoint = 0;
        nextCheckpoint = 0;
        totalDistance = 0;
        lap = 1;
        //gameController.AddCar(this);

		carSoundManager = GetComponent<CarSoundManager> ();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public Vector3 ForwardVelocity()
    {
        return transform.forward * Vector3.Dot(rigidBody.velocity, transform.forward);
    }

    public Vector3 RightVelocity()
    {
        return transform.right * Vector3.Dot(rigidBody.velocity, transform.right);
    }

    public Vector3 UpVelocity()
    {
        return transform.up * Vector3.Dot(rigidBody.velocity, transform.up);
    }

    public void Accelerate(float impulse)
    {
       rigidBody.AddForce(transform.forward * accelerationForce * impulse, ForceMode.Acceleration);
       rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
       rigidBody.velocity = ForwardVelocity() + RightVelocity() * driftFactor + UpVelocity();
		// Sound
		if (carSoundManager) carSoundManager.SetVolume (impulse);
    }

    public Transform CheckpointPassed()
    {
        totalCheckpoints++;
        currentCheckpoint = nextCheckpoint;
        nextCheckpoint++;
        if (nextCheckpoint == gameController.waypoints.Count)
        {
            nextCheckpoint = 0;
            lap++;
            gameController.NotifyLap(lap);
        }
        return gameController.GetWaypointPosition(nextCheckpoint);
    }

    public Transform NextCheckpoint()
    {
        int next = nextCheckpoint + 1;
        if (next == gameController.waypoints.Count)
        {
            next = 0;
        }
        return gameController.GetWaypointPosition(nextCheckpoint);
    }

    public void UpdateTotalDistance()
    {
        totalDistance = totalCheckpoints * 10000 - (gameController.GetWaypointPosition(nextCheckpoint).position - transform.position).magnitude;
    }

    public bool IsVisibleInCamera()
    {
        Collider col = gameObject.GetComponent<Collider>();
        if (!col)
            return false;

        // Calculate the planes from the main camera's view frustum
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }

    public void Respawn(Vector3 offset)
    {
        transform.position = gameController.waypoints[currentCheckpoint].position;
        transform.rotation = Quaternion.LookRotation(gameController.waypoints[currentCheckpoint].forward);
        //transform.LookAt(gameController.waypoints[nextCheckpoint]);
        transform.Translate(offset);
        rigidBody.velocity = Vector3.zero;
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
        gameController.AddCar(this);
    }

	public float GetSpeed(){
		return rigidBody.velocity.magnitude;
	}
}
