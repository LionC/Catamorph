using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float damage = 1.0f;
	public bool isEnemyShot = false;

	void OnCollisionEnter2D(Collision2D other) {

	}
		
	void Start () {
		Destroy (gameObject, 20);
	}
}
