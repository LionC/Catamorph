using UnityEngine;
using System.Collections;

public class LaserDestroyable: MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
		if (gameObject.tag == "Rocket")
			Destroy (gameObject);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		}

}