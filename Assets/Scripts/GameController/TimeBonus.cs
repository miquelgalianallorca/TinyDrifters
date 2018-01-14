using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : MonoBehaviour {

    public TimeAttackMode gameMode;

    void OnTriggerEnter(Collider other)
    {
        if (gameMode)
        {
            gameMode.timeLeft += 15;
            Destroy(this.gameObject);
        }
    }
}
