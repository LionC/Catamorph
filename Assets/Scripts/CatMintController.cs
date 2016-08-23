using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	public GameObject catMintSmall, catMintWindow;
	private GameObject player;
	private float timeStart;

	void Start () {
		catMintSmall.SetActive (false);
		catMintWindow.SetActive(false);
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate(){
		//position of windowanimation
		if (catMintWindow.activeSelf == true) 
			catMintWindow.transform.position = player.transform.position;

		//stop windowanimation
		if (timeStart + 12 < Time.time)
			catMintWindow.SetActive (false);
	}

	//start animations and the catnip inversion
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);
			catMintSmall.SetActive (true);
			catMintSmall.transform.position = player.transform.position;
			catMintWindow.SetActive (true);
			timeStart = Time.time;

		} 

	}
	//stop animation at the moment cateline is leaving catnip
	private void OnTriggerExit2D (Collider2D other){
		catMintSmall.SetActive (false);
	}
}
