using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour {
	public GameObject player;
	private Vector3 posPlayer, posDog;
	public float abs,tryJump;
	private bool wallCol = false;

	// Use this for initialization
	void Start () {
	
	}
		
	
	// Update is called once per frame
	void FixedUpdate () {
		posPlayer = player.transform.position;
		posDog = transform.position;
		abs = Vector3.Distance (posDog, posPlayer);
		if (abs < 5) {
			if ((Mathf.Abs (posPlayer.y - posDog.y) > 2 || wallCol == true) && tryJump <2) {
				tryJump+= 0.2f;
			}
			transform.position += new Vector3 ((posPlayer.x - posDog.x) / 20.0f, tryJump, 0.0f);
			wallCol = false;
			tryJump = 0.0f;
		}
	}

	private void OnCollisionEnter2D(Collision2D other){
		if (other.collider.tag != "Player" && other.collider.tag != "Boden") {
			wallCol = true;

		}
		if (other.collider.tag == "Player") {
			transform.position += new Vector3 ((posPlayer.x - posDog.x) / 20.0f, 0, 0.0f);
		}
	}
		
}
