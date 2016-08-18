using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CheesController : MonoBehaviour {

	public Vector3 spawnPos;
	public GameObject player;


	// Use this for initialization
	void Start () {
		spawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		
		if (Vector3.Distance (spawnPos, transform.position) >= 7) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag == "Player") {
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);
		}
		Destroy (gameObject);
	}
}
