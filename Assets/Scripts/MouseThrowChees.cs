using UnityEngine;
using System.Collections;

public class MouseThrowChees : MonoBehaviour {

	public GameObject player, cheese, cheeseClone;
	public float timeLastShot, delay, forceUp;
	public Vector3 dir;

	// Use this for initialization
	void Start () {
		timeLastShot = 0;
		forceUp = 160;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		dir = (player.transform.position - transform.position) + new Vector3 (0, 1,0);

		if ((Vector2.Distance(player.transform.position, transform.position) < 5) && (timeLastShot +delay <= Time.time)) {
			cheeseClone = Instantiate (cheese);
			cheeseClone.SetActive (true);
			cheeseClone.transform.position = transform.position;
			cheeseClone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(dir.x*(forceUp/2), dir.y*forceUp));
			timeLastShot = Time.time;
		}




	}
}
