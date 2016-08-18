using UnityEngine;
using System.Collections;

public class MouseThrowChees : MonoBehaviour {

	public GameObject player, chees;
	public float timeLeftShoot = 1.0f;
	public Vector3 dir;

	// Use this for initialization
	void Start () {
		timeLeftShoot = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (timeLeftShoot > 0) {
			timeLeftShoot -= Time.deltaTime;
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
