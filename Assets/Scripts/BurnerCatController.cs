using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class BurnerCatController : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public float speedAsBurner = 10f;  //Moving pace if BurnerCat
	public float speedAsDefault = 5f;  //Default moving pace if not BurnerCat
	public Color burnerCatColor = new Color(75, 0, 130);  //Skin color
	public AudioClip burnLoop;
	public Sprite burner;  //BurnerCat Sprite
	private GameObject kitchenItem;  //Reference to KitchenItem GameObject
	private PlatformerCharacter2D platformerCharacter2D;  //Reference to PlatformerCharacter2D

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		kitchenItem = player.transform.Find ("KitchenItem").gameObject;  //Initialization of KitchenItem GameObject
		platformerCharacter2D = player.GetComponent<PlatformerCharacter2D> ();  //Initialization of PlatformerCharacter2D
	}

	void OnEnable() {
		platformerCharacter2D.setMaxSpeed (speedAsBurner);
		kitchenItem.transform.localScale = new Vector3(-1f, -1f, 1f);
		kitchenItem.transform.localPosition += new Vector3(-1.35f, 0.2f, 0f);
		kitchenItem.GetComponent<SpriteRenderer>().sprite = burner;
		player.GetComponent<Animator> ().SetBool ("Burner", true);
	}

	void OnDisable() {
		platformerCharacter2D.setMaxSpeed (speedAsDefault);
		kitchenItem.transform.localPosition += new Vector3(1.35f, -0.2f, 0f);
		kitchenItem.GetComponent<SpriteRenderer>().sprite = null;
		player.GetComponent<Animator> ().SetBool ("Burner", false);

	}

	public override string ToString() {
		return "BurnerCat";  //Authentification of BurnerCat
	}
}