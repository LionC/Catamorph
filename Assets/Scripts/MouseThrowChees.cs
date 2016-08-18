using UnityEngine;
using System.Collections;

public class MouseThrowChees : MonoBehaviour {

	public GameObject player, mouse, chees;
	public float timeLeftShoot = 1;
	public Vector3 dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (timeLeftShoot > 0) {
			timeLeftShoot -= Time.deltaTime;
		}

		dir = player.transform.position - mouse.transform.position;

		if (player.GetComponent<EnemyControll> ().abs < 5 && timeLeftShoot <= 0) {
			Instantiate (chees);
			chees.AddComponent<Rigidbody2D> ().AddForce (new Vector2 (dir.x*100, 100.0f));
		}
	}
}
