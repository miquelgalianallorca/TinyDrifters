using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour {
    protected GameController gameController;
    protected GameController gamecontroller;

    private void Awake()
    {
        this.enabled = false;
        gameController = GetComponent<GameController>();
        gamecontroller = gameController;
    }


    public abstract void Activate();
    
    public void Deactivate () {
        this.enabled = false;
        gameController.DestroyAllCars();
        gameController.menuUI.ActivateRetryMenu();
	}
}
