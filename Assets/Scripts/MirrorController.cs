using UnityEngine;
using System.Collections;

public class MirrorController : MonoBehaviour {

	private bool mirror;

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player") {
			mirror = true;
		}
	}

	public bool isMirror(){
		return mirror;
	}
}
