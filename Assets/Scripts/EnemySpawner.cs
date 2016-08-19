﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	private GameObject player;
	public int häufigkeit, anzEnemy;
	public float delay;
	private float timeLastSpawn;
	private int countEnemy;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestroyEnemy(){
		countEnemy--;
	}

	void FixedUpdate(){
		if ((Random.Range(0, 10 -häufigkeit) == 0)  && (timeLastSpawn + delay <= Time.time) && countEnemy <= anzEnemy-1){
			var enemyClone = Instantiate (enemy);
			enemyClone.GetComponent<EnemyControll> ().spawner = this;
			countEnemy++;
			enemyClone.SetActive (true);
			enemyClone.transform.position = transform.position;
			timeLastSpawn = Time.time;
		}


	}
}
