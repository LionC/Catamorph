using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditAnimationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
	}
}
