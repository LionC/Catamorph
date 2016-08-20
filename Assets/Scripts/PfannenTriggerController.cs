using UnityEngine;
using System.Collections;
using System;

public class PfannenTriggerController : MonoBehaviour {
	private CircleCollider2D Collider;

	void start(){
		Collider = GetComponent<CircleCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			GameObject Pfanne = transform.parent.gameObject;
			PfannenBewegung PfannenBewegung = Pfanne.GetComponent<PfannenBewegung> ();
			PfannenBewegung.SetActive = true;
		}
	}
}
