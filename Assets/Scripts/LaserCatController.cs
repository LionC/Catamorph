using UnityEngine;
using System.Collections;

public class LaserCatController : MonoBehaviour {

	public GameObject player;
	public Color laserCatColor = new Color(220, 20, 60);

	// Update is called once per frame
	void Update () {

	}

	void OnEnable() {
		player.GetComponent<SpriteRenderer> ().color = laserCatColor; 
	}

	void OnDisable() {
	}

	public override string ToString() {
		return "LaserCat";
	}
}
