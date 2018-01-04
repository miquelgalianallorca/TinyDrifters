using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGonzalo : MonoBehaviour
{

    public float accelerationForce = 100;
    public float rotationForce = 100;
    public float rotationSpeed = 100;
    public float maxSpeed = 20;
    public float driftFactor = 0;
    Rigidbody rigidBody;
    //public GameObject bullet;
    //public GameObject barrel;

    //public GameObject skidMarkRight;
    //public GameObject skidMarkLeft;

    public float fireDelta = 0.5f;
    private float timeCounter = 0.0f;


    public int nextWaypoint = 0;

    bool boost = false;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //skidMarkRight.SetActive(false);
        //skidMarkLeft.SetActive(false);
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * horizontal);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            driftFactor = 1;
            //skidMarkRight.SetActive(true);
            //skidMarkLeft.SetActive(true);
        }
        else
        {
            driftFactor = 0.9f;
            //skidMarkRight.SetActive(false);
            //skidMarkLeft.SetActive(false);
        }

        if (Input.GetKey(KeyCode.AltGr) && timeCounter > fireDelta)
        {
            Debug.Log("Fire");
            //GameObject firedBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
            //Rigidbody bulletRigidbody = firedBullet.GetComponent<Rigidbody>();
            //bulletRigidbody.AddForce(barrel.transform.forward * 80, ForceMode.Impulse);
            timeCounter = 0.0f;
        }
        if (Input.GetKey(KeyCode.LeftControl) && timeCounter > fireDelta)
        {
            boost = true;
            timeCounter = 0.0f;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        if (rigidBody.velocity.magnitude < maxSpeed)
        {
            rigidBody.AddForce(transform.forward * accelerationForce * vertical, ForceMode.Impulse);
        }


        //rigidBody.AddTorque(transform.up * rotationForce * horizontal, ForceMode.Acceleration);
        //rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        //float horizontal = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * horizontal);
        //rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        rigidBody.velocity = ForwardVelocity() + RightVelocity() * driftFactor + transform.up * Vector3.Dot(rigidBody.velocity, transform.up);

        if (boost)
        {
            Debug.Log("Boost");
            rigidBody.AddForce(transform.forward * 500, ForceMode.Impulse);
            boost = false;
        }
    }

    Vector3 ForwardVelocity()
    {
        return transform.forward * Vector3.Dot(rigidBody.velocity, transform.forward);
    }

    Vector3 RightVelocity()
    {
        return transform.right * Vector3.Dot(rigidBody.velocity, transform.right);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Respawn respawn = other.gameObject.GetComponent<Respawn>();
    //    if (respawn)
    //    {
    //        int currentWaypont = respawn.waypointNum;
    //        if (currentWaypont > nextWaypoint)
    //        {
    //            Transform[] pathTransforms = rc.path.GetComponentsInChildren<Transform>();


    //            transform.position = rc.nodes[nextWaypoint - 1].position;
    //            transform.LookAt(rc.nodes[nextWaypoint].position);
    //            rigidBody.velocity = Vector3.zero;
    //        }
    //        else if (currentWaypont == nextWaypoint)
    //        {
    //            nextWaypoint++;
    //            if (nextWaypoint >= rc.nodes.Count)
    //            {
    //                nextWaypoint = 0;
    //            }
    //        }
    //    }
    //}
}
