using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;


public class EnemyControll : MonoBehaviour {

	private GameObject player;
	public MonoBehaviour spawner;
	public bool hindernis;
	public Vector3 posEnemy, posPlayer;
	public float abs, timeLastHit, timeLastJump, jumpTry;
	public float damageValue = 1f, delayHit, delayJump;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate(){
		posEnemy = transform.position;
		posPlayer = player.transform.position;

		if (posEnemy.x < posPlayer.x) {
			GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			GetComponent<SpriteRenderer>().flipX = false;
		}

		abs = Vector3.Distance (posEnemy, posPlayer);

		if (abs < 3 && tag != "Maus") {
			transform.position += new Vector3 (((posPlayer.x - posEnemy.x) *0.02f), 0.0f, 0.0f);
			//Jump
			if ((posPlayer.y - posEnemy.y > 2.0f || hindernis == true) && timeLastJump + delayJump <= Time.time) {
				jumpTry += 0.1f;
				transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
				timeLastJump = Time.time;
			}
			hindernis = false;
		} else {
			if (abs < 6 && abs > 3.5f) {
				transform.position += new Vector3 (((posPlayer.x - posEnemy.x) * 0.02f), 0.0f, 0.0f);
			} else {
				if (abs < 3 && tag == "Maus") {
					transform.position += new Vector3 (((posEnemy.x - posPlayer.x) * 0.02f), 0.0f, 0.0f);
				}
			}
		}
		if (abs > 10) {
			//spawner.GetComponent<ObjectSpawner> ().reduceObjectsOnScreen ();
			//Destroy (gameObject);
		}
	}


	private void OnCollisionEnter2D(Collision2D coll){
		if (tag == "Hund") {
			print ("damage");
			if (coll.collider.tag == "Player" && timeLastHit + delayHit <= Time.time) {
				transform.position += new Vector3 (coll.collider.GetComponent<Rigidbody2D>().velocity.x*(-1),0.5f,0.0f); //nach hinten fliegen
				timeLastHit = Time.time; //Timer Hit reset
				player.GetComponent<CatBehaviour>().takeDamage(damageValue);
			}

			if (coll.collider.tag != "Player" && coll.collider.tag != "Ground") {
				hindernis = true;
			}
		}
	}
}
