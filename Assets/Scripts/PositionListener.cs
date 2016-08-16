using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class PositionListener : MonoBehaviour {
	public GameObject player;
	public KeyCode key;
	private bool anSteckdose = false;

	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void FixedUpdate () {
		positionListener ();
		if (anSteckdose == true) {
			player.GetComponent<CatBehaviour>().batteryLoad ();
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
			if (tag == "Katzenminze") {
				player.GetComponent<PlatformerCharacter2D> ().timeLeft = 10.0f;
				player.GetComponent<PlatformerCharacter2D> ().inverted = true;
			}
		}
	}
}
