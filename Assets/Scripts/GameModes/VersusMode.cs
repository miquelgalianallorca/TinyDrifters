using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusMode : MonoBehaviour
{

    GameController gamecontroller;

    public GameObject player1Car;
    public GameObject player2Car;
    Car carPlayer1;
    Car carPlayer2;

    // Use this for initialization
    void Start()
    {
        gamecontroller = GetComponent<GameController>();
        carPlayer1 = Instantiate(player1Car, gamecontroller.startPoints[0].position, gamecontroller.startPoints[0].rotation).GetComponent<Car>() ;
        carPlayer1.SetGameController(gamecontroller);

        carPlayer2 = Instantiate(player2Car, gamecontroller.startPoints[1].position, gamecontroller.startPoints[0].rotation).GetComponent<Car>();
        carPlayer2.SetGameController(gamecontroller);
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
            if (secondCar.lifes == 0)
            {
                gamecontroller.GameOver();
            }
            else
            {
                gamecontroller.RespawnAll();
            }
        }
        gamecontroller.ui.PrintLives(carPlayer1.lifes, carPlayer2.lifes);
    }
}