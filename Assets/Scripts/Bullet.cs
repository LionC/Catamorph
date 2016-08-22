using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;

	void OnCollisionEnter2D(Collision2D col) {
			Destroy (gameObject);
	}
		
	void Start () {
		Destroy (gameObject, 20);
	}
}
