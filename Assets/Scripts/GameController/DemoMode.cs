using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : GameMode
{
    public override void Activate()
    {
        //Generate CPU cars
        for (int i = 0; i < gameController.startPoints.Count; i++)
        {
            Car car = Instantiate(gameController.cpuPrefab, gameController.startPoints[i].position, gameController.startPoints[i].rotation).GetComponent<Car>() ;

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

        //Init camera
        //Camera.main.gameObject.GetComponent<CameraFollow>().SetCameraPosition(gameController.cars[0].transform.position);

        //Init UI
        menuUI.ActivateMainMenu();
        gameUI.ActivateDemoUI();

        //enabled = true;
    }

    void LateUpdate()
    {
        //Camara follows first car
        Camera.main.gameObject.GetComponent<CameraFollow>().target = gameController.cars[0].transform;
    }
}
