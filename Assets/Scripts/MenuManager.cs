using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	bool buttonPressed = false;
	public string sceneName = "testGonzalo";

	void Awake() {
		//Don't destroy MenuManager when changing scene
		DontDestroyOnLoad (gameObject);
	}

	// On button press
	public void StartGame() {
		if (!buttonPressed) {
			StartCoroutine("LoadSceneAsync");
			buttonPressed = true;
		}
	}

	IEnumerator LoadSceneAsync() {
		Debug.Log ("Starting to load scene");
		// Load scene asyncronously
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		// Wait until loading is done
		while (!asyncLoad.isDone) {
			yield return null;
		}
		Debug.Log ("Loading scene completed.");
		buttonPressed = false;
	}

}
