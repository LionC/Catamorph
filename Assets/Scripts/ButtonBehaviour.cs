using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {

	public GameObject toDestroy;
	public Sprite afterTriggerSprite;

	bool pressed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerenter2D() {
		if (pressed == true)
			return;

		pressed = true;
		GetComponent<SpriteRenderer> ().sprite = afterTriggerSprite;
		Destroy (toDestroy);
	}
}
