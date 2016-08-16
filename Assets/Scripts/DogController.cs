using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {
	public GameObject enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (tag == "Player") {
			enemy.GetComponent<CatBehaviour> ().lives--;
		}
	}
}
