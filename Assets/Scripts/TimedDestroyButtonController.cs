using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class TimedDestroyButtonController : MonoBehaviour {

	private bool pressed = false;
	private bool triggered = false;
	new private Transform transform;
	private PlatformerCharacter2D platformerCharacter2D;

	public float timeAfterTriggerInitialValue = 0f;
	public float timeUntilRebuildInitialValue = 0f;

	public GameObject player;
	public GameObject reactionObject;
	public GameObject reactionObjectAnim;
	public float timeAfterTrigger = 0f;
	public float timeUntilRebuild = 0f;

	// Use this for initialization
	void Start () {
		transform = gameObject.GetComponent<Transform> ();
		platformerCharacter2D = player.GetComponent<PlatformerCharacter2D> ();
		timeAfterTrigger = timeAfterTriggerInitialValue;
		timeUntilRebuild = timeUntilRebuildInitialValue;
	}

	void FixedUpdate() {
		if (pressed) {
			timeAfterTrigger -= Time.deltaTime;

			if (timeAfterTrigger <= 0 && !triggered) {
				triggered = true;

				if (timeUntilRebuild <= 0)
					Destroy (reactionObject);
				else
					reactionObject.SetActive (true);
				
				reactionObjectAnim.SetActive(true);
			}
		}

		if (timeUntilRebuild > 0 && triggered) {
			timeUntilRebuild -= Time.fixedDeltaTime;

			if (timeUntilRebuild <= 0) {
				reactionObject.SetActive (true);
				reactionObjectAnim.SetActive(false);
				pressed = false;
				triggered = false;
				timeAfterTrigger = timeAfterTriggerInitialValue;
				timeUntilRebuild = timeUntilRebuildInitialValue;
				transform.localScale += new Vector3 (0, 0.5f, 0);
				transform.localPosition += new Vector3 (0, 0.12f, 0);
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
}
