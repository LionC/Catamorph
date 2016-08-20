using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class RocketCatController : MonoBehaviour {

	public GameObject player;
	public GameObject rocketPack;
	public float jumpForceAsRocket = 1200f;
	public float jumpForceAsDefault = 600f;
	public Color rocketCatColor = new Color(60, 179, 113);

	private PlatformerCharacter2D platformerCharacter2D;
	private ObjectSpawner rocketSpawner;
	private GameObject rocket;
	private Rigidbody2D rigidBody;
	public bool isRocketJumping = false;

	void Awake () {
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
	}
		
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		rocketSpawner = rocketPack.GetComponent<ObjectSpawner> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0))
			rocket = rocketSpawner.spawn ();
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = rocketCatColor; 
		rocketPack.GetComponent<SpriteRenderer> ().enabled = true;
		platformerCharacter2D.setJumpForce (jumpForceAsRocket);
		rocketPack.SetActive (true);
	}

	void OnDisable() {
		platformerCharacter2D.setJumpForce (jumpForceAsDefault);
		rocketPack.GetComponent<SpriteRenderer> ().enabled = false;
		rocketPack.SetActive (false);
	}

	public override string ToString() {
		return "RockatCat";
	}
}