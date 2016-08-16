using UnityEngine;
using System.Collections;

public class RocketDestroyable : MonoBehaviour {
	void OnCollisionenter(Collision col)
	{
		if (col.gameObject.tag == "Laser")
			Destroy (col.gameObject);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

}