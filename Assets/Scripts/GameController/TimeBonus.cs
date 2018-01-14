using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : MonoBehaviour {

    public TimeAttackMode gameMode;

    void OnTriggerEnter(Collider other)
    {
        if (gameMode)
        {
            gameMode.timeLeft += 10;
            Destroy(this.gameObject);
        }
    }
}
