using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class BurnerCatController : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public float speedAsBurner = 10f;  //Moving pace if BurnerCat
	public float speedAsDefault = 5f;  //Default moving pace if not BurnerCat
	public Color burnerCatColor = new Color(75, 0, 130);  //Skin color
	public Sprite burner;  //BurnerCat Sprite
	private GameObject kitchenItem;  //Reference to KitchenItem GameObject
	private PlatformerCharacter2D platformerCharacter2D;  //Reference to PlatformerCharacter2D


	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		kitchenItem = player.transform.Find ("KitchenItem").gameObject;  //Initialization of KitchenItem GameObject
		platformerCharacter2D = GetComponent<PlatformerCharacter2D> ();  //Initialization of PlatformerCharacter2D
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = burnerCatColor;  //Changing the skin color
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = burner;  //Changing the KitchenItem sprite to burner
		kitchenItem.transform.localPosition += new Vector3 (-0.5f, -0.2f, 0);  //Moving the burner in order to be fitting
		platformerCharacter2D.setMaxSpeed (speedAsBurner);  //Increase Cat's maximum moving pace
	}

	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);  //Decrease Cat's maximum moving pace
		kitchenItem.GetComponent<SpriteRenderer> ().sprite = null;  //Remove the KitchenItem sprite
		kitchenItem.transform.localPosition += new Vector3 (0.5f, 0.2f, 0);  //Moving the KitchenItem to default position
	}

	public override string ToString() {
		return "BurnerCat";  //Authentification of BurnerCat
	}
}