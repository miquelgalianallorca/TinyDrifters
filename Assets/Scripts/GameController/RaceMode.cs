using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameMode {
    
    Car carPlayer;
    float totalTime;

    public override void Activate()
    {
        //Generate CPU cars
        for (int i = 0; i < gameController.startPoints.Count - 1; i++)
        {
            Car car = Instantiate(gameController.cpuPrefab, gameController.startPoints[i].position, gameController.startPoints[i].rotation).GetComponent<Car>();

            //Pick random AI
            car.Init(gameController.cpuTypes[Random.Range(0, gameController.cpuTypes.Length)]);

            //Set color and icon
            if (gameController.carColors[i])
            {
                Renderer carRenderer = car.GetComponentInChildren<Renderer>();
                carRenderer.material = gameController.carColors[i];
                car.icon = gameController.carIcons[i];
            }

            //Add Car to Game Controller
            gameController.cars.Add(car);
            car.SetGameController(gameController);
        }

        //Generate Player car
        carPlayer = Instantiate(gameController.player1Prefab, gameController.startPoints[gameController.startPoints.Count - 1].position, gameController.startPoints[gameController.startPoints.Count - 1].rotation).GetComponent<Car>();

        //Add Car to Game Controller
        gameController.cars.Add(carPlayer);
        carPlayer.SetGameController(gameController);

        //Init camera
        Camera.main.gameObject.GetComponent<CameraFollow>().target = carPlayer.transform;

        //Init mode variables
        totalTime = 0f;

        //Init UI
        menuUI.DeactivateMenu();
        gameUI.ActivateRaceUI();
        gameUI.SetTotalLaps(gameController.lapsLimit);
        gameUI.SetCurrentLap(carPlayer.lap);
        gameUI.UpdateP1Speed(carPlayer.ForwardVelocity().magnitude, carPlayer.maxSpeed);
        gameUI.SetTotalTime(totalTime);
        gameUI.SetFirstPosition(gameController.cars[0].icon);
        gameUI.SetSecondPosition(gameController.cars[1].icon);
        gameUI.SetThirdPosition(gameController.cars[2].icon);
        gameUI.SetFourthPosition(gameController.cars[3].icon);

        //Count down starts
        StartCoroutine(gameController.CountDown(3));
    }
    
    void LateUpdate ()
    {
        totalTime += Time.deltaTime;

        //Update UI
        gameUI.SetCurrentLap(carPlayer.lap);
        gameUI.UpdateP1Speed(carPlayer.ForwardVelocity().magnitude, carPlayer.maxSpeed);
        gameUI.SetTotalTime(totalTime);

        gameUI.SetFirstPosition(gameController.cars[0].icon);
        gameUI.SetSecondPosition(gameController.cars[1].icon);
        gameUI.SetThirdPosition(gameController.cars[2].icon);
        gameUI.SetFourthPosition(gameController.cars[3].icon);

        //Check Game Over conditions
        int finished = 0;
        for(int i = 0; i < gameController.cars.Count; i++)
        {
            Car car = gameController.cars[i];
            if (car.lap > gameController.lapsLimit)
            {
                finished++;
            }
        }

        if (finished >= gameController.cars.Count - 1 || carPlayer.lap > gameController.lapsLimit) {
            if (carPlayer.lap > gameController.lapsLimit)
            {
                carPlayer.lap = gameController.lapsLimit;
                gameUI.SetCurrentLap(carPlayer.lap);
            }

            int pos = gameController.GetCarPosition(carPlayer);
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

            gameUI.SetResultText(resultText);
            gameController.GameOver();
        }
	}
}
