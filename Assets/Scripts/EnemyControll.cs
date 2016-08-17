using UnityEngine;
using System.Collections;

public class EnemyControll : MonoBehaviour {

	public GameObject player;
	public bool hindernis, hit;
	public Vector3 posEnemy, posPlayer;
	public float abs, timeLeftHit, timeLeftJump, jumpTry;

	// Use this for initialization
	void Start () {
		timeLeftHit = 5;	//5 Sekunden zum nächsten schlag
		timeLeftJump = 0.2f; // Zeit bis zum nächsten Sprung
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		if (timeLeftHit > 0) {
			timeLeftHit -= Time.deltaTime;
		}

		if (timeLeftJump > 0) {
			timeLeftJump -= Time.deltaTime;
		}

		posEnemy = transform.position;
		posPlayer = player.transform.position;

		abs = Vector3.Distance (posEnemy, posPlayer);

		if (abs < 3) {
			transform.position += new Vector3 (((posPlayer.x - posEnemy.x)/ 10),0.0f,0.0f);
			if ((posEnemy.y-posPlayer.y>1.5f)|| (timeLeftJump <= 0 && hindernis == true)){
				jumpTry += 0.5f;
				transform.position += new Vector3 (0.0f, jumpTry, 0.0f);
				timeLeftJump = 0.2f;
			}
			hindernis = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.tag == "Player" && timeLeftHit <= 0) {
			timeLeftHit = 5.0f; //Timer Hit reset
			hit = true;	//Spieler wurde getroffen
		}

		if (coll.collider.tag != "Player" && coll.collider.tag != "Ground") {
			hindernis = true;
		}
	}
}
