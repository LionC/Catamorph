using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class ObjectSpawner : MonoBehaviour {

	public GameObject spawningObject;
	public int maxCountOnScreen = 1;
	public float coolDown = 0f;
	public bool takeDirectionOfPlayer = true;
	public Vector2 throwForce = new Vector2 (0, 0);
	public Vector2 relativeSpawningPosition = new Vector2(0, 0);

	private GameObject player;
	private GameObject clonedObject;
	private bool isEnabled = false;
	private int objectsOnScreenCounter = 0;
	private int direction = 1;
	private float passedCoolDown = 0f;
	private Rigidbody2D rigidBody2D;
	private PlatformerCharacter2D platformerCharacter2D;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		platformerCharacter2D = player.GetComponent<PlatformerCharacter2D> ();
	}

	void FixedUpdate() {
		if (platformerCharacter2D.isFacingRight() || !takeDirectionOfPlayer)
			direction = 1;
		else
			direction = -1;

		if (coolDown > 0) 
			passedCoolDown += Time.fixedDeltaTime;

		if (isEnabled)
			spawn ();
	}

	public GameObject spawn() {
		if (spawningObject != null && passedCoolDown >= coolDown && (objectsOnScreenCounter < maxCountOnScreen || maxCountOnScreen < 0)) {
			clonedObject = Instantiate (spawningObject);
			rigidBody2D = clonedObject.GetComponent<Rigidbody2D> ();
			clonedObject.transform.localScale = new Vector3 (direction * clonedObject.transform.localScale.x, clonedObject.transform.localScale.y, 0);
			clonedObject.transform.position = new Vector3 (player.transform.position.x + (direction * relativeSpawningPosition.x), player.transform.position.y + relativeSpawningPosition.y, 0);
			rigidBody2D.AddForce (new Vector2 ((float)direction * throwForce.x, throwForce.y));

			passedCoolDown = 0f;
			if(maxCountOnScreen >= 0)
				objectsOnScreenCounter++;

			return clonedObject;
		} else
			return null;
	}

	public void setEnabled(bool enabled) {
		isEnabled = enabled;
	}
}
