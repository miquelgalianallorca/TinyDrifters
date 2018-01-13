using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : GameMode
{
    UIManagement ui;
    Car carPlayer;


    public override void Activate()
    {
        //Init cars
        gameController.DestroyAllCars();
        for (int i = 0; i < gameController.startPoints.Count; i++)
        {
            GameObject carCPU = Instantiate(gameController.cpuPrefab, gameController.startPoints[i].position, gameController.startPoints[i].rotation) as GameObject;
            carCPU.GetComponent<Car>().SetGameController(gameController);
            carCPU.GetComponent<Car>().Init(gameController.cpuTypes[Random.Range(0, gameController.cpuTypes.Length)]);
            carCPU.GetComponent<Car>().icon = gameController.carIcons[i];
            // Car color
            if (gameController.carColors[i])
            {
                Renderer carRenderer = carCPU.GetComponentInChildren<Renderer>();
                carRenderer.material = gameController.carColors[i];
            }
        }

        //Init camera
        Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gameController.cars[0].transform.position);

        //Init UI
        gameController.menuUI.ActivateMainMenu();
        gameController.ui.ActivateDemoUI();

        gameController.finishedCars = 0;
        this.enabled = true;
    }

    // Use this for initialization
    void Start()
    {
        //gameController = GetComponent<GameController>();
        //ui = gameController.ui;
        
        
        
        //StartCoroutine(gamecontroller.CountDown(3));
        //gameController.ui.gameObject.SetActive(false);
    }

    private void Update()
    {
        Camera.main.gameObject.GetComponent<CameraFollow>().target = (gameController.cars[0].transform);
    }
}
