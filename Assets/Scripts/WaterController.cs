using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class WaterController : MonoBehaviour {

	public GameObject player;
	public Vector2 spawnPoint;
	private bool triggered = false;
	private Fader fader;
	private Rigidbody2D rigidBody2D;
	private CatBehaviour catBehavior;
	private PlatformerCharacter2D platformerCharacter2D;

	// Use this for initialization
	void Start () {
		fader = player.GetComponent<Fader> ();
		rigidBody2D = player.GetComponent<Rigidbody2D> ();
		catBehavior = player.GetComponent<CatBehaviour> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !triggered && (catBehavior.currentAbility == null || catBehavior.currentAbility.ToString() != "FreezerCat")) {
			triggered = true;
			scareOutOfWater ();
		}
	}

	private void scareOutOfWater() {
		fader.fadeIn ();
		player.transform.position = new Vector3 (spawnPoint.x, spawnPoint.y, 0f);
		triggered = false;
	}
}
