using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class BurnerCatController : MonoBehaviour {

	public GameObject player;
	public float speedAsBurner = 10f;
	public float speedAsDefault = 5f;
	public Color burnerCatColor = new Color(75, 0, 130);
	private PlatformerCharacter2D platformerCharacter2D;

	void Awake () {
		platformerCharacter2D = GetComponent<PlatformerCharacter2D> ();
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = burnerCatColor; 
		platformerCharacter2D.setMaxSpeed (speedAsBurner);
	}

	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);
	}

	public override string ToString() {
		return "BurnerCat";
	}
}