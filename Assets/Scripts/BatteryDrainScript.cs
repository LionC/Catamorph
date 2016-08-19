using UnityEngine;
using System.Collections;

public class BatteryDrainScript: MonoBehaviour {
	private BoxCollider2D Collider;
	private GameObject player;
	private CatBehaviour catBehavior;
	private MixerCatController mixerCatController;
	private bool drainActive;
	private float delay, timeLastDrain;
	public float BatteryDrain = 1f;


	void Start () {
		catBehavior = player.GetComponent<CatBehaviour> ();
		mixerCatController = player.GetComponent<MixerCatController> ();
		Collider = GetComponent<BoxCollider2D>();
		delay = 1f;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate() {
		if (drainActive == true) {
			if (timeLastDrain + delay <= Time.time) {
				mixerCatController.batteryCurrent = (mixerCatController.batteryCurrent - BatteryDrain);
				timeLastDrain = Time.time;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log ("OnTriggerEnter2D");
		if (other.gameObject.tag=="Player") {
			drainActive = true;
		}
	}
	void OnTriggerExit2D(Collider2D other) 
	{
		Debug.Log("OnTriggerExit2D");
		if (other.gameObject.tag=="Player") {
			drainActive = false;
		}
	}
}
