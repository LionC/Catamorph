using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class WaterController : MonoBehaviour {

	public GameObject player;
	public float xScareForce;
	public float yScareForce;
	private bool triggered = false;
	private int direction = -1;
	private Rigidbody2D rigidBody2D;
	private PlatformerCharacter2D platformerCharacter2D;

	// Use this for initialization
	void Start () {
		rigidBody2D = player.GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if (triggered) {
			player.transform.position += new Vector3 (((float) direction) * 0.08f, 0.04f, 0f);
			triggered = false;
		}
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !triggered) {
			if (rigidBody2D.velocity.x <= 0)
				direction = 1;
			triggered = true;
		}
	}

	private void scareOutOfWater(Rigidbody2D rigidBody2D) {
		//rigidBody2D.AddForce (new Vector2(direction * xScareForce, yScareForce));


	}
}
