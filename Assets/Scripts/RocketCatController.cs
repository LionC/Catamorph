using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class RocketCatController : MonoBehaviour {

	private GameObject player;
	private GameObject groundCheck;
	private Animator animator;
	public float jumpForceAsRocket = 1200f;
	public float jumpForceAsDefault = 600f;
	public Color rocketCatColor = new Color(60, 179, 113);
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
		groundCheck = player.transform.Find ("GroundCheck").gameObject;
		animator = groundCheck.GetComponent<Animator>();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}
		
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		catBehavior = GetComponent<CatBehaviour> ();
	}
		
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("Fire")) {
            if (rocketSpawner.spawn() != null) { 
                //platformerCharacter2D.catEffectAudioSource.clip = rocketStartSound;
                //platformerCharacter2D.catEffectAudioSource.Play();
            }
        }

        // TODO: only play sound / take damage if jump really occures (not while pressing jump mid-air)
		if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            //platformerCharacter2D.catEffectAudioSource.clip = rocketJumpSound;
            //platformerCharacter2D.catEffectAudioSource.Play();
            catBehavior.takeDamage(1f);
			Debug.Log("Explode");
			animator.SetTrigger("Explode");
        }
    }

	void OnEnable() {
		rocketSpawner = kitchenItem.GetComponent<ObjectSpawner> ();
		platformerCharacter2D.setJumpForce (jumpForceAsRocket);
		player.GetComponent<Animator> ().SetBool ("Rocket", true);
	}

	void OnDisable() {
		platformerCharacter2D.setJumpForce (jumpForceAsDefault);
		player.GetComponent<Animator> ().SetBool ("Rocket", false);
	}

	public override string ToString() {
		return "RockatCat";
	}
}