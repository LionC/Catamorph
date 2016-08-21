using UnityEngine;
using System.Collections;

public class FinalEnemyController : MonoBehaviour {

	public int lives = 15;
	private GameObject player;
	private float angryTimeStart;
	private bool isgrounded;
	private GameObject enemyClone, enemy;

	void Awake () {
		enemy = GameObject.FindGameObjectWithTag ("Maus");
	}

	void FixedUpdate(){
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.tag == "Player") {
			lives--;
			GetComponent<MouseThrowChees> ().delay = lives;
			angry ();
			print ("angry");
		}
			
		if (coll.collider.tag == "Boden")
			isgrounded = true;
		print ("Boden");
	}

	private void angry(){
		GetComponent<EnemyControll> ().enabled = false;
		angryTimeStart = Time.time;
		while (angryTimeStart + 5 <= Time.time) {
			if (isgrounded) {
				isgrounded = false;
				GetComponent<Rigidbody2D> ().AddForce (new Vector2(0.0f, 100.0f));
				enemyClone = Instantiate (enemy);
				enemyClone.transform.position += transform.position +(new Vector3 (1.0f, 1.0f,0.0f));
				enemyClone.SetActive (true);
			}
		}
		GetComponent<EnemyControll> ().enabled = true;
	}
}
