using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : GameMode
{

    UIManagement ui;
    Car carPlayer;

    // Use this for initialization
    void Start()
    {
        gamecontroller = GetComponent<GameController>();
        ui = gamecontroller.ui;
        for (int i = 0; i < gamecontroller.startPoints.Count; i++)
        {
            GameObject carCPU = Instantiate(gamecontroller.cpuPrefab, gamecontroller.startPoints[i].position, gamecontroller.startPoints[i].rotation) as GameObject;
            carCPU.GetComponent<Car>().SetGameController(gamecontroller);
            carCPU.GetComponent<Car>().Init(gamecontroller.cpuTypes[Random.Range(0, gamecontroller.cpuTypes.Length)]);
            carCPU.GetComponent<Car>().icon = gamecontroller.carIcons[i];
            // Car color
            if (gamecontroller.carColors [i]) {
				Renderer carRenderer = carCPU.GetComponentInChildren<Renderer> ();
				carRenderer.material = gamecontroller.carColors [i];
			}
        }
        Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gamecontroller.cars[0].transform.position);
        gamecontroller.finishedCars = 0;
        //StartCoroutine(gamecontroller.CountDown(3));
        gamecontroller.ui.gameObject.SetActive(false);
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (gamecontroller.cars[0].transform);
        
    }
}
