using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTriggerBehaviour : MonoBehaviour {

	public string text;
	public GameObject textBox;
	public int readingTime;
	public bool repeatable = false;

	private bool triggered = false;
	private bool disappeared = false;

	private float triggerTime = 0;

	private GameObject box = null;

	void OnTriggerEnter2D(Collider2D other) {
		if (triggered)
			return;

		triggered = true;

		Canvas canvas = FindObjectOfType<Canvas> ();

		box = Instantiate (textBox);
		box.transform.parent = canvas.transform;
		box.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 0, 0);

		box.GetComponentInChildren<Text>().text = text;

		triggerTime = Time.time;
	}

	public void Update() {
		if (triggered && !disappeared && (Time.time - triggerTime) > readingTime) {
			Destroy (box);

			disappeared = !repeatable;
			triggered = !repeatable;
		}
	}
}
