using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class RocketCatController : MonoBehaviour {

	private GameObject player;
	public float jumpForceAsRocket = 1200f;
	public float jumpForceAsDefault = 600f;
	public Color rocketCatColor = new Color(60, 179, 113);
	public Sprite rocketPack;
    public AudioClip rocketStartSound;
    public AudioClip rocketJumpSound;

    private GameObject kitchenItem;
	private ObjectSpawner rocketSpawner;
	private CatBehaviour catBehavior;
	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	public bool isRocketJumping = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		kitchenItem = player.transform.Find ("KitchenItem").gameObject;
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}
		
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		catBehavior = GetComponent<CatBehaviour> ();
	}
		
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (rocketSpawner.spawn() != null) { 
                platformerCharacter2D.catEffectAudioSource.clip = rocketStartSound;
                platformerCharacter2D.catEffectAudioSource.Play();
            }
        }

        // TODO: only play sound / take damage if jump really occures (not while pressing jump mid-air)
		if (Input.GetButtonDown("Jump")) {
            // load and play rocket jump sound
            platformerCharacter2D.catEffectAudioSource.clip = rocketJumpSound;
            platformerCharacter2D.catEffectAudioSource.Play();
            catBehavior.takeDamage(1f);
        }
    }

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = rocketCatColor; 
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = rocketPack;
		kitchenItem.transform.localPosition += new Vector3 (0.5f, 0, 0);
		rocketSpawner = kitchenItem.GetComponent<ObjectSpawner> ();
		platformerCharacter2D.setJumpForce (jumpForceAsRocket);
	}

	void OnDisable() {
		platformerCharacter2D.setJumpForce (jumpForceAsDefault);
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = null;
		kitchenItem.transform.localPosition += new Vector3 (-0.5f, 0, 0);
	}

	public override string ToString() {
		return "RockatCat";
	}
}