using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class BurnerCatController : MonoBehaviour {

	private GameObject player;
	public float speedAsBurner = 10f;
	public float speedAsDefault = 5f;
	public Color burnerCatColor = new Color(75, 0, 130);
	public Sprite burner;
	private GameObject kitchenItem;
	private PlatformerCharacter2D platformerCharacter2D;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		kitchenItem = player.transform.Find ("KitchenItem").gameObject;
		platformerCharacter2D = GetComponent<PlatformerCharacter2D> ();
	}

	void OnEnable() {
		platformerCharacter2D.setMaxSpeed (speedAsBurner);
		player.GetComponent<Animator> ().SetBool ("Burner",true);
	}

	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);
		player.GetComponent<Animator> ().SetBool ("Burner",false);
	}

	public override string ToString() {
		return "BurnerCat";
	}
}