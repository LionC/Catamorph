using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityStandardAssets._2D;

public class CatBehaviour : MonoBehaviour {


	public int batteryMax = 100;
	public double batteryActual = 0;
	public int lives = 9;

	private enum Character {
		FREEZE, FLAME
	}

	private Character character = Character.FREEZE;
	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	private bool isFlying = false;
	private bool isGliding = false;
	private bool glideForceAddedOnce = false;

	public float flyForce = 50f;
	public float glideForce = -1f;

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





		if (Input.GetKeyDown (KeyCode.B))
			switchCharacter(Character.FLAME);
				
		if (Input.GetKeyDown (KeyCode.C))
			switchCharacter(Character.FREEZE);

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
			
		if (isGliding && rigidBody.velocity.y <= -1f && !glideForceAddedOnce) {
			rigidBody.gravityScale = 0;
			rigidBody.AddForce (new Vector2 (0f, glideForce));
			glideForceAddedOnce = true;
		}
	}

	private void resetGravity() {
		rigidBody.gravityScale = 3;
		glideForceAddedOnce = false;
	}

	private bool switchCharacter(Character to) {
		if (character == to)
			return false;

		character = to;
		gameObject.GetComponent<SpriteRenderer> ().color = (Color)colorMap[to];

		return true;
	}


	public void batteryLoad ()
	{
		while (batteryActual < batteryMax) {
			batteryActual += 1;
			//print (batteryActual);
		}
	}

}

