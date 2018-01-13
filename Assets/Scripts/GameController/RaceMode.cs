using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameMode {
    
    UIManagement ui;
    Car carPlayer;

    public override void Activate()
    {
        gamecontroller = GetComponent<GameController>();
        ui = gamecontroller.ui;
        for (int i = 0; i < gamecontroller.startPoints.Count - 1; i++)
        {
            GameObject carCPU = Instantiate(gamecontroller.cpuPrefab, gamecontroller.startPoints[i].position, gamecontroller.startPoints[i].rotation) as GameObject;
            carCPU.GetComponent<Car>().SetGameController(gamecontroller);
            carCPU.GetComponent<Car>().Init(gamecontroller.cpuTypes[Random.Range(0, gamecontroller.cpuTypes.Length)]);
            carCPU.GetComponent<Car>().icon = gamecontroller.carIcons[i];
            // Car color
            if (gamecontroller.carColors[i])
            {
                Renderer carRenderer = carCPU.GetComponentInChildren<Renderer>();
                carRenderer.material = gamecontroller.carColors[i];
            }
        }
        carPlayer = (Instantiate(gamecontroller.player1Prefab, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].position, gamecontroller.startPoints[gamecontroller.startPoints.Count - 1].rotation) as GameObject).GetComponent<Car>();
        carPlayer.SetGameController(gamecontroller);
        Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(carPlayer.transform.position);
        gamecontroller.finishedCars = 0;
        gamecontroller.ui.SetTotalLaps(gamecontroller.lapsLimit);

        //Init Menu
        gameController.menuUI.DeactivateMenu();
        gameController.ui.ActivateRaceUI();

        StartCoroutine(gamecontroller.CountDown(3));
    }
        // Use this for initialization
        void Start () {
        
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (carPlayer.transform);

        //Update UI
        ui.UpdateP1Speed(carPlayer.ForwardVelocity().magnitude, carPlayer.maxSpeed);
        //ui.SetSpeedSlider(carPlayer.ForwardVelocity().magnitude / carPlayer.maxSpeed);
        //ui.SetSpeedText(Mathf.RoundToInt(Mathf.Clamp(carPlayer.ForwardVelocity().magnitude, 0, carPlayer.maxSpeed)));
        ui.SetCurrentLap(carPlayer.lap);
        ui.PrintTime(gamecontroller.totalTime);
    }

    // Update is called once per frame
    void LateUpdate () {

        ui.SetFirstPosition(gamecontroller.cars[0].icon);
        ui.SetSecondPosition(gamecontroller.cars[1].icon);
        ui.SetThirdPosition(gamecontroller.cars[2].icon);
        ui.SetFourthPosition(gamecontroller.cars[3].icon);
        if (gamecontroller.finishedCars >= gamecontroller.cars.Count - 1 || carPlayer.lap > gamecontroller.lapsLimit) {
            if (carPlayer.lap > gamecontroller.lapsLimit)
                carPlayer.lap = gamecontroller.lapsLimit;
            gamecontroller.GameOver();
            
            int pos = gamecontroller.GetCarPosition(carPlayer);
            string resultText = "";
            switch (pos)
            {
                case 1:
                    resultText = "1st";
                    break;
                case 2:
                    resultText = "2nd";
                    break;
                case 3:
                    resultText = "3rd";
                    break;
                case 4:
                    resultText = "4th";
                    break;
            }
            resultText = "You finished " + resultText;

            ui.SetResultText(resultText);
        }
	}
}
