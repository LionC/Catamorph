using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

public class TextTriggerBehaviour : MonoBehaviour {

	public string text;
	public GameObject textBox;
	public int readingTime;
	public bool repeatable = false;
	public bool freezesCharacter = false;

	public string[] dialog;

	private bool triggered = false;
	private float triggerTime = 0;
	private GameObject box = null;
	private Platformer2DUserControl playerControl = null;
	private int dialogIndex = 0;

	void OnTriggerEnter2D(Collider2D other) {
		if (triggered || other.gameObject.tag != "Player")
			return;

		triggered = true;

		Canvas canvas = FindObjectOfType<Canvas> ();

		GameObject currentTextBox = GameObject.FindGameObjectWithTag ("Text");

		if (currentTextBox != null)
			Destroy (currentTextBox);

		box = Instantiate (textBox);
		box.tag = "Text";
		box.transform.parent = canvas.transform;
		box.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 0, 0);

		box.GetComponentInChildren<Text>().text = isDialog() ? dialog[0] : text;

		if (shouldFreeze()) {
			playerControl = other.gameObject.GetComponent<Platformer2DUserControl> ();
			playerControl.enabled = false;
		}

		triggerTime = Time.time;
	}

	public void Update() {
		if (triggered) {
			if((Time.time - triggerTime) > readingTime || (CrossPlatformInputManager.GetButtonDown ("Cancel") && !(shouldFreeze() && !isDialog()))) {
				if (isDialog () && continueDialog()) {
					triggerTime = Time.time;
					return;
				}

				Destroy (box);

				triggered = !repeatable;

				if (shouldFreeze())
					playerControl.enabled = true;
			}
		}
	}

	private bool isDialog() {
		return dialog.Length > 0;
	}

	private bool continueDialog() {
		if (dialogIndex == dialog.Length - 1) {
			return false;
		}

		box.GetComponentInChildren<Text> ().text = dialog [++dialogIndex];
		return true;
	}

	private bool shouldFreeze() {
		return freezesCharacter || isDialog ();
	}
}
