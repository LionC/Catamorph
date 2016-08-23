using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class RocketCatController : MonoBehaviour {

	private GameObject player;
	public float jumpForceAsRocket = 1200f;
	public float jumpForceAsDefault = 600f;
	public Color rocketCatColor = new Color(60, 179, 113);
	public Sprite rocketPack;

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
		if (Input.GetKeyDown (KeyCode.Mouse0))
			rocketSpawner.spawn ();
		if (Input.GetButtonDown("Jump"))
			catBehavior.takeDamage(1f);
	}

	void OnEnable() {
		rocketSpawner = kitchenItem.GetComponent<ObjectSpawner> ();
		platformerCharacter2D.setJumpForce (jumpForceAsRocket);
		player.GetComponent<Animator> ().SetBool ("Rocket",true);
	}

	void OnDisable() {
		platformerCharacter2D.setJumpForce (jumpForceAsDefault);
		player.GetComponent<Animator> ().SetBool ("Rocket",false);
	}

	public override string ToString() {
		return "RockatCat";
	}
}