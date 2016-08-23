using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CheeseController : MonoBehaviour {

	public float damage = 1f;  //Damage caused by cheese
	public float destroyDistance = 7f;  //Distance from spawningPosition, at which the object should be destroyed
	private Vector3 spawningPosition;  //Position of spawing cheese
	private GameObject player;  //Reference to Cat GameObject

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		spawningPosition = transform.position;  //Initialization of spawningPosition
	}

	void FixedUpdate() {
		if (Vector3.Distance (spawningPosition, transform.position) >= destroyDistance)  //Cheese is farther away from spawningPosition than intended
			Destroy (gameObject);  //Destroy cheese
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player") {  //Colliding object must be Cat
			player.GetComponent<CatBehaviour> ().takeDamage (damage);  //Cat takes damage from cheese
			Destroy (gameObject);  //Destroy cheese
		}
	}

}
