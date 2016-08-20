using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class FreezerCatController : MonoBehaviour {

	private GameObject player;
	public float speedAsFreezer = 2.5f;
	public float speedAsDefault = 5f;
	public Color freezerCatColor = new Color(98, 181, 229);
	private PlatformerCharacter2D platformerCharacter2D;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		platformerCharacter2D = GetComponent<PlatformerCharacter2D> ();
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = freezerCatColor; 
		platformerCharacter2D.setMaxSpeed (speedAsFreezer);
	}
	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);
	}

	public override string ToString() {
		return "FreezerCat";
	}
}
