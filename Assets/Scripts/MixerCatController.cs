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
	public Sprite mixer;
    public AudioClip mixerLoop;

	private PlatformerCharacter2D platformerCharacter2D;
	private GameObject kitchenItem;
	private Rigidbody2D rigidBody;
	public bool isFlying = false;
	public bool isGliding = false;
	private bool glideForceAddedOnce = false;
	private bool crashed = false;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
		batteryCurrent = batteryMax;	//Änderung
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		kitchenItem = player.transform.Find ("KitchenItem").gameObject;
	}
	
	void Update () {
		if (batteryCurrent < 1)
			crash ();

		if (!crashed) {
            if (Input.GetKey (KeyCode.F) && batteryCurrent > 0) {
				isFlying = true;
			} else if (!Input.GetKey (KeyCode.F) && isFlying && !platformerCharacter2D.isGrounded() && batteryCurrent > 0) {
				isFlying = false;
				isGliding = true;
			} else if (isGliding && platformerCharacter2D.isGrounded()) {
				isGliding = false;
				resetGravity ();
			}

            // start mixer loop if player is flying or gliding and music isn't playing already
            if ((isFlying || isGliding) && !platformerCharacter2D.catEffectAudioSource.isPlaying)
                platformerCharacter2D.catEffectAudioSource.UnPause();
        } else {
			if (platformerCharacter2D.isGrounded())
				crashed = false;
		}
	}

	void FixedUpdate() {
		if (isFlying) {
			batteryCurrent -= batteryDrain / 50;	//Änderung
			resetGravity ();
			rigidBody.AddForce(new Vector2(0f, flyForce));
		}

		if (isGliding) {
			batteryCurrent -= batteryDrain / 500;
					//Änderung
			if(rigidBody.velocity.y <= glideVelocityDelay && !glideForceAddedOnce) {
				rigidBody.gravityScale = 0;
				rigidBody.AddForce (new Vector2 (0f, glideForce));
				glideForceAddedOnce = true;
			}
		}
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = mixerCatColor; 
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = mixer;
		kitchenItem.transform.localPosition += new Vector3 (0.5f, 0, 0);

        if (platformerCharacter2D == null) platformerCharacter2D = GetComponent<PlatformerCharacter2D>();

        // load mixer loop
        platformerCharacter2D.catEffectAudioSource.clip = mixerLoop;
        // enable looping while mixer cat is active
        platformerCharacter2D.catEffectAudioSource.loop = true;
        // start music and pause it instantly (to unpause later)
        platformerCharacter2D.catEffectAudioSource.Play();
        platformerCharacter2D.catEffectAudioSource.Pause();
    }

	void OnDisable() {
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = null;
		kitchenItem.transform.localPosition += new Vector3 (-0.5f, 0, 0);

        // enable looping while mixer cat is active
        platformerCharacter2D.catEffectAudioSource.loop = false;
    }

	private void resetGravity() {
		rigidBody.gravityScale = 3;
		glideForceAddedOnce = false;

        // pause mixer sound loop again
        platformerCharacter2D.catEffectAudioSource.Pause();
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
