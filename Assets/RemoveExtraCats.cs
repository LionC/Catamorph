using UnityEngine;
using System.Collections;

public class RemoveExtraCats : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Rocket") {  //Collision by rocket
			var cat = GameObject.FindGameObjectWithTag("Player").GetComponent<CatBehaviour>();
			cat.switchToNormalCat ();

		}
	}
}
