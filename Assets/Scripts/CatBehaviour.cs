using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityStandardAssets._2D;

public class CatBehaviour : MonoBehaviour {

	public GameObject player;
	public static int MAX_LIFE=9;
	public int batteryMax = 100;
	public double batteryActual = 0;
	public int lives = 9;
	public float flyForce = 50f;
	public float glideForce = -1f;
	public float glideVelocityDelay = -2.5f; //Lower value => faster glide-down 
	public int CharacterType= 0;
	public enum Character {
		FREEZE, FLAME
	}

	private Character character = Character.FREEZE;
	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	private bool isFlying = false;
	private bool isGliding = false;
	private bool glideForceAddedOnce = false;

	private IDictionary colorMap = new Dictionary<Character, Color> ();

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
		colorMap.Add (Character.FREEZE, new Color (0, .2f, .8f));
		colorMap.Add (Character.FLAME, new Color (.8f, .2f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			switchCharacter (Character.FLAME);
			CharacterType = 1;
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			switchCharacter (Character.FREEZE);
			if (Input.GetKey (KeyCode.F)) {
				isFlying = true;
			} else if (!Input.GetKey (KeyCode.F) && isFlying && !platformerCharacter2D.m_Grounded) {
				isFlying = false;
				isGliding = true;
			} else if (isGliding && platformerCharacter2D.m_Grounded) {
				isGliding = false;
				resetGravity ();
			}
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

	private void resetGravity() {
		rigidBody.gravityScale = 3;
		glideForceAddedOnce = false;
	}
		

	private bool switchCharacter(Character to) 
	{
		if (character == to)
			return false;

		character = to;
		gameObject.GetComponent<SpriteRenderer> ().color = (Color)colorMap[to];

		return true;
	}

	public void batteryLoad ()
	{
		batteryActual = batteryMax;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Hund") {
			lives=Damage(lives);

		}
	}
	public int Damage(int lives)
	{
		lives--;
		return lives;
	}

	public void gameOver(){	//Alles was nach tod passsiert
		Destroy(gameObject);
	}

}

