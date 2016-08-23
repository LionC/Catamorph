using UnityEngine;
using System.Collections;

public class AbilityChangeSpotController : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public bool rocketIsAvailable;  //Enables switch to RocketCat
	public bool freezerIsAvailable;  //Enables switch to FreezerCat
	public bool burnerIsAvailable;  //Enables switch to BurnerCat
	public bool mixerIsAvailable;  //Enables switch to MixerCat
	private bool triggered = false;  //Player triggered AbilitySpot by moving through
	private CatBehaviour catBehavior;  //Reference to CatBehavior


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initalization of Cat GameObject
		catBehavior = player.GetComponent<CatBehaviour> ();  //Initialization of CatBehavior
	}
		
	void Update () {
		if(triggered)  //AbilitySpot is triggered
			catBehavior.switchAbility (rocketIsAvailable, freezerIsAvailable, burnerIsAvailable, mixerIsAvailable);  //Enable ability switch
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")  //Triggering object must be Cat
			triggered = true;  //AbilitySpot is triggered
	}

	public void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {  //Triggering object must be Cat
			triggered = false;  //AbilitySpot isn't triggered any more after leaving it
		}
	}
}
