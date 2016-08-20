using UnityEngine;
using System.Collections;

public class MirrorController : MonoBehaviour {

	private bool mirror;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player") {
			mirror = true;
		}
	}

	public bool isMirror(){
		return mirror;
	}
}
