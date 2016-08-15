using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CatBehaviour : MonoBehaviour {

	private enum Character {
		FREEZE, FLAME
	}

	private Character character = Character.FREEZE;

	private IDictionary colorMap = new Dictionary<Character, Color> ();

	// Use this for initialization
	void Start () {
		colorMap.Add (Character.FREEZE, new Color (0, .2f, .8f));
		colorMap.Add (Character.FLAME, new Color (.8f, .2f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			switchCharacter(Character.FLAME);
				
		if (Input.GetKeyDown (KeyCode.Alpha2))
			switchCharacter(Character.FREEZE);
	}

	private bool switchCharacter(Character to) {
		if (character == to)
			return false;

		character = to;
		gameObject.GetComponent<SpriteRenderer> ().color = (Color)colorMap[to];

		return true;
	}
}

