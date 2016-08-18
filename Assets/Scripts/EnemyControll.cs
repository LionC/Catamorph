﻿using UnityEngine;
using System.Collections;

public class EnemyControll : MonoBehaviour {

	public GameObject player;
	public bool hindernis, hit;
	public Vector3 posEnemy, posPlayer;
	public float abs, timeLeftHit, timeLeftJump, timeBug, jumpTry;
	public float damageValue = 1f;

	// Use this for initialization
	void Start () {
		timeLeftHit = 5;	//5 Sekunden zum nächsten schlag
		timeLeftJump = 0.2f; // Zeit bis zum nächsten Sprung
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		if (timeLeftHit > 0) {
			timeLeftHit -= Time.deltaTime;
		}

		if (timeLeftJump > 0) {
			timeLeftJump -= Time.deltaTime;
		}

		posEnemy = transform.position;
		posPlayer = player.transform.position;

		abs = Vector3.Distance (posEnemy, posPlayer);

		if (abs < 3) {
			transform.position += new Vector3 (((posPlayer.x - posEnemy.x) / (10*Random.Range(0,2))), 0.0f, 0.0f);
				if ((posPlayer.y - posEnemy.y > 0.5f || hindernis == true) && timeLeftJump <= 0) {
					jumpTry += 0.2f;
					transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
					timeLeftJump = 0.2f;
				}
				if (posEnemy.y - posPlayer.y > 0) {
					transform.position += new Vector3 (player.GetComponent<Rigidbody2D> ().velocity.x, jumpTry, 0.0f);
				}
				hindernis = false;
			} 
		}


		private void OnCollisionEnter2D(Collision2D coll){
		if ((tag == "Dog") || (tag == "Human")){
			if (coll.collider.tag == "Player" && timeLeftHit <= 0) {
				transform.position += new Vector3 (coll.collider.GetComponent<Rigidbody2D>().velocity.x*(-1),0.5f,0.0f); //naach hinten fliegen
				timeLeftHit = 5.0f; //Timer Hit reset
				hit = true;	//Spieler wurde getroffen
				player.GetComponent<CatBehaviour>().takeDamage(damageValue);
			}

			if (coll.collider.tag != "Player" && coll.collider.tag != "Ground") {
				hindernis = true;
			}
		}
	}
}
