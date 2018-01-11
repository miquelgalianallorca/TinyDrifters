using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : MonoBehaviour {

    GameController gamecontroller;
    UIManagement ui;
    public GameObject cpuCar;
    public GameObject playerCar;
    Car carPlayer; 

    // Use this for initialization
    void Start () {
        gamecontroller = GetComponent<GameController>();
        ui = gamecontroller.ui;
        for (int i = 0; i < gamecontroller.startPoints.Count -1; i++)
        {
            GameObject carCPU = Instantiate(cpuCar, gamecontroller.startPoints[i].position, gamecontroller.startPoints[i].rotation);
            carCPU.GetComponent<Car>().SetGameController(gamecontroller);
        }
        carPlayer = Instantiate(playerCar, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].position, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].rotation).GetComponent<Car>();
        carPlayer.SetGameController(gamecontroller);
        StartCoroutine(gamecontroller.CountDown(3));
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().SetTarget(carPlayer.transform);
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (carPlayer.transform);

        //Update UI
        ui.SetSpeedSlider(carPlayer.ForwardVelocity().magnitude / carPlayer.maxSpeed);
        ui.SetSpeedText(Mathf.RoundToInt(Mathf.Clamp(carPlayer.ForwardVelocity().magnitude, 0, carPlayer.maxSpeed)));
        ui.SetCurrentLap(carPlayer.lap);
    }

    // Update is called once per frame
    void LateUpdate () {
		if (gamecontroller.finishedCars >= 3) {
            gamecontroller.GameOver();
        }
	}
}
