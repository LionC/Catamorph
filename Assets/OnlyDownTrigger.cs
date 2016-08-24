using UnityEngine;
using System.Collections;

public class OnlyDownTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			var vel = other.GetComponent<Rigidbody2D> ().velocity;

			if (vel.y > 0) {
				vel.y = -vel.y;
				other.GetComponent<Rigidbody2D> ().velocity = vel;
			}
		}
	}
}
