﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class WaterController : MonoBehaviour {

	private GameObject player;
	public Vector2 spawnPoint;
	private bool triggered = false;
	private int direction = -1;
	private Rigidbody2D rigidBody2D;
	private CatBehaviour catBehavior;
	private PlatformerCharacter2D platformerCharacter2D;

	// Use this for initialization
	void Start () {
		rigidBody2D = player.GetComponent<Rigidbody2D> ();
		catBehavior = player.GetComponent<CatBehaviour> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !triggered && (catBehavior.currentAbility == null || catBehavior.currentAbility.ToString() != "FreezerCat")) {
			if (rigidBody2D.velocity.x <= 0)
				direction = 1;
			triggered = true;
			scareOutOfWater ();
		}
	}

	private void scareOutOfWater() {
		//catBehavior.takeDamage ();
		rigidBody2D.AddForce (new Vector2(direction * xScareForce, yScareForce));
		triggered = false;
	}
}
