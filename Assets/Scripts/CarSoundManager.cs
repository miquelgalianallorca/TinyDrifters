using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundManager : MonoBehaviour {

	public  AudioClip   crashSound;
	public  AudioClip   engineSound;
	private AudioSource audioSource;
	private Car         car;

	public float minVolume = 0.1f;
	public float maxVolume = 0.4f;
	public float fadeTime  = 5.0f;

	void Start () {
		car = GetComponent<Car> ();
		audioSource = GetComponent<AudioSource> ();
		if (audioSource && engineSound) {
			audioSource.volume = minVolume;
			audioSource.time   = Random.value;
			audioSource.Play ();
		}
	}

	// Call from Car when accelerating
	public void SetVolume(float impulse) {
		audioSource.volume = Mathf.Lerp (minVolume, maxVolume, impulse);
	}

	// Call from GameController when deactivating cars
	public void StopEngine() {
		if (audioSource && audioSource.volume > 0)
			StartCoroutine (FadeSound());
	}

	public IEnumerator FadeSound(){
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		audioSource.Stop ();
		audioSource.volume = startVolume;
	}

	// Crashes
	void OnCollisionEnter(Collision collision) {
		if (crashSound && audioSource && collision.gameObject.name != "Floor")
			audioSource.PlayOneShot (crashSound);
	}

}
