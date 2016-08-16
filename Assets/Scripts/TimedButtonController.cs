using UnityEngine;
using System.Collections;

public class TimedButtonController : MonoBehaviour {

	private bool pressed = false;
	private bool triggered = false;
	private Transform transform;

	public GameObject objectToDestroy;
	public GameObject afterObjectDestructionAnim;
	public float timeAfterTrigger = 0f;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if (pressed) {
			timeAfterTrigger -= Time.fixedDeltaTime;

			if (timeAfterTrigger <= 0 && !triggered) {
				triggered = true;
				triggerReaction ();
			}
		}
	}

	public void OnTriggerEnter2D() {
		if (pressed == true)
			return;

		pressed = true;
		transform.localScale += new Vector3 (0, -0.5f, 0);
		transform.localPosition += new Vector3 (0, -0.12f, 0);
	}

	public void triggerReaction() {
		//Code for actions after triggering the button

		Destroy (objectToDestroy);
		afterObjectDestructionAnim.SetActive(true);
	}
}
