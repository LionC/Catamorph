using UnityEngine;
using System.Collections;

public class CheesController : MonoBehaviour {

	public bool hit;
	public Vector3 spawnPos;


	// Use this for initialization
	void Start () {
		hit = false;
		spawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (Vector3.Distance (spawnPos, transform.position) >= 5) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag == "Player") {
			hit = true;
		}
		Destroy (gameObject);
	}
}
