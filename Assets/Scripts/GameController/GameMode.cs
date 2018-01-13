using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour {

    protected GameController gameController;
    protected UIManagement gameUI;
    protected MenuManagement menuUI;


    private void Awake()
    {
        this.enabled = false;
        gameController = GetComponent<GameController>();
        gameUI = gameController.gameUI;
        menuUI = gameController.menuUI;
    }

    public abstract void Activate();
}
