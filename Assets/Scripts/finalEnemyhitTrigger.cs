using UnityEngine;
using System.Collections;

public class finalEnemyhitTrigger : MonoBehaviour {

	private GameObject finalEnemy;
	private bool hitBool;

	// Use this for initialization
	void Start () {
	
	}
	void Awake(){
		finalEnemy = GameObject.FindGameObjectWithTag ("finalEnemy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			hitBool = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			print ("out");
			hitBool = false;
		}
	}

	public bool hit(){
		return hitBool;
	}
}
