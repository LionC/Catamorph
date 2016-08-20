using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
	public int fadingTime = 90;

	private bool fading = false;
	private bool fadingIn = false;
	private SpriteRenderer sprite;
	private int fadingStep = 0;

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate () {
		if (fading) {
			if (fadingStep > fadingTime) {
				fading = false;
				return;
			}

			float progress = (float)fadingStep / (float)fadingTime;

			sprite.color = new Color(
				sprite.color.r,
				sprite.color.g,
				sprite.color.b,
				fadingIn ? progress :  (1 - progress)
			);

			fadingStep++;
		}
	}

	public void fadeOut() {
		fadingIn = false;
		fadingStep = 0;
		fading = true;
	}

	public void fadeIn() {
		fadingIn = true;
		fadingStep = 0;
		fading = true;
	}
}
