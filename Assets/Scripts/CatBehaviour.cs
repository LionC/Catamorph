using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class CatBehaviour : MonoBehaviour {

	private GameObject player;
	public float invisibleTimeAfterHitInitialValue = 3f;
	public float invisibleTimeAfterHit = 0f;
	public float livesInitialValue = 9f;
	public float lives = 9f;
	public MonoBehaviour currentAbility = null;
	public float CatchedLives=40f;
	public float CatchedLivesDrain=0.02f;
	public float CatchedLivesAdd=1f;
	private float catchedlivessave;
	public bool IsCatched = false;
	private PlatformerCharacter2D platformerCharacter2D;
	private Rigidbody2D rigidBody;
	private Scene currentScene;
	public float DamageIfHumanCatched,DamgeIfWiggeldFree;
	public Animator rocketCatAnimator;

	void Start () {
		catchedlivessave = CatchedLives;
		if (PlayerPrefs.HasKey ("currentLives"))
			lives = PlayerPrefs.GetFloat ("currentLives");
		else
			lives = livesInitialValue;
		
		rigidBody = GetComponent<Rigidbody2D> ();
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();
		currentScene = SceneManager.GetActiveScene ();
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		switchAbility (true, true, true, true);  // change if wished/implemented
	}

	void FixedUpdate() 
	{
		if (IsCatched)
		{
			CatchedLives -= CatchedLivesDrain;
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				CatchedLives += CatchedLivesAdd;
			}
			if (CatchedLives > 100) 
			{
				CatchedLives = catchedlivessave;
				IsCatched = false;
				takeDamage(0.5f);
			}
			else if (CatchedLives <= 0) 
			{
				CatchedLives = catchedlivessave;
				IsCatched = false;
				takeDamage (1f);
			}

		}
		if (invisibleTimeAfterHit > 0)
			invisibleTimeAfterHit -= Time.deltaTime;
		else if (player.GetComponent<SpriteRenderer> ().color.a == 5f)
			player.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);
	}

	public float takeDamage(float damage) {
		if (invisibleTimeAfterHit <= 0) {
			lives -= damage;
			if (lives <= 0)
				gameOver ();

			invisibleTimeAfterHit = invisibleTimeAfterHitInitialValue;
			player.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 5f);
		}

		return lives;
	}

	public void switchAbility(bool rocketIsAvailable, bool freezerIsAvailable, bool burnerIsAvailable, bool mixerIsAvailable) {
		if (Input.GetKeyDown (KeyCode.Alpha1) && rocketIsAvailable) {
			if(currentAbility != null)
				currentAbility.enabled = false;

			currentAbility = GetComponent<RocketCatController> ();
			//player.GetComponent<Animator> ().runtimeAnimatorController.animationClips = rocketCatAnimator;
			currentAbility.enabled = true;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2) && freezerIsAvailable) {
			if(currentAbility != null)
				currentAbility.enabled = false;

			currentAbility = GetComponent<FreezerCatController> ();
			currentAbility.enabled = true;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3) && burnerIsAvailable) {
			if(currentAbility != null)
				currentAbility.enabled = false;

			currentAbility = GetComponent<BurnerCatController> ();
			currentAbility.enabled = true;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4) && mixerIsAvailable) {
			if(currentAbility != null)
				currentAbility.enabled = false;

			currentAbility = GetComponent<MixerCatController> ();
			currentAbility.enabled = true;
		}
	}

	public void fallOutOfLevel() {
		PlayerPrefs.SetFloat ("currentLives", lives);
		SceneManager.LoadScene(currentScene.name);
	}

	public void gameOver() {
		if (PlayerPrefs.HasKey ("currentLives"))
			PlayerPrefs.DeleteKey ("currentLives");

		SceneManager.LoadScene(currentScene.name);
	}
}