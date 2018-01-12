﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusMode : GameMode
{

    Car carPlayer1;
    Car carPlayer2;

    // Use this for initialization
    void Start()
    {
        gamecontroller = GetComponent<GameController>();
        carPlayer1 = (Instantiate(gamecontroller.player1Prefab, gamecontroller.startPoints[0].position, gamecontroller.startPoints[0].rotation) as GameObject).GetComponent<Car>();
        carPlayer1.SetGameController(gamecontroller);

        carPlayer2 = (Instantiate(gamecontroller.player2Prefab, gamecontroller.startPoints[1].position, gamecontroller.startPoints[1].rotation) as GameObject).GetComponent<Car>();
        carPlayer2.SetGameController(gamecontroller);
        Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gamecontroller.cars[0].transform.position);
        StartCoroutine(gamecontroller.CountDown(3));
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (gamecontroller.cars[0].gameObject.transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gamecontroller.ui.SetFirstPosition(gamecontroller.cars[0].icon);
        gamecontroller.ui.SetSecondPosition(gamecontroller.cars[1].icon);

        Car secondCar = gamecontroller.cars[1];
        if (!secondCar.IsVisibleInCamera())
        {
            secondCar.lifes--;
            gamecontroller.RespawnAllCars();
            Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gamecontroller.cars[0].transform.position);
            if (secondCar.lifes == 0)
            {
                gamecontroller.GameOver();
            }
            else
            {
                StartCoroutine(RoundResult(1, gamecontroller.cars[0].carName));
            }
        }
    }

    public IEnumerator RoundResult(int seconds, string player)
    {
        gamecontroller.DeactivateAllCars();
        gamecontroller.ui.versusPanel.SetActive(true);
        gamecontroller.ui.SetRoundResultText(player + " wins");
        yield return new WaitForSeconds(seconds);
        gamecontroller.ui.versusPanel.SetActive(false);
        StartCoroutine(gamecontroller.CountDown(3));
    }
}