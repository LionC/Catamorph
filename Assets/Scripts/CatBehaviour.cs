using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CatBehaviour : MonoBehaviour {

	public GameObject player;
	public MonoBehaviour currentAbility = null;

	public int lives = 9;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (currentAbility == player.GetComponent<MixerCatController> ())
				return;
			else {
				if(currentAbility != null)
					currentAbility.enabled = false;
				
				currentAbility = player.GetComponent<MixerCatController> ();
				currentAbility.enabled = true;
			}
		}

		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if (currentAbility == player.GetComponent<FreezerCatController> ())
				return;
			else {
				if(currentAbility != null)
					currentAbility.enabled = false;
				
				currentAbility = player.GetComponent<FreezerCatController> ();
				currentAbility.enabled = true;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Hund") {
			lives--;
			if (lives < 0) {
				gameOver ();

			}
		}
	}

	private void gameOver(){	//Alles was nach tod passsiert
		Destroy(player);
	}

}

