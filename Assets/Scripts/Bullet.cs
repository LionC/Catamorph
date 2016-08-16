using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour {

	public int damage = 1;
	public bool isEnemShot = false;
	void OnCollisionenter(Collision col)
	{
			Destroy (col.gameObject);
	}
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
