using UnityEngine;
using System.Collections;

public class FreezerCatController : MonoBehaviour {

	public GameObject player;
	public Color freezerCatColor = new Color(98, 181, 229);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = freezerCatColor; 
	}

	public override string ToString() {
		return "FreezerCat";
	}
}
