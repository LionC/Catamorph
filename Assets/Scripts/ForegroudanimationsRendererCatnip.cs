using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForegroudanimationsRendererCatnip : MonoBehaviour {

	private  CatMintController catmintcontroller;
	private float startTime;
	private bool played;

	void Start() {
		catmintcontroller = GameObject.FindGameObjectWithTag("Katzenminze").GetComponent<CatMintController> ();
		gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,0);
		played = false;

	}

	void FixedUpdate () {

		if (catmintcontroller != null && catmintcontroller.hitCatMint() == true) {
			startTime = Time.time;
			played = true;
			GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
			gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,255);

		} else{
			if (startTime + 5 > Time.time && played == true) {
				GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
				gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,255);
			} else {
				gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,0);
				played = false;

			}
		}

	}
}
