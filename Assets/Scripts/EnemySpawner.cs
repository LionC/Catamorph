using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy, player;
	public int häufigkeit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (Random.Range(0, 100 -häufigkeit) == 0){
			Instantiate (enemy);
			transform.position = player.transform.position + new Vector3 (5.0f, 0.1f, 0.0f);
		}

	}
}
