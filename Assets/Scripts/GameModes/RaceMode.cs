using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : MonoBehaviour {

    GameController gamecontroller;
    public GameObject cpuCar;
    public GameObject playerCar;
    GameObject carPlayer; 

    // Use this for initialization
    void Start () {
        gamecontroller = GetComponent<GameController>();
        for (int i = 0; i < gamecontroller.startPoints.Count -1; i++)
        {
            GameObject carCPU = Instantiate(cpuCar, gamecontroller.startPoints[i].position, gamecontroller.startPoints[i].rotation);
            carCPU.GetComponent<Car>().SetGameController(gamecontroller);
        }
        carPlayer = Instantiate(playerCar, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].position, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].rotation);
        carPlayer.GetComponent<Car>().SetGameController(gamecontroller);

    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().SetTarget(carPlayer.transform);
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (carPlayer.transform);
    }

    // Update is called once per frame
    void LateUpdate () {
		if (gamecontroller.finishedCars >= 3) {
            gamecontroller.GameOver();
        }
	}
}
