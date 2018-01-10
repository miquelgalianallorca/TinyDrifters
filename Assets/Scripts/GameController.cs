using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject path;
    public GameObject starts;
    public List<Vector3> waypoints = new List<Vector3>();
    public List<Transform> startPoints = new List<Transform>();
    public List<Car> cars = new List<Car>();
    public int lapsLimit;

    //UI
    public UITest ui;

    public int finishedCars;

    public GameObject kk1;
    public GameObject kk2;

    void Awake()
    {
        Waypoint[] pathTransforms = path.GetComponentsInChildren<Waypoint>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                waypoints.Add(pathTransforms[i].transform.position);
                pathTransforms[i].waypointNum = i;

            }
        }

        Transform[] startTransforms = starts.GetComponentsInChildren<Transform>();
        for (int i = 0; i < startTransforms.Length; i++)
        {
            if (startTransforms[i] != starts.transform)
            {
                startPoints.Add(startTransforms[i].transform);
            }
        }

        finishedCars = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].UpdateTotalDistance();
        }
        cars = cars.OrderByDescending(car => car.totalDistance).ToList();
        //positions.text  = "1. " + cars[0].gameObject.name + "\n";
        //positions.text += "2. " + cars[1].gameObject.name + "\n";
        //positions.text += "3. " + cars[2].gameObject.name + "\n";

        Debug.Log((kk1.transform.position- kk2.transform.position).magnitude);
    }

    public Vector3 GetWaypointPosition(int waypoint)
    {
        //Instantiate(kk, waypoints[waypoint], Quaternion.identity);
        return waypoints[waypoint];
    }

    public void AddCar(Car car)
    {
        cars.Add(car);
    }

    public void NotifyLap(int lap)
    {
        if (lap > lapsLimit)
        {
            finishedCars++;
            //if (finishedCars == 3)
            //{
            //    //positions.text = "GAME OVER";
                
            //}
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].gameObject.SetActive(false);
        }
    }

    public void RespawnAll()
    {
        Car firstCar = cars[0];
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].currentWaypoint = firstCar.currentWaypoint;
            cars[i].nextWaypoint = firstCar.nextWaypoint;
            cars[i].totalWaypoints = firstCar.totalWaypoints;
            cars[i].Respawn(Vector3.right * i *5);
        }
    }
}
