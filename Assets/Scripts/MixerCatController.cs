using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class MixerCatController : MonoBehaviour {

	public GameObject player;
	public int batteryMax = 100;
	public double batteryCurrent = 0;
	public float flyForce = 50f;
	public float glideForce = -1f;
	public float glideVelocityDelay = -2.5f; //Lower value => faster glide-down
	public Color mixerCatColor = new Color(255, 170, 77);

	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	private bool isFlying = false;
	private bool isGliding = false;
	private bool glideForceAddedOnce = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F)) {
			isFlying = true;
		}
		else if (!Input.GetKey (KeyCode.F) && isFlying && !platformerCharacter2D.m_Grounded) {
			isFlying = false;
			isGliding = true;
		} 
		else if (isGliding && platformerCharacter2D.m_Grounded) {
			isGliding = false;
			resetGravity ();
		}
	}

	void FixedUpdate() {
		if (isFlying) {
			resetGravity ();
			rigidBody.AddForce(new Vector2(0f, flyForce));
		}

		if (isGliding && rigidBody.velocity.y <= glideVelocityDelay && !glideForceAddedOnce) {
			rigidBody.gravityScale = 0;
			rigidBody.AddForce (new Vector2 (0f, glideForce));
			glideForceAddedOnce = true;
		}
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = mixerCatColor; 
	}

	private void resetGravity() {
		rigidBody.gravityScale = 3;
		glideForceAddedOnce = false;
	}

	public void batteryLoad () {
		batteryCurrent = batteryMax;
	}
		
	public override string ToString() {
		return "MixerCat";
	}
}
