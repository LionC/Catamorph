using UnityEngine;
using System.Collections;

public class DamagingObjectsController : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public float damage = 1f;  //Damage caused by object
	public bool onlyDamageMixerCat = false;  //Whether only MixerCat should take damage on collision
	private CatBehaviour catBehavior;  //Reference to CatBehavior
	private MixerCatController mixerCatController;  //Reference to MixerCatController

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		catBehavior = player.GetComponent<CatBehaviour> ();  //Initialization of CatBehavior
		mixerCatController = player.GetComponent<MixerCatController> ();  //Initialization of MixerCatController
	}

	public void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player") {  //Colliding object must be Cat
			if(!onlyDamageMixerCat)  //Cat takes damage independent of currentAbility
				catBehavior.takeDamage (damage);  //Cat takes damage from collision
			else if (onlyDamageMixerCat && catBehavior.currentAbility.ToString () == "MixerCat" && (mixerCatController.isFlying || mixerCatController.isGliding)) {  //Only MixerCat takes damage when flying against
				catBehavior.takeDamage (damage);  //Cat takes damage from collision
				mixerCatController.crash ();  //Crash on collision
			}
		}
	}
}
