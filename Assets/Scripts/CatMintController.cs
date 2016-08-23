﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	public Animator catMintSmall, catMintWindow;
	private GameObject player;

	void Start () {
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate(){
		//position of windowanimation
		if (catMintWindow.isActiveAndEnabled) 
			catMintWindow.transform.position = player.transform.position;
	}

	//start animations and the catnip inversion
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);
			GetComponent<Animator> ().SetBool ("Hit",true);
			catMintSmall.transform.position = player.transform.position;
		} 

	}
}
