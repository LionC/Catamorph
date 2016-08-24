using UnityEngine;
using System.Collections;

public class PowerPlugController : MonoBehaviour {

	public KeyCode key;
	private GameObject player;
	private CatBehaviour catBehavior;
	private bool triggered = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start () {
		catBehavior = player.GetComponent<CatBehaviour> ();
	}

	void Update () {
		if (triggered && catBehavior.currentAbility.ToString () == "MixerCat")
			player.GetComponent<MixerCatController> ().batteryLoad ();
	}

	public void OnTriggerEnter2D() {
		triggered = true;
	}

	public void OnTriggerExit2D() {
		triggered = false;
	}
}
