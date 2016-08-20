using UnityEngine;
using System.Collections;

public class DamageWalls : MonoBehaviour {

	private GameObject player;

	public float damageValue = 1f;
	private CatBehaviour catBehavior;
	private MixerCatController mixerCatController;

	void Start () {
		catBehavior = player.GetComponent<CatBehaviour> ();
		mixerCatController = player.GetComponent<MixerCatController> ();
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag=="Player" ) {
			catBehavior.takeDamage (damageValue);
			mixerCatController.crash ();
		}
	}
}
