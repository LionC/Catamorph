using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class MixerCatController : MonoBehaviour {

	private GameObject player;
	public float batteryMax = 100;
	public float batteryCurrent = 0;
	public float batteryDrain = 5f;
	public float flyForce = 35f;
	public float glideForce = -1f;
	public float glideVelocityDelay = -2.5f; //Lower value => faster glide-down
	public Color mixerCatColor = new Color(255, 170, 77);

	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	public bool isFlying = false;
	public bool isGliding = false;
	private bool glideForceAddedOnce = false;
	private bool crashed = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!crashed) {
			if (Input.GetKey (KeyCode.F)) {
				isFlying = true;
			} else if (!Input.GetKey (KeyCode.F) && isFlying && !platformerCharacter2D.m_Grounded) {
				isFlying = false;
				isGliding = true;
			} else if (isGliding && platformerCharacter2D.m_Grounded) {
				isGliding = false;
				resetGravity ();
			}
		} else {
			if (platformerCharacter2D.m_Grounded)
				crashed = false;
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

	public void crash() {
		crashed = true;
		isFlying = false;
		isGliding = false;
		resetGravity ();
	}
		
	public override string ToString() {
		return "MixerCat";
	}
}
