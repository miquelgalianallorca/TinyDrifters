using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
