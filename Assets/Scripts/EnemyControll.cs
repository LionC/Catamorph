using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;


public class EnemyControll : MonoBehaviour {

	private GameObject player;
	public EnemySpawner spawner;
	public bool hindernis, hit;
	public Vector3 posEnemy, posPlayer;
	public float abs, timeLastHit, timeLastJump, jumpTry;
	public float damageValue = 1f, delayHit, delayJump;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
			transform.position += new Vector3 (((posPlayer.x - posEnemy.x) / 10), 0.0f, 0.0f);
			//Jump
			if ((posPlayer.y - posEnemy.y > 0.5f || hindernis == true) && timeLastJump + delayJump <= Time.time) {
				jumpTry += 0.1f;
				transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
				timeLastJump = Time.time;
			}
			if ((posEnemy.y - posPlayer.y > 0) && tag != "Maus") {
				transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
			}
			hindernis = false;
		} else {
			if (abs < 6 && abs > 3.5f) {
				transform.position += new Vector3 (((posPlayer.x - posEnemy.x) / 20), 0.0f, 0.0f);
			} else {
				if (abs < 3 && tag == "Maus") {
					transform.position += new Vector3 (((posEnemy.x - posPlayer.x) / 20), 0.0f, 0.0f);
				}
			}
		}
		if (abs > 10) {
			spawner.DestroyEnemy ();
			Destroy (gameObject);
		}
	}


		private void OnCollisionEnter2D(Collision2D coll){
		if ((tag == "Dog") || (tag == "Human")){
			if (coll.collider.tag == "Player" && timeLastHit + delayHit <= Time.time) {
				transform.position += new Vector3 (coll.collider.GetComponent<Rigidbody2D>().velocity.x*(-1),0.5f,0.0f); //naach hinten fliegen
				timeLastHit = Time.time; //Timer Hit reset
				hit = true;	//Spieler wurde getroffen
				player.GetComponent<CatBehaviour>().takeDamage(damageValue);
			}

			if (coll.collider.tag != "Player" && coll.collider.tag != "Ground") {
				hindernis = true;
			}
		}
	}
}
