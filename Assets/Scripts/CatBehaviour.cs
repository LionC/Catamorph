using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class CatBehaviour : MonoBehaviour {

	private GameObject player;  //Reference to Cat GameObject
	public float invisibleTimeAfterHitInitialValue = 3f;  //Initial time to be invisible after being damaged
	private float invisibleTimeAfterHit = 0f;  //Current time to be invisible after being damaged
	public float livesInitialValue = 9f;  //Initial amount of lives
	public bool rocketIsAlwaysAvailable = false;  //Can always switch to RocketCat
	public bool freezerIsAlwaysAvailable = false;  //Can always switch to FreezerCat
	public bool burnerIsAlwaysAvailable = false;  //Can always switch to BurnerCat
	public bool mixerIsAlwaysAvailable = false;  //Can always switch to MixerCat
	private float lives = 9f;  //Current amount of lives
	public MonoBehaviour currentAbility = null;  //Reference to Cat's currentAbility
	private PlatformerCharacter2D platformerCharacter2D;  //Reference to PlatformerCharacter2D
	private Rigidbody2D rigidBody;  //Reference to Rigidbody2D
	private Scene currentScene;  //Reference to currentScene

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		rigidBody = GetComponent<Rigidbody2D> ();  //Initialization of Rigidbody2D
		platformerCharacter2D = GetComponent<PlatformerCharacter2D>();  //Initialization of PlatformerCharacter2D
		currentScene = SceneManager.GetActiveScene ();  //Initialization of currentScene

		if (PlayerPrefs.HasKey ("currentLives"))  //Lives are saved from former level or falling out of the level
			lives = PlayerPrefs.GetFloat ("currentLives");  //Adopt saved value
		else  //Lives aren't saved; loading from main menu or after GameOver
			lives = livesInitialValue;  //Set lives to initial value
	}

	void Update () {
		switchAbility (rocketIsAlwaysAvailable, freezerIsAlwaysAvailable, burnerIsAlwaysAvailable, mixerIsAlwaysAvailable);  //Ability switch at any time
	}

	void FixedUpdate() {
		if (invisibleTimeAfterHit > 0)  //Is invisible after bein damaged
			invisibleTimeAfterHit -= Time.deltaTime;  //Reduce invisibility time counter
		else if (player.GetComponent<SpriteRenderer> ().color.a == 0.5f)  //Invisible state is over
			player.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 1f);  //Set opacity to default
	}

	public float getLives() {
		return lives;
	}

	public float takeDamage(float damage) {
		if (invisibleTimeAfterHit <= 0) {  //Isn't already invisible
			lives -= damage;  //Reduce lives by taken damage
			if (lives <= 0)  //No lives any more
				gameOver ();  //GameOver

			invisibleTimeAfterHit = invisibleTimeAfterHitInitialValue;  //Set invisibility time counter to initial value after being damaged
			player.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0.5f);  //Decrease opacity by half
		}

		return lives;  //Remaining lives
	}

	public void switchAbility(bool rocketIsAvailable, bool freezerIsAvailable, bool burnerIsAvailable, bool mixerIsAvailable) {
		if (Input.GetKeyDown (KeyCode.Alpha1) && rocketIsAvailable) {  //Switch to RockatCat when '1' is pressed
			if(currentAbility != null)  //Check if there was a former ability (otherwise => NullPointerException)
				currentAbility.enabled = false;  //Disable former ability

			currentAbility = GetComponent<RocketCatController> ();  //Set currentAbility to RocketCat
			currentAbility.enabled = true;  //Enable new ability
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2) && freezerIsAvailable) { //Switch to FreezerCat when '2' is pressed
			if(currentAbility != null)  //Check if there was a former ability (otherwise => NullPointerException)
				currentAbility.enabled = false;  //Disable former ability

			currentAbility = GetComponent<FreezerCatController> ();  //Set currentAbility to FreezerCat
			currentAbility.enabled = true;  //Enable new ability
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3) && burnerIsAvailable) { //Switch to BurnerCat when '3' is pressed
			if(currentAbility != null)  //Check if there was a former ability (otherwise => NullPointerException)
				currentAbility.enabled = false;  //Disable former ability

			currentAbility = GetComponent<BurnerCatController> ();  //Set currentAbility to BurnerCat
			currentAbility.enabled = true;  //Enable new ability
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4) && mixerIsAvailable) { //Switch to MixerCat when '4' is pressed
			if(currentAbility != null)  //Check if there was a former ability (otherwise => NullPointerException)
				currentAbility.enabled = false;  //Disable former ability

			currentAbility = GetComponent<MixerCatController> ();  //Set currentAbility to MixerCat
			currentAbility.enabled = true;  //Enable new ability
		}
	}

	public void fallOutOfLevel() {
		PlayerPrefs.SetFloat ("currentLives", lives);  //Save currentLives if falling out of the level
		SceneManager.LoadScene(currentScene.name);  //Reload currentScene
	}

	public void gameOver() {
		if (PlayerPrefs.HasKey ("currentLives"))  //Lives are saved from former level or falling out of the level
			PlayerPrefs.DeleteKey ("currentLives");  //Delete saved value in order to reset lives to initial value

		SceneManager.LoadScene(currentScene.name);  //Reload currentScene
	}
}