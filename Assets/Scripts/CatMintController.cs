using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CatMintController : MonoBehaviour {

	public GameObject catMintSmall;  //Small animation
	public GameObject catMintWindow;  //Window-sized animation
	private GameObject player;  //Reference to Cat GameObject
	public float windowAnimationDuration = 12f;  //Duration, at which the window-sized animation is shown
	private float windowAnimationTimeCounter = 0f;  //Time counter in order to manage the duration, at which the window-sized animation is shown

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Ininialization of Cat GameObject
		catMintSmall.SetActive (false);  //Disable small animation by default
		catMintWindow.SetActive(false);  //Disbale window-sized animation by default
	}

	void FixedUpdate(){
		if (catMintWindow.activeSelf) {  //Window-sized animation is enabled
			windowAnimationTimeCounter += Time.fixedDeltaTime;  //Add 0.02 sec per FixedUpdateCycle
			catMintWindow.transform.position = player.transform.position;  //Animation follows Cat's movement

			if(windowAnimationTimeCounter >= windowAnimationDuration)  //Time counter has overtaken the intended duration, at which the window-sized animation is shown
				catMintWindow.SetActive (false);  //Disable window-sized animation
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {  //Triggering object must be Cat
			player.GetComponent<PlatformerCharacter2D> ().setInversion (true);  //Inverse the movement controls
			catMintSmall.SetActive (true);  //Enable small animation
			catMintSmall.transform.position = player.transform.position;  //Animation is located at Cat
			catMintWindow.SetActive (true);  //Enable window-sized animation
		} 
	}

	private void OnTriggerExit2D (Collider2D other) {
		catMintSmall.SetActive (false);  //Disable small animation
	}
}
