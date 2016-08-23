using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
	public float fadingTimeInSeconds = 2;  //Duration of fading animation

	private bool fading = false;  //Fading in progress
	private bool fadingIn = false;  //FadingIn in progress
	private SpriteRenderer sprite;  //Reference to SpriteRenderer
	private float fadingStep = 0;  //Time counter in order to manage animation's duration

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();  //Initialization of SpriteRenderer
	}

	void FixedUpdate () {
		if (fading) {  //Fading in progress
			if (fadingStep >= fadingTimeInSeconds * 50) {  //Fading animation already longer shown than intended
				fading = false;  //Fading not in progress any more
				return;  //Ignore following fading procedure
			}

			float progress = fadingStep / (fadingTimeInSeconds * 50);  //Calculate progress

			sprite.color = new Color(
				sprite.color.r,
				sprite.color.g,
				sprite.color.b,
				fadingIn ? progress :  (1 - progress)
			);  //Change sprite's alpha channel depending on fading progress

			fadingStep++;  //Increase time counter
		}
	}

	public void fadeOut() {
		fadingIn = false;  //No fadingIn
		fadingStep = 0;  //Reset time counter
		fading = true;  //Apply fading
	}

	public void fadeIn() {
		fadingIn = true;  //Apply fadingIn
		fadingStep = 0;  //Reset time counter
		fading = true;  //Apply fading
	}
}
