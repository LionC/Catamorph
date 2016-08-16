using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenderController : MonoBehaviour {
	public int CatTransformation;
	public GameObject Weapons;
	private SpriteRenderer render;
	public Sprite MännchenGrün;
	public Sprite MännchenRot;
	public Sprite MännchenSchwarz;
	public Sprite clear;
	// Use this for initialization
	void Start () {
		CatTransformation= 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			CatTransformation = CatTransformation + 1;
			CatTransformation = CatTransformation %4;
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			CatTransformation = CatTransformation - 1;
			if (CatTransformation == -1)
				CatTransformation = 3;
		}
		switch (CatTransformation) {
		case 1:
			Weapons.GetComponent<SpriteRenderer> ().sprite = MännchenGrün;
			break;
		case 2:
			Weapons.GetComponent<SpriteRenderer> ().sprite = MännchenRot;
			break;
		case 3:
			Weapons.GetComponent<SpriteRenderer> ().sprite = MännchenSchwarz;
			break;
		default:
			Weapons.GetComponent<SpriteRenderer> ().sprite=clear;
			break;

		}
	}
		
		void OnGUI () {
		// Make a background box
		GUI.Box (new Rect (10, 10, 100, 90), CatTransformation.ToString());
	}
}
