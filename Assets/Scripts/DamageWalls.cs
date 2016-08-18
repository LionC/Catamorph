using UnityEngine;
using System.Collections;

public class DamageWalls : MonoBehaviour {

	public GameObject player;

	public float damageValue = 1.5f;
	private CatBehaviour catBehavior;
	private MixerCatController mixerCatController;

	void Start () {
		catBehavior = player.GetComponent<CatBehaviour> ();
		mixerCatController = player.GetComponent<MixerCatController> ();
	}



	public void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player" && catBehavior.currentAbility != null && catBehavior.currentAbility.ToString () == "MixerCat" && (mixerCatController.isFlying || mixerCatController.isGliding)) {
			catBehavior.takeDamage (damageValue);
			mixerCatController.crash ();
		}
	}
}
