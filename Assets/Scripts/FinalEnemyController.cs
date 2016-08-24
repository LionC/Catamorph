using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalEnemyController : MonoBehaviour {

	public int lives = 15;
	public GameObject throwEnemy,hitBoxHead;
	public AudioSource bark, growl;
	public string sceneName;


	private GameObject player;
	private float angryTime,lastHitTime, trefferTimer;
	private bool isAngry;
	private GameObject enemyClone;

	void start (){
		GetComponent<MouseThrowChees> ().delay = 3;
		//movement by normal enemycontoll
		GetComponent<EnemyController> ().enabled = true;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate(){  
		//hit by player
		if (hitBoxHead.GetComponent<finalEnemyhitTrigger>().hit() == true && lastHitTime +  1.5f < Time.time){
			lives--;
			lastHitTime = Time.time;
			isAngry = true;
		}

		if (isAngry == true ) {
			//movement angry
			GetComponent<Fader>().fadeIn();
			GetComponent<EnemyController> ().enabled = false;
			GetComponent<Rigidbody2D>().AddForce (new Vector2((player.transform.position.x-transform.position.x)*2000,0.0f));
			if (lives % 3 == 0) {
				
				enemyClone = Instantiate (throwEnemy);
				enemyClone.transform.position = transform.position + (new Vector3 (1.0f, 0.0f, 0.0f));
				enemyClone.SetActive (true);
				bark.Play();
			}
			isAngry = false;
			GetComponent<EnemyController> ().enabled = true;
		}
		if (lives <= 0) {
			Destroy (gameObject);
			SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
		}
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.tag == "Rocket" || coll.collider.tag == "Laser") {
			lives--;
			GetComponent<Fader> ().fadeOut();
			GetComponent<MouseThrowChees> ().delay = lives / 5;
			isAngry = true;

		}

		if (coll.collider.tag == "Player" && isAngry == false && trefferTimer + 3 < Time.time) {
			trefferTimer = Time.time;
			growl.Play();
		}
	}
}
