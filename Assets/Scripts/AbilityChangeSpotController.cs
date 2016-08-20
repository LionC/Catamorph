using UnityEngine;
using System.Collections;

public class AbilityChangeSpotController : MonoBehaviour {

	private GameObject player;
	public bool rocketIsAvailable;
	public bool freezerIsAvailable;
	public bool burnerIsAvailable;
	public bool laserIsAvailable;
	public bool mixerIsAvailable;
	private bool triggered = false;
	private CatBehaviour catBehavior;

	// Use this for initialization
	void Start () {
		catBehavior = player.GetComponent<CatBehaviour> ();
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if(triggered)
			catBehavior.switchAbility (rocketIsAvailable, freezerIsAvailable, burnerIsAvailable, laserIsAvailable, mixerIsAvailable);
	}

	public void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" && !triggered) {
			triggered = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		triggered = false;
	}
}
