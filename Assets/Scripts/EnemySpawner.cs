using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy, player;
	public int häufigkeit, anzEnemy;
	public float delay, timeLastSpawn;
	public int countEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if ((Random.Range(0, 10 -häufigkeit) == 0)  && (timeLastSpawn + delay <= Time.time) && countEnemy <= anzEnemy-1){
			Instantiate (enemy);
			countEnemy++;
			enemy.transform.position = transform.position;
			timeLastSpawn = Time.time;
		}

	}
}
