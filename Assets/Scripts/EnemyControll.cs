using UnityEngine;
using System.Collections;

public class EnemyControll : MonoBehaviour {

	public GameObject player;
	public bool hindernis, hit;
	public Vector3 posEnemy, posPlayer;
	public float abs, timeLastHit, timeLastJump, jumpTry;
	public float damageValue = 1f, delayHit, delayJump;
	private bool nichtwichtig;	//test

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		posEnemy = transform.position;
		posPlayer = player.transform.position;

		abs = Vector3.Distance (posEnemy, posPlayer);

		if (abs < 3) {
			transform.position += new Vector3 (((posPlayer.x - posEnemy.x) / 10), 0.0f, 0.0f);
			if ((posPlayer.y - posEnemy.y > 0.5f || hindernis == true) && timeLastJump + delayJump<= Time.time) {
					jumpTry += 0.2f;
					transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
					timeLastJump =Time.time;
				}
				if (posEnemy.y - posPlayer.y > 0) {
					transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
				}
				hindernis = false;
			}
		if (abs > 100) {
			Destroy (gameObject);
			GetComponent<EnemySpawner> ().countEnemy--;
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
