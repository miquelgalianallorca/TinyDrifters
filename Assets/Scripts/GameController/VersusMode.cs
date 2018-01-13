using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusMode : GameMode
{

    Car carPlayer1;
    Car carPlayer2;
    public override void Activate()
    {
        gamecontroller = GetComponent<GameController>();
        carPlayer1 = (Instantiate(gamecontroller.player1Prefab, gamecontroller.startPoints[0].position, gamecontroller.startPoints[0].rotation) as GameObject).GetComponent<Car>();
        carPlayer1.SetGameController(gamecontroller);

        carPlayer2 = (Instantiate(gamecontroller.player2Prefab, gamecontroller.startPoints[1].position, gamecontroller.startPoints[1].rotation) as GameObject).GetComponent<Car>();
        carPlayer2.SetGameController(gamecontroller);
        Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gamecontroller.cars[0].transform.position);

        gameController.menuUI.DeactivateMenu();
        gameController.ui.ActivateVersusUI();
        gamecontroller.ui.SetLives(carPlayer1.points, carPlayer2.points);
        StartCoroutine(gamecontroller.CountDown(3));
    }

    // Use this for initialization
    void Start()
    {
        
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (gamecontroller.cars[0].gameObject.transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gamecontroller.ui.UpdateP1Speed(carPlayer1.ForwardVelocity().magnitude, carPlayer1.maxSpeed);
        gamecontroller.ui.UpdateP2Speed(carPlayer2.ForwardVelocity().magnitude, carPlayer2.maxSpeed);
        Car firstCar = gamecontroller.cars[0];
        Car secondCar = gamecontroller.cars[1];
        if (!secondCar.IsVisibleInCamera())
        {
            firstCar.points++;
            gamecontroller.ui.SetLives(carPlayer1.points, carPlayer2.points);
            gamecontroller.RespawnAllCars();
            gamecontroller.ui.UpdateP1Speed(carPlayer1.ForwardVelocity().magnitude, carPlayer1.maxSpeed);
            gamecontroller.ui.UpdateP2Speed(carPlayer2.ForwardVelocity().magnitude, carPlayer2.maxSpeed);
            Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gamecontroller.cars[0].transform.position);
            if (firstCar.points == gameController.pointsLimit)
            {
                gamecontroller.GameOver();
                gamecontroller.ui.SetResultText(firstCar.carName + " Wins");
            }
            else
            {
                StartCoroutine(RoundResult(2, firstCar.carName));
            }
        }
    }

    public IEnumerator RoundResult(int seconds, string player)
    {
        gamecontroller.DeactivateAllCars();
        gamecontroller.ui.SetResultText(player + " Wins Round");
        yield return new WaitForSeconds(seconds);
        StartCoroutine(gamecontroller.CountDown(3));
    }
}