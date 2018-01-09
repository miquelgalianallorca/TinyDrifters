using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject path;
    public List<Vector3> waypoints = new List<Vector3>();
    public List<Car> cars = new List<Car>();
    public int lapsLimit;

    //UI
    //public Text positions;

    int finishedCars;

    //public GameObject kk;

    void Awake()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                waypoints.Add(pathTransforms[i].position);
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
            if (finishedCars == 3)
            {
                //positions.text = "GAME OVER";
                this.enabled = false;
            }
        }
    }
}
