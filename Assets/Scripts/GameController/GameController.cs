using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Circuit
    public GameObject path;
    public GameObject starts;
    public List<Transform> checkPoints = new List<Transform>();
    public List<Transform> startPoints = new List<Transform>();

    //Cars
    public List<Car> cars = new List<Car>();
    public CarProperties[] cpuTypes;
    public GameObject cpuPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Material[] carColors;
    public Sprite[] carIcons;

    //Game mode options
    public int lapsLimit;
    public int pointsLimit;
    GameMode gameMode;

    //UI
    public UIManagement gameUI;
    public MenuManagement menuUI;

    void Awake()
    {
        //Init checkpoints list
        Checkpoint[] pathCheckPoints = path.GetComponentsInChildren<Checkpoint>();
        for (int i = 0; i < pathCheckPoints.Length; i++)
        {
            if (pathCheckPoints[i] != path.transform)
            {
                checkPoints.Add(pathCheckPoints[i].transform);
                pathCheckPoints[i].checkPointNum = i;
            }
        }

        //Init start points list
        Transform[] startTransforms = starts.GetComponentsInChildren<Transform>();
        for (int i = 0; i < startTransforms.Length; i++)
        {
            if (startTransforms[i].CompareTag("StartPoint"))
            {
                startPoints.Add(startTransforms[i].transform);
            }
        }
    }
    
    void Start()
    {
        SetGameMode("demo");
    }

    void Update()
    {
        //Calculate car positions
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].UpdateTotalDistance();
        }
        cars = cars.OrderByDescending(car => car.totalDistance).ToList();
    }

    public void GameOver()
    {
        DeactivateAllCars();
        menuUI.ActivateRetryMenu();
        gameMode.enabled = false;
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

            // Start engine sound
            CarSoundManager soundManager = cars[i].GetComponent<CarSoundManager>();
            if (soundManager) soundManager.StartEngine();
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
        gameMode.enabled = false;
        gameUI.SetResultText("");
        for (int i = seconds; i > 0; i--)
        {
            gameUI.SetCountDownText(i.ToString());
            yield return new WaitForSeconds(1);
        }
        gameUI.SetCountDownText("GO");
        yield return new WaitForSeconds(1);
        gameUI.SetCountDownText("");
        gameMode.enabled = true;
        ActivateAllCars();
    }

    public void SetGameMode(string mode)
    {
        if (gameMode)
            Destroy(gameMode);
        switch (mode)
        {
            case "race":
                gameMode = gameObject.AddComponent <RaceMode> ();
                break;
            case "versus":
                gameMode = gameObject.AddComponent<VersusMode>();
                break;
            case "demo":
                gameMode = gameObject.AddComponent<DemoMode>();
                break;
        }
        ActivateGameMode();
    }

    public void ActivateGameMode()
    {
        DestroyAllCars();
        gameMode.Activate();
    }

    public int GetCarPosition(Car carToFind)
    {
        int pos = 0;
        for (int i = 0; i < cars.Count; i++)
        {
            Car car = cars[i];
            if (car == carToFind)
            {
                pos = i + 1;
                break;
            }
        }
        return pos;
    }
}
