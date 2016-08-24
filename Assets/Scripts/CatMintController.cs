using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	private GameObject player, mainCamera;
	private bool hit = false;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	//start animations and the catnip inversion
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			hit = true;
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);
			GetComponent<Animator> ().SetBool ("Hit", true);
		} 
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			hit = false;
			GetComponent<Animator> ().SetBool ("Hit", false);
		}

	}

	public bool hitCatMint(){
		return hit;
	}


}
