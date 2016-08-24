using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class EnemyController : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public ObjectSpawner spawner;  //Reference to ObjectSpawner
	public float damage = 1f;  //Damage caused by enemy
	public float delayHit;
	public float delayJump;
	private bool obstracle = true;
	private Vector3 posEnemy;
	private Vector3 posPlayer;
	private float abs;
	private float timeLastHit;
	private float timeLastJump;
	private float jumpTry;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initializing Cat GameObject
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

		if (abs < 8 && tag != "Maus") {
			transform.position += new Vector3 (((posPlayer.x - posEnemy.x) *0.02f), 0.0f, 0.0f);
			//Jump
			if ((posPlayer.y - posEnemy.y > 2.0f || obstracle == true) && timeLastJump + delayJump <= Time.time) {
				if (jumpTry < 2) {
					jumpTry += 0.1f;
				}
				transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
				timeLastJump = Time.time;
			}
			obstracle = false;
		} else {
			if (abs < 15 && abs > 5f) {
				transform.position += new Vector3 (((posPlayer.x - posEnemy.x) * 0.02f), 0.0f, 0.0f);
			} else {
				//movement mouse flight
				if (abs < 3 && tag == "Maus") {
					transform.position += new Vector3 (((posEnemy.x - posPlayer.x) * 0.02f), 0.0f, 0.0f);
				}
			}
		}

		if (abs > 20) {
			//del Enemy 
			if (spawner != null && spawner.GetComponent<ObjectSpawner> () != null) {
				spawner.GetComponent<ObjectSpawner> ().reduceObjectsOnScreen ();
				Destroy (gameObject);
			}
		}
	}


	private void OnCollisionEnter2D(Collision2D coll){
		//dog mages damge to cateline
		if (tag == "Hund") {
			if (coll.collider.tag == "Player" && timeLastHit + delayHit <= Time.time) {
				transform.position += new Vector3 (coll.collider.GetComponent<Rigidbody2D>().velocity.x*(-1),0.5f,0.0f); //nach hinten fliegen
				timeLastHit = Time.time; //Timer Hit reset
				player.GetComponent<CatBehaviour>().takeDamage(damage);
			}

			//obstacle
			if (coll.collider.tag != "Player" && coll.collider.tag != "Ground") {
				obstracle = true;
			}
		}
	}
}
