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
		player.GetComponent<SpriteRenderer> ().color = burnerCatColor; 
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = burner;
		kitchenItem.transform.localPosition += new Vector3 (-0.5f, -0.2f, 0);
		platformerCharacter2D.setMaxSpeed (speedAsBurner);
	}

	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = null;
		kitchenItem.transform.localPosition += new Vector3 (0.5f, 0.2f, 0);
	}

	public override string ToString() {
		return "BurnerCat";
	}
}