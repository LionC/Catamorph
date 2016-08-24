using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class WaterController : MonoBehaviour {

	private GameObject player;
	public Vector2 spawnPoint;
	private bool triggered = false;
	private Fader fader;
	private Rigidbody2D rigidBody2D;
	private CatBehaviour catBehavior;
	private PlatformerCharacter2D platformerCharacter2D;

	void Start () {
		fader = player.GetComponent<Fader> ();
		rigidBody2D = player.GetComponent<Rigidbody2D> ();
		catBehavior = player.GetComponent<CatBehaviour> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !triggered && (catBehavior.currentAbility == null || catBehavior.currentAbility.ToString() != "FreezerCat")) {
			triggered = true;
			scareOutOfWater ();
		}
	}

	private void scareOutOfWater() {
		fader.fadeIn ();
		print ("scared");
		player.transform.position = new Vector3 (transform.position.x + spawnPoint.x, transform.position.y + spawnPoint.y, 0.0f);
		triggered = false;
	}
}
