using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class CatBehaviour : MonoBehaviour {

	public GameObject player;
	public float invisibleTimeAfterHitInitialValue = 3f;
	public float invisibleTimeAfterHit = 0f;
	public float livesInitialValue = 9f;
	public float lives = 9f;
	public MonoBehaviour currentAbility = null;

	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	private Scene currentScene;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("currentLives"))
			lives = PlayerPrefs.GetFloat ("currentLives");
		else
			lives = livesInitialValue;
		
		rigidBody = GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
		currentScene = SceneManager.GetActiveScene ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if(currentAbility != null)
				currentAbility.enabled = false;
			
			currentAbility = GetComponent<MixerCatController> ();
			currentAbility.enabled = true;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if(currentAbility != null)
				currentAbility.enabled = false;

			currentAbility = GetComponent<FreezerCatController> ();
			currentAbility.enabled = true;
		}
	}

	void FixedUpdate() {
		if (invisibleTimeAfterHit > 0)
			invisibleTimeAfterHit -= Time.deltaTime;
	}

	public float takeDamage(float damage) {
		if (invisibleTimeAfterHit <= 0) {
			lives -= damage;
			if (lives == 0)
				gameOver ();

			invisibleTimeAfterHit = invisibleTimeAfterHitInitialValue;
		}
		
		return lives;
	}

	public void fallOutOfLevel() {
		PlayerPrefs.SetFloat ("currentLives", lives);
		gameOver ();
	}

	public void gameOver() {
		SceneManager.LoadScene(currentScene.name);
	}
}