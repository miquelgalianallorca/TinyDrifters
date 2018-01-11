using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusMode : MonoBehaviour
{

    GameController gamecontroller;
    
    Car carPlayer1;
    Car carPlayer2;

    // Use this for initialization
    void Start()
    {
        gamecontroller = GetComponent<GameController>();
        carPlayer1 = (Instantiate(Resources.Load("Car Player"), gamecontroller.startPoints[0].position, gamecontroller.startPoints[0].rotation) as GameObject).GetComponent<Car>();
        carPlayer1.SetGameController(gamecontroller);
        
        carPlayer2 = (Instantiate(Resources.Load("Car Player 2"), gamecontroller.startPoints[1].position, gamecontroller.startPoints[1].rotation) as GameObject).GetComponent<Car>();
        carPlayer2.SetGameController(gamecontroller);
        StartCoroutine(gamecontroller.CountDown(3));
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().SetTarget(gamecontroller.cars[0].gameObject.transform);
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (gamecontroller.cars[0].gameObject.transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
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
                StartCoroutine(gamecontroller.CountDown(3));
            }
        }
    }
}