using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	public GameObject catMintSmall, catMintWindow, player;

	// Use this for initialization
	void Start () {
		catMintSmall.SetActive (false);
		catMintWindow.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);
			catMintSmall.SetActive (true);
			catMintWindow.SetActive (true);
		} 
	}

	private void OnTriggerExit2D (Collider2D other){
		catMintSmall.SetActive (false);
		catMintWindow.SetActive (false);
	}
}
