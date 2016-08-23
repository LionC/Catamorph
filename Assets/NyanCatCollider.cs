using UnityEngine;
using System.Collections;

public class NyanCatCollider : MonoBehaviour {



	public NyanCat nyanCat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		nyanCat.play = true;
	}
}
