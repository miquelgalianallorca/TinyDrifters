using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameMode {
    
    UIManagement ui;
    Car carPlayer; 

    // Use this for initialization
    void Start () {
        gamecontroller = GetComponent<GameController>();
        ui = gamecontroller.ui;
        for (int i = 0; i < gamecontroller.startPoints.Count -1; i++)
        {
            GameObject carCPU = Instantiate(Resources.Load("Car CPU New"), gamecontroller.startPoints[i].position, gamecontroller.startPoints[i].rotation) as GameObject;
            carCPU.GetComponent<Car>().SetGameController(gamecontroller);
        }
        carPlayer = (Instantiate(Resources.Load("Car Player"), gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].position, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].rotation) as GameObject).GetComponent<Car>();
        carPlayer.SetGameController(gamecontroller);
        Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(carPlayer.transform.position);
        gamecontroller.finishedCars = 0;
        StartCoroutine(gamecontroller.CountDown(3));
    }

    private void Update()
    {
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
