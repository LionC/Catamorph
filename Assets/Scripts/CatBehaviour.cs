using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

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

	private int currentAbilityNum = 0;

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
			if (Input.GetKeyDown ("Jump")) 
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
		int oldAbilityNum = currentAbilityNum;
		if (CrossPlatformInputManager.GetButtonDown ("SwitchAbility")) {
			currentAbilityNum++;
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			currentAbilityNum = 0;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			currentAbilityNum = 1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			currentAbilityNum = 2;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			currentAbilityNum = 3;
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			currentAbilityNum = 4;
		}

		currentAbilityNum %= 5;

		if (!rocketIsAvailable && currentAbilityNum == 1)
			currentAbilityNum++;

		if (!freezerIsAvailable && currentAbilityNum == 2)
			currentAbilityNum++;

		if (!burnerIsAvailable && currentAbilityNum == 3)
			currentAbilityNum++;

		if (!mixerIsAvailable && currentAbilityNum == 4)
			currentAbilityNum++;

		currentAbilityNum %= 5;


		if (currentAbilityNum == oldAbilityNum)
			return;

		if (currentAbility != null)
			currentAbility.enabled = false;
		
		if (currentAbilityNum == 0) {
			currentAbility = null;

			player.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);
		}
		if (currentAbilityNum == 1)
			currentAbility = GetComponent<RocketCatController> ();
		if (currentAbilityNum == 2)
			currentAbility = GetComponent<FreezerCatController> ();
		if (currentAbilityNum == 3)
			currentAbility = GetComponent<BurnerCatController> ();
		if (currentAbilityNum == 4)
			currentAbility = GetComponent<MixerCatController> ();

		if(currentAbility != null)
			currentAbility.enabled = true;
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