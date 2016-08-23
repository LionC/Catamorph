using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class FreezerCatController : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public float speedAsFreezer = 2.5f;  //Moving pace if FreezerCat
	public float speedAsDefault = 5f;  //Default moving pace if not BurnerCat
	public Color freezerCatColor = new Color(98, 181, 229);  //Skin color
	private PlatformerCharacter2D platformerCharacter2D;  //Reference to PlatformerCharacter2D

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		platformerCharacter2D = GetComponent<PlatformerCharacter2D> ();  //Initialization of PlatformerCharacter2D
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = freezerCatColor;   //Changing the skin color
		player.GetComponent<Animator> ().SetBool ("Freezer", true);  //Enabled animation
		platformerCharacter2D.setMaxSpeed (speedAsFreezer);  //Decrease Cat's maximum moving pace
	}
	void OnDisable() {
		player.GetComponent<Animator> ().SetBool ("Freezer", false);  //Disabled animation
		platformerCharacter2D.setMaxSpeed (speedAsDefault);  //Increase Cat's maximum moving pace
	}

	public override string ToString() {
		return "FreezerCat";  //Authentification of FreezerCat
	}
}
