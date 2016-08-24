using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class EndScene : MonoBehaviour {

	public GameObject textBox;
	public int readingTime;

	public string[] dialog;

	public Animator creditsAnimation;

	private bool triggered = false;
	private float triggerTime = 0;
	private GameObject box = null;
	private int dialogIndex = 0;

	void Start() {
		creditsAnimation.enabled = false;

		triggered = true;

		Canvas canvas = FindObjectOfType<Canvas> ();

		GameObject currentTextBox = GameObject.FindGameObjectWithTag ("Text");

		if (currentTextBox != null)
			Destroy (currentTextBox);

		box = Instantiate (textBox);
		box.tag = "Text";
		box.transform.parent = canvas.transform;
		box.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 0, 10f);
		box.transform.SetAsFirstSibling ();

		box.GetComponentInChildren<Text>().text = dialog[0];

		triggerTime = Time.time;
	}

	void OnTriggerEnter2D(Collider2D other) {
		
	}

	public void Update() {
		if (triggered) {
			if((Time.time - triggerTime) > readingTime || CrossPlatformInputManager.GetButtonDown ("Cancel")) {
				if (continueDialog()) {
					triggerTime = Time.time;
					return;
				}

				Destroy (box);

				triggered = false;

				creditsAnimation.enabled = true;
			}
		}
	}
		
	private bool continueDialog() {
		if (dialogIndex == dialog.Length - 1) {
			return false;
		}

		box.GetComponentInChildren<Text> ().text = dialog [++dialogIndex];
		return true;
	}
}
