using UnityEngine;
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

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag == "Player") {
			Destroy (gameObject);
		}
	}

}
