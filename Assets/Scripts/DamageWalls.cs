using UnityEngine;
using System.Collections;

public class DamageWalls : MonoBehaviour {

	public GameObject player;

	public float invisibleTimeAfterHitInitialValue = 3f;
	public float invisibleTimeAfterHit = 0f;
	private CatBehaviour catBehavior;
	private MixerCatController mixerCatController;

	void Start () {
		catBehavior = player.GetComponent<CatBehaviour> ();
		mixerCatController = player.GetComponent<MixerCatController> ();
	}

	void FixedUpdate() {
		if (invisibleTimeAfterHit > 0)
			invisibleTimeAfterHit -= Time.deltaTime;
	}

	public void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player" && catBehavior.currentAbility != null && catBehavior.currentAbility.ToString () == "MixerCat" && (mixerCatController.isFlying || mixerCatController.isGliding)) {
			catBehavior.takeDamage ();
			mixerCatController.crash ();
			invisibleTimeAfterHit = invisibleTimeAfterHitInitialValue;
		}
	}
}
