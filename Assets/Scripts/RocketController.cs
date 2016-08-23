using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	public float damage = 1.0f;  //Damage caused by Rocket
	public float livingTime = 20f;  //Time in sec, at which the rocket is destroyed at the latest

		
	void Start () {
		Destroy (gameObject, 20);  //Destroy rocket at the latest
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		Destroy (gameObject);  //Destroy rocket on collision
	}
}
