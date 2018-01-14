using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarProperties stats;

    Rigidbody rigidBody;
    [HideInInspector] public GameController gameController;
    [HideInInspector] public Sprite icon;

    //Properties obtain from scriptable object
    [HideInInspector] public float accelerationForce;
    [HideInInspector] public float maxSpeed;
    [HideInInspector] public float rotationSpeed;
    [HideInInspector] public float driftFactor;
    [HideInInspector] public float waypointOffset;
    [HideInInspector] public float brakeProbability;
    [HideInInspector] public string carName;

    //Gameplay variables
    [HideInInspector] public int currentCheckpoint;
    [HideInInspector] public int nextCheckpoint;
    [HideInInspector] public int totalCheckpoints;
    [HideInInspector] public int lap;
    [HideInInspector] public float totalDistance;
    [HideInInspector] public int points;
    
    public void Init(CarProperties properties)
    {
        stats = properties;
        accelerationForce = properties.accelerationForce;
        maxSpeed = properties.maxSpeed;
        rotationSpeed = properties.rotationSpeed;
        driftFactor = properties.driftFactor;
        carName = properties.carName;
        waypointOffset = properties.waypointOffset;
        brakeProbability = properties.brakeProbability;
    }

	private CarSoundManager carSoundManager;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentCheckpoint = 0;
        nextCheckpoint = 0;
        totalDistance = 0;
        lap = 0;

        if (stats)
        {
            Init(stats);
        }

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
        if (rigidBody.velocity.magnitude < maxSpeed)
        {
            rigidBody.AddForce(transform.forward * accelerationForce * impulse, ForceMode.Acceleration);
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
            rigidBody.velocity = ForwardVelocity() + RightVelocity() * driftFactor + UpVelocity();
        }
        
		// Sound
		if (carSoundManager) carSoundManager.SetVolume (impulse);
    }

    public void Brake(float brakeFactor)
    {
        rigidBody.velocity = ForwardVelocity() * (1-brakeFactor) + RightVelocity() + UpVelocity();
    }

    public void Boost(float impulse)
    {
        rigidBody.AddForce(transform.forward * impulse, ForceMode.Impulse);
    }

    public Transform CheckpointPassed()
    {
        totalCheckpoints++;
        currentCheckpoint = nextCheckpoint;
        nextCheckpoint++;
        if (nextCheckpoint == gameController.checkPoints.Count)
        {
            nextCheckpoint = 0;
        }
        else if (nextCheckpoint == 1)
        {
            lap++;
        }
        return gameController.checkPoints[nextCheckpoint];
    }

    public Transform NextCheckpoint()
    {
        int next = nextCheckpoint + 1;
        if (next == gameController.checkPoints.Count)
        {
            next = 0;
        }
        return gameController.checkPoints[nextCheckpoint];
    }

    public void UpdateTotalDistance()
    {
        totalDistance = totalCheckpoints * 10000 - (gameController.checkPoints[nextCheckpoint].position - transform.position).magnitude;
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
        transform.position = new Vector3(gameController.checkPoints[currentCheckpoint].position.x, transform.position.y, gameController.checkPoints[currentCheckpoint].position.z);
        transform.rotation = Quaternion.LookRotation(gameController.checkPoints[currentCheckpoint].forward);
        //transform.LookAt(gameController.waypoints[nextCheckpoint]);
        transform.Translate(offset);
        rigidBody.velocity = Vector3.zero;
        CarPlayer carPlayer = GetComponent<CarPlayer>();
        if (carPlayer)
        {
            carPlayer.DestroyTrails();
        }
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
        //gameController.AddCar(this);
    }

	public float GetSpeed(){
		return rigidBody.velocity.magnitude;
	}

    public void AdjustSpeedByPosition()
    {
        int pos = gameController.GetCarPosition(this) - 1;
        maxSpeed = stats.maxSpeed * (1 + 0.03f * pos);
    }
}
