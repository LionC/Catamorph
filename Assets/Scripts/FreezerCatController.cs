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
		platformerCharacter2D.setMaxSpeed (speedAsFreezer);
		player.GetComponent<Animator> ().SetBool ("Freezer",true);
	}
	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);
		player.GetComponent<Animator> ().SetBool ("Freezer",false);
	}

	public override string ToString() {
		return "FreezerCat";
	}
}
