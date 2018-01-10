using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour {

    public Text p1Lives;
    public Text p2Lives;
    public void PrintLives(int p1, int p2)
    {
        p1Lives.text = p1.ToString();
        p2Lives.text = p2.ToString();
    }
}
