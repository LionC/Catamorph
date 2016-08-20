using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class PositionListener : MonoBehaviour {
	private GameObject player;
	public KeyCode key;
	private bool anSteckdose = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate () {
		positionListener ();
		if (anSteckdose == true) {
			//player.GetComponent<CatBehaviour>().batteryLoad ();
			anSteckdose = false;
		}
	}

	private void positionListener(){
		if (Mathf.Round (transform.position.x) == Mathf.Round (player.transform.position.x)&&Mathf.Round (transform.position.y) == Mathf.Round (player.transform.position.y)) {
			if (Input.GetKeyDown (key)) {
				if (tag == "Steckdose") {
					anSteckdose = true;
				}
			}
		}
	}
}
