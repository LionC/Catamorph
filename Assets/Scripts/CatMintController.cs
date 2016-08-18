using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	public bool inverted;
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
		while (other.tag == "Player") {
			player.GetComponent<PlatformerCharacter2D> ().setInversion(true);
			catMintSmall.SetActive (true);
			catMintWindow.SetActive (true);
		}
		inverted = false;
	}
}
