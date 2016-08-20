using UnityEngine;
using System.Collections;

public class MouseThrowChees : MonoBehaviour {

	public GameObject cheese;
	private GameObject player;
	private GameObject cheeseClone;
	public float delay, forceUp;
	private float timeLastShot, abs;
	private Vector3 dir, posEnemy, posPlayer;

	// Use this for initialization
	void Start () {
		timeLeftShoot = 1;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		posEnemy = transform.position;
		posPlayer = player.transform.position;
		abs = Vector3.Distance (posEnemy, posPlayer);
		dir = (player.transform.position - transform.position) + new Vector3 (0, 1,0);

		if ((Vector2.Distance(player.transform.position, transform.position) < 5) && (timeLastShot +delay <= Time.time) && abs >3) {
			cheeseClone = Instantiate (cheese);
			cheeseClone.SetActive (true);
			cheeseClone.transform.position = transform.position;
			cheeseClone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(dir.x*(forceUp/2), dir.y*forceUp));
			timeLastShot = Time.time;
		}

		dir = player.transform.position - transform.position;

		if ((GetComponent<EnemyControll> ().abs < 5) && (timeLeftShoot <= 0)) {
			Instantiate (chees);
			chees.transform.position = transform.position;
			chees.AddComponent<Rigidbody2D> ().AddForce (new Vector2 (dir.y*100, -100.0f));
			timeLeftShoot = 1.0f;
		}


	}
}
