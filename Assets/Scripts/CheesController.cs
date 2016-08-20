using UnityEngine;
using System.Collections;

public class CheesController : MonoBehaviour {

	public Vector3 spawnPos;
	private GameObject player;
	public 


	// Use this for initialization
	void Start () {
		hit = false;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		
		if (Vector3.Distance (spawnPos, transform.position) >= 6) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Destroy (gameObject);
		}
		if (other.tag != "Katzenminze" && other.tag != "Maus")
			Destroy (gameObject);
	}
}
