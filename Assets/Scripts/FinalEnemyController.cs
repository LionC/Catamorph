using UnityEngine;
using System.Collections;

public class FinalEnemyController : MonoBehaviour {

	public int lives = 15;
	public GameObject throwEnemy,hitBoxHead;


	private GameObject player;
	private float angryTime,lastHitTime;
	private bool isgrounded, isAngry;
	private GameObject enemyClone;

	void start (){
		GetComponent<MouseThrowChees> ().delay = 3;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate(){  
		if (hitBoxHead.GetComponent<finalEnemyhitTrigger>().hit() == true && lastHitTime +  2 <= Time.time){
			print (lives);
			lives--;
			lastHitTime = Time.time;
			print ("hit");
			isAngry = true;
		}

		if (isgrounded && isAngry == true ) {
			print ("angry");
			GetComponent<EnemyControll> ().enabled = false;
			GetComponent<Rigidbody2D>().AddForce (new Vector2((player.transform.position.x-transform.position.x),0.0f));
			if (lives % 3 == 0) {
				isgrounded = false;
				enemyClone = Instantiate (throwEnemy);
				enemyClone.transform.position = transform.position + (new Vector3 (1.0f, 1.0f, 0.0f));
				enemyClone.SetActive (true);
			}
			isAngry = false;
			GetComponent<EnemyControll> ().enabled = true;
		}
		if (lives <= 0) {
			Destroy (gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D coll){
			
		if (coll.collider.tag == "Ground")
			isgrounded = true;

		if (coll.collider.tag == "Rocket" || coll.collider.tag == "Laser") {
			lives--;
			GetComponent<MouseThrowChees> ().delay = lives / 5;
		}


	}
}
