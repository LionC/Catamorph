﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CheesController : MonoBehaviour {

	public Vector3 spawnPos;
	private GameObject player;

	void Start () {
		spawnPos = transform.position;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate() {
		
		if (Vector3.Distance (spawnPos, transform.position) >= 7)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		//on contact with player
		if (other.tag == "Player") {
			player.GetComponent<CatBehaviour> ().lives--;
			Destroy (gameObject);
		} else {
			if (other.tag !="Katzenminze"){
				Destroy (gameObject);
			}
		}

	}

}
