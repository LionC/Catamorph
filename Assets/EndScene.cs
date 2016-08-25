using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

	public GameObject textBox;
	public int readingTime;

	public string[] dialog;

	public GameObject creditsMovie;

	private bool triggered = false;
	private float triggerTime = 0;
	private GameObject box = null;
	private int dialogIndex = 0;
	private bool plays = false;
	private bool videoOver = false;
	private float videoOvertime = 0;

	void Start() {
		creditsMovie.SetActive(false);

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

				creditsMovie.SetActive (true);
				((MovieTexture)creditsMovie.GetComponent<MeshRenderer> ().material.mainTexture).Play ();
				plays = true;
			}
		}

		if (plays) {
			if (!((MovieTexture)creditsMovie.GetComponent<MeshRenderer> ().material.mainTexture).isPlaying) {
				videoOver = true;
				plays = false;
				videoOvertime = Time.time;
			}
		}

		if (videoOver && (Time.time - videoOvertime) > 3) {
			SceneManager.LoadScene ("Main Menu");
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
