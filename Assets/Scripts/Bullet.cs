using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;

	void OnCollisionenter(Collision col) {
			Destroy (col.gameObject);
	}
		
	void Start () {
		Destroy (gameObject, 20);
	}

	void Update () {
	
	}
}
