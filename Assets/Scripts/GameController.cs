using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject path;
    public GameObject starts;
    public List<Transform> waypoints = new List<Transform>();
    public List<Transform> startPoints = new List<Transform>();
    public List<Car> cars = new List<Car>();
    public int lapsLimit;

    //Car Properties
    public CarProperties[] cpuTypes;
    public GameObject cpuPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Material[] carColors;
    public Sprite[] carIcons;

    //UI
    public UIManagement ui;

    public int finishedCars;

    GameMode gameMode;

    void Awake()
    {
        Checkpoint[] pathTransforms = path.GetComponentsInChildren<Checkpoint>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                waypoints.Add(pathTransforms[i].transform);
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
        //InitRaceMode();
        //InitVersusMode();
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

    public Transform GetWaypointPosition(int waypoint)
    {
        //Instantiate(kk, waypoints[waypoint], Quaternion.identity);
        return waypoints[waypoint];
    }

    public void AddCar(Car car)
    {
        cars.Add(car);
    }
    public void RemoveCar(Car car)
    {
        cars.Remove(car);
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
        DeactivateAllCars();
    }

    public void RespawnAllCars()
    {
        Car firstCar = cars[0];
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].currentCheckpoint = firstCar.currentCheckpoint;
            cars[i].nextCheckpoint = firstCar.nextCheckpoint;
            cars[i].totalCheckpoints = firstCar.totalCheckpoints;
            cars[i].Respawn(Vector3.right * i *5);
        }
    }

    public void ActivateAllCars()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            CarAI carAI = cars[i].GetComponent<CarAI>();
            if (carAI) carAI.enabled = true;
            CarPlayer carPlayer = cars[i].GetComponent<CarPlayer>();
            if (carPlayer) carPlayer.enabled = true;
        }
    }

    public void DeactivateAllCars()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            CarAI carAI = cars[i].GetComponent<CarAI>();
            if (carAI) carAI.enabled = false;
            CarPlayer carPlayer = cars[i].GetComponent<CarPlayer>();
            if (carPlayer) carPlayer.enabled = false;

			// Stop engine sound
			CarSoundManager soundManager = cars[i].GetComponent<CarSoundManager>();
			if (soundManager) soundManager.StopEngine ();
        }
    }

    public void DestroyAllCars()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            Car car = cars[i];
            Destroy(car.gameObject);
        }
        cars.Clear();
    }

    public IEnumerator CountDown(int seconds)
    {
        DeactivateAllCars();
        for (int i = seconds; i > 0; i--)
        {
            ui.SetCountDownText(i.ToString());
            yield return new WaitForSeconds(1);
        }
        ui.SetCountDownText("GO");
        yield return new WaitForSeconds(1);
        ui.SetCountDownText("");
        ActivateAllCars();
        //ResetTimes();
    }

    public void InitRaceMode()
    {
        //ui.racePanel.SetActive(true);
        if (gameMode) gameMode.Deactivate();
        gameMode = gameObject.AddComponent<RaceMode>();
    }

    public void InitVersusMode()
    {
        if (gameMode) gameMode.Deactivate();
        gameMode = gameObject.AddComponent<VersusMode>();
    }

    public void InitDemoMode()
    {
        if (gameMode) gameMode.Deactivate();
        gameMode = gameObject.AddComponent<DemoMode>();
    }
}
