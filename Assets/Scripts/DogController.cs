using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			print ("TestCollider");
		}
	}
}
