using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {

	public GameObject laser;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		laser.transform.position =Vector3.Reflect(transform.position, Vector3.right) ;
	}
}
