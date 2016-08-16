using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityStandardAssets._2D;

public class CatBehaviour : MonoBehaviour {

	//public Component posEmpty = null;

	private enum Character {
		FREEZE, FLAME
	}

	private Character character = Character.FREEZE;
	private Rigidbody2D rigidBody;
	private bool isFlying = false;
	private bool isGliding = false;
	public float flyForce = 50f;
	public float glideForce = 30f;

	private IDictionary colorMap = new Dictionary<Character, Color> ();

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		colorMap.Add (Character.FREEZE, new Color (0, .2f, .8f));
		colorMap.Add (Character.FLAME, new Color (.8f, .2f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		//positionListener ();
		if (Input.GetKeyDown (KeyCode.B))
			switchCharacter(Character.FLAME);
				
		if (Input.GetKeyDown (KeyCode.C))
			switchCharacter(Character.FREEZE);

		if (Input.GetKey (KeyCode.F))
			isFlying = true;
		else if (!Input.GetKey (KeyCode.F) && isFlying)
			isFlying = false;
		else if (!isFlying && !PlatformerCharacter2D.m_Grounded)
			isGliding = true;
		else if (isGliding && PlatformerCharacter2D.m_Grounded)
			isGliding = false;
	}

	void FixedUpdate() {
		if (isFlying)
			rigidBody.AddForce(new Vector2(0f, flyForce));
		
		if (isGliding) {
			Debug.Log (rigidBody.velocity.y);
			rigidBody.gravityScale = 0;
			//!!!position or velocity down!!!

			//rigidBody.AddForce(new Vector2(0f, glideForce));
		}
	}

	private bool switchCharacter(Character to) {
		if (character == to)
			return false;

		character = to;
		gameObject.GetComponent<SpriteRenderer> ().color = (Color)colorMap[to];

		return true;
	}

//	private bool positionListener(){
//		if (Mathf.Round(this.transform.position.x) == Mathf.Round(posEmpty.transform.position.x)) {
//			switchCharacter(Character.FLAME);
//			return true;
//		}
//		return false;
//	
//	}
}

