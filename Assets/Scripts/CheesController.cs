using UnityEngine;
using System.Collections;

public class CheesController : MonoBehaviour {

	public bool hit;

	// Use this for initialization
	void Start () {
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag == "Player") {
			hit = true;
		}
		Destroy (gameObject);
	}
}
