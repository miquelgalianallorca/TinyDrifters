using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {
    protected GameController gamecontroller;
    // Use this for initialization
    public void Activate () {
		
	}
    
    public void Deactivate () {
        this.enabled = false;
        gamecontroller.DestroyAllCars();
        Destroy(this);
	}
}
