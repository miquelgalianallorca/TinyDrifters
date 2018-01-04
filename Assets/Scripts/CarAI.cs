using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{

    public float accelerationForce = 100;
    public float rotationSpeed = 1;
    public float maxSpeed = 20;

    public Transform carCenter;

    public Transform rightWheel;
    public Transform leftWheel;

    public GameObject path;
    public float radius = 2f;
    public float deltaAngle;
    public int totalWayPoint = 0;
    public int goalDistance = 2;

    Rigidbody rigidBody;
    bool isGrounded = false;
    float distToGround = 0.1f;

    private List<Transform> nodes = new List<Transform>();
    private Vector3 goal;
    public int waypointIndex = 0;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        distToGround = carCenter.position.y;

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
        if (nodes.Count > 0)
        {
            waypointIndex = 0;
            goal = GetPositionAroundObject(nodes[waypointIndex].transform);
            //transform.LookAt(goal);
        }
    }

    private void Update()
    {
        if ((transform.position - goal).magnitude < goalDistance)
        {
            waypointIndex++;
            totalWayPoint++;
            if (waypointIndex == nodes.Count)
            {
                waypointIndex = 0;
            }
            goal = GetPositionAroundObject(nodes[waypointIndex].transform);
        }
    }

    private void FixedUpdate()
    {
        //Method 1
        Vector3 targetDir = goal - transform.position;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Method2
        //Vector3 targetDir = nodes[waypointIndex].position - transform.position;
        //float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        //Debug.Log("angle: " + angle);
        //float rotAngle;
        //if (Mathf.Abs(angle) > deltaAngle)
        //{
        //    if (angle > 0)
        //    {
        //        Debug.Log("mayor");
        //        rotAngle = Time.deltaTime * rotationSpeed;
        //        if (rotAngle > angle)
        //        {
        //            rotAngle = angle;
        //        }


        //    }
        //    else
        //    {
        //        Debug.Log("menor");
        //        rotAngle = Time.deltaTime * -rotationSpeed;
        //        if (rotAngle < angle)
        //        {
        //            rotAngle = angle;
        //        }
        //    }
        //    Debug.Log("rotAngle: " + rotAngle);
        //    transform.Rotate(Vector3.up, rotAngle);
        //}

        isGrounded = Physics.Raycast(carCenter.position, -transform.up, distToGround + 0.1f);
        //Debug.DrawRay(carCenter.position, -transform.up * distToGround, Color.red);
        //Debug.Log ("isGrounded: " + isGrounded);

        // Force based movement
        float horizontal = 0;
        float vertical = 1;

        float rotValue = rotationSpeed * Time.deltaTime * horizontal;
        //transform.Rotate(Vector3.up, rotValue);
        RotWheels(rotValue);
        if (isGrounded)
        {
            //rigidBody.AddTorque (transform.up * rotationForce * horizontal, ForceMode.Acceleration);
            rigidBody.AddForce(transform.forward * accelerationForce * vertical, ForceMode.Acceleration);
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }

    }
    void RotWheels(float rotValue)
    {
        /*
		if (rightWheel) {
			float val = 0;
			if (rotValue > 0) val = Mathf.Clamp (rotValue, 0, 40);
			else val = Mathf.Clamp (rotValue, -40, 0);
			rightWheel.Rotate (Vector3.forward, val);
		}
		if (leftWheel) {
			float val = Mathf.Clamp (rotValue, 0, 40);
			leftWheel.Rotate (Vector3.forward, rotValue);
		}*/
    }

    Vector3 GetPositionAroundObject(Transform tx)
    {
        Vector3 offset = Random.insideUnitCircle * radius;
        Vector3 pos = tx.position + offset;
        return pos;
    }
}
