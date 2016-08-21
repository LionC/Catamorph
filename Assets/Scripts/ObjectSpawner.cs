using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class ObjectSpawner : MonoBehaviour {

	public GameObject spawningObject;
	public int maxCountOnScreen = 1;
	public float delay = 0f;
	public bool takeDirectionOfPlayer = true;
	public Vector2 throwForce = new Vector2 (0, 0);
	public Vector2 relativeSpawningPosition = new Vector2(0, 0);

	private GameObject player;
	private GameObject clonedObject;
	private bool isEnabled = false;
	private int objectsOnScreenCounter = 0;
	private int direction = 1;
	private float passedDelayTime;
	private Rigidbody2D rigidBody2D;
	private PlatformerCharacter2D platformerCharacter2D;

	void Start() {
		//player = 
		platformerCharacter2D = player.GetComponent<PlatformerCharacter2D> ();
		passedDelayTime = delay;
	}

	void FixedUpdate() {
		if (platformerCharacter2D.isFacingRight() || !takeDirectionOfPlayer)
			direction = 1;
		else
			direction = -1;

		if (isEnabled) {
			passedDelayTime -= Time.fixedDeltaTime;

			if (spawningObject != null && objectsOnScreenCounter < maxCountOnScreen && passedDelayTime <= 0) {
				spawn ();

				objectsOnScreenCounter++;
				passedDelayTime = delay;
			}
		}
	}

	public GameObject spawn() {
		clonedObject = Instantiate (spawningObject);
		rigidBody2D = clonedObject.GetComponent<Rigidbody2D> ();
		clonedObject.transform.position = new Vector3 (direction * (player.transform.position.x + relativeSpawningPosition.x), player.transform.position.y + relativeSpawningPosition.y, 0);
		rigidBody2D.AddForce (new Vector2((float) direction * throwForce.x, throwForce.y));

		return clonedObject;
	}

	public void setEnabled(bool enabled) {
		isEnabled = enabled;
	}
}
