using UnityEngine;
using System.Collections;

public class LaserPointerController : MonoBehaviour {

	public float maxY = 2f;
	public float minY = 2f;
	public float ditherMaxRangeX = 40f;
	public float movingPerSecond = 100f;
	public float pixelOffsetX = 100f;
	private GameObject player;
	private Vector3 screenPosition;
	private int directionX = 1;
	private int directionY = 1;
	private float ditherRangeX = 0;
	private float defaultX = 0;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start () {
		defaultX = Camera.main.pixelWidth - pixelOffsetX;
		screenPosition = new Vector3 (defaultX, player.transform.position.y, (-1) * Camera.main.transform.position.z);
	}

	void FixedUpdate() {
		if (screenPosition.x <= defaultX - (ditherRangeX / 2)) {
			directionX = 1;
			ditherRangeX = Random.Range (0, ditherMaxRangeX);
		} 
		else if (screenPosition.x >= defaultX + (ditherRangeX / 2)) {
			directionX = -1;
			ditherRangeX = Random.Range (0, ditherMaxRangeX);
		}

		if (gameObject.transform.position.y <= player.transform.position.y - minY)
			directionY = 1;
		else if (gameObject.transform.position.y >= player.transform.position.y + maxY)
			directionY = -1;

		screenPosition.x += directionX * (movingPerSecond / 50);
		screenPosition.y += directionY * (movingPerSecond / 50);
		gameObject.transform.position = Camera.main.ScreenToWorldPoint (screenPosition);
	}
}
