using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CheeseController : MonoBehaviour {

	private Vector3 spawnPos;
	private GameObject player;
	private float startTime;
	private bool hit;

	void Start () {
		spawnPos = transform.position;
		GetComponent<Animator> ().SetBool ("Hit",false);
		hit = false;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate() {
		
		if (Vector3.Distance (spawnPos, transform.position) >= 7)
			Destroy (gameObject);
		if (hit == true && startTime + 1 > Time.time) {
			GetComponent<Animator> ().SetBool ("Hit", true);
		} else {
			if (hit == true)
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		//on contact with player
		if (other.collider.tag == "Player") {
			other.collider.GetComponent<CatBehaviour> ().takeDamage (1.0f);
			print ("Hit");
			hit = true;
			startTime = Time.time;




		}

	}

}
