﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	public GameObject catMintSmall, catMintWindow;
	private GameObject player;

	// Use this for initialization
	void Start () {
		catMintSmall.SetActive (false);
		catMintWindow.SetActive(false);
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (catMintWindow.activeSelf == true) {
			print ("test");
			catMintWindow.transform.position = player.transform.position;
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);
			catMintSmall.SetActive (true);
			catMintSmall.transform.position = transform.position;
			catMintWindow.SetActive (true);

		} 

	}

	private void OnTriggerExit2D (Collider2D other){
		catMintSmall.SetActive (false);
	}
}