﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagement : MonoBehaviour {
	public GameObject startButton;
	public GameObject p1Button;
	public GameObject p2Button;
	public GameObject timeButton;
	public Text p1Text;
	public Text p2Text;
	public Text timeText;
	private int normalFontSize;
	private int highlightFontSize;

	// Use this for initialization
	void Start () {
		normalFontSize = 35;
		highlightFontSize = 42;
	}
	
	public void StartButtonClick(){
		p1Button.SetActive (true);
		p2Button.SetActive (true);
		timeButton.SetActive (true);

		startButton.SetActive (false);
		Debug.Log ("button clicked");
	}

	public void P1ButtonClick(){
		//Load 1p vs com scene
	}

	public void P1ButtonHover(){
		p1Text.fontSize = highlightFontSize;
	}

	public void P1ButtonNormal(){
		p1Text.fontSize = normalFontSize;
	}

	public void P2ButtonClick(){
		//Load 1p vs 2p scene
	}

	public void P2ButtonHover(){
		p2Text.fontSize = highlightFontSize;
	}

	public void P2ButtonNormal(){
		p2Text.fontSize = normalFontSize;
	}

	public void TimeButtonClick(){
		//Load TimeAttack scene
	}

	public void TimeButtonHover(){
		timeText.fontSize = highlightFontSize;
	}

	public void TimeButtonNormal(){
		timeText.fontSize = normalFontSize;
	}
}