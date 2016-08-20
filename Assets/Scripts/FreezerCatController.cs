﻿using UnityEngine;
using System.Collections;

public class FreezerCatController : MonoBehaviour {

	private GameObject player;
	public Color freezerCatColor = new Color(98, 181, 229);

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = freezerCatColor; 
	}

	public override string ToString() {
		return "FreezerCat";
	}
}