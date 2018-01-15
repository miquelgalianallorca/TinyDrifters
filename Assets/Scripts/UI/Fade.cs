using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
	public Outline outline;
	Text text;
	Color color;
	Color outlineColor;
	float alpha;
	float fadeSpeed;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		color = text.color;
		outlineColor = outline.effectColor;
		alpha = 0;
		fadeSpeed = 1f;
	}

	// Update is called once per frame
	void Update () {
		alpha += Time.deltaTime * fadeSpeed;
		color.a = alpha;

		text.color = color;

		outlineColor.a = alpha;

		outline.effectColor = outlineColor;


		if (alpha < 0 || alpha >= 1)
			fadeSpeed *= -1;
	}
}
