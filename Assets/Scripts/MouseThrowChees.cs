using UnityEngine;
using System.Collections;

public class MouseThrowChees : MonoBehaviour {

	public GameObject player, chees;
	public float timeLastShot, delay;
	public Vector3 dir;

	// Use this for initialization
	void Start () {
		delay = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		

		dir = player.transform.position - transform.position;

		if ((GetComponent<EnemyControll> ().abs < 5) && (timeLastShot +delay <= Time.time)) {
			Instantiate (chees);
			chees.SetActive (true);
			timeLastShot = Time.time;
			chees.transform.position = transform.position;
			chees.AddComponent<Rigidbody2D> ().AddForce (new Vector2 (dir.y*100, 100.0f));
		}


	}
}
