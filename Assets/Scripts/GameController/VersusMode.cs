using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusMode : GameMode
{
    Car carPlayer1;
    Car carPlayer2;

    public override void Activate()
    {
        //Generate Player1 car
        carPlayer1 = Instantiate(gameController.player1Prefab, gameController.startPoints[0].position, gameController.startPoints[0].rotation).GetComponent<Car>();

        //Add Car to Game Controller
        gameController.cars.Add(carPlayer1);
        carPlayer1.SetGameController(gameController);

        //Generate Player2 car
        carPlayer2 = Instantiate(gameController.player2Prefab, gameController.startPoints[1].position, gameController.startPoints[1].rotation).GetComponent<Car>();

        //Add Car to Game Controller
        gameController.cars.Add(carPlayer2);
        carPlayer2.SetGameController(gameController);

        //Init camera
        //Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gameController.cars[0].transform.position);
        Camera.main.gameObject.GetComponent<CameraFollow>().target = gameController.cars[0].transform;

        //Init UI
        menuUI.DeactivateMenu();
        gameUI.ActivateVersusUI();
        gameUI.SetPoints(carPlayer1.points, carPlayer2.points);

        //Count down starts
        StartCoroutine(gameController.CountDown(3));
    }

    void LateUpdate()
    {
        gameUI.SetPoints(carPlayer1.points, carPlayer2.points);
        Car firstCar = gameController.cars[0];
        Car secondCar = gameController.cars[1];

        //Camera follows first
        Camera.main.gameObject.GetComponent<CameraFollow>().target = firstCar.transform;

        //Update UI
        gameUI.UpdateP1Speed(carPlayer1.ForwardVelocity().magnitude, carPlayer1.maxSpeed);
        gameUI.UpdateP2Speed(carPlayer2.ForwardVelocity().magnitude, carPlayer2.maxSpeed);

        //Check round winner
        if (!secondCar.IsVisibleInCamera())
        {
            firstCar.points++;

            //Respawn cars and reset camera
            gameController.RespawnAllCars();
            Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(firstCar.transform.position);
            //Camera.main.gameObject.GetComponent<CameraFollow>().target = firstCar.transform;

            //Update UI
            gameUI.SetPoints(carPlayer1.points, carPlayer2.points);
            gameUI.UpdateP1Speed(carPlayer1.ForwardVelocity().magnitude, carPlayer1.maxSpeed);
            gameUI.UpdateP2Speed(carPlayer2.ForwardVelocity().magnitude, carPlayer2.maxSpeed);

            //Check Game Over conditions
            if (firstCar.points == gameController.pointsLimit)
            {
                gameUI.SetResultText(firstCar.carName + " Wins");
                gameController.GameOver();
            }
            else
            {
                StartCoroutine(RoundResult(2, firstCar.carName));
            }
        }
    }

    public IEnumerator RoundResult(int seconds, string player)
    {
        gameController.DeactivateAllCars();
        gameUI.SetResultText(player + " Wins Round");
        yield return new WaitForSeconds(seconds);
        StartCoroutine(gameController.CountDown(3));
    }
}