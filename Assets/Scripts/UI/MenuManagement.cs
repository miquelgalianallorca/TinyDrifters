using System.Collections;
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
    public GameObject mainMenuPanel;
    public GameObject retryPanel;
    public GameObject pausePanel;
    public GameObject controlsExplanation;

	private GameController gameController;
	private string wantedGameMode;

    // Use this for initialization
    void Start () {
		GameObject gameControllerObj = GameObject.Find ("GameController");
		if (gameControllerObj) {
			gameController = gameControllerObj.GetComponent<GameController> ();
		}
		PlayerPrefs.SetInt ("ShowControlsExplanation", 0);

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

	public void SetGameMode(string gameMode) {
		wantedGameMode = gameMode;
		if (PlayerPrefs.GetInt("ShowControlsExplanation") == 0) {
			controlsExplanation.SetActive (true);
			PlayerPrefs.SetInt ("ShowControlsExplanation", 1);
		} else {
			LoadWantedGameMode ();
		}
	}

	public void LoadWantedGameMode() {
		controlsExplanation.SetActive (false);
		gameController.SetGameMode (wantedGameMode);
		wantedGameMode = "";
	}

    public void ButtonHover(Text buttonText)
    {
        buttonText.fontSize = highlightFontSize;
    }

    public void ButtonNormal(Text buttonText)
    {
        buttonText.fontSize = normalFontSize;
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

    public void ActivateMainMenu()
    {
        gameObject.SetActive(true);
        retryPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ActivateRetryMenu()
    {
        gameObject.SetActive(true);
        mainMenuPanel.SetActive(false);
        retryPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ActivatePauseMenu()
    {
        gameObject.SetActive(true);
        pausePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        retryPanel.SetActive(false);
    }

    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
