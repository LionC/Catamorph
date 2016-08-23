using UnityEngine;
using System.Collections;

public class LaserPointerController : MonoBehaviour {

	public float maxY = 2f;  //Maximum y-coordinate
	public float minY = 2f;  //Minimum y-coordinate
	public float ditherMaxRangeX = 40f;  //Maximum dither range on x-axis (in px)
	public float movingPerSecond = 100f;  //Movement pace
	public float pixelOffsetX = 100f;  //Offset position on x-axis (in px, from right)
	private GameObject player;  //Reference to Cat GameObject
	private Vector3 screenPosition;  //Current screenPosition
	private int directionX = 1;  //Current direction on x-axis
	private int directionY = 1;  //Current direction on y-axis
	private float ditherRangeX = 0;  //Current dither range on x-axis
	private float defaultX = 0;  //Default x-coordinate


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		defaultX = Camera.main.pixelWidth - pixelOffsetX;  //Calculation of default x-coordinate
		screenPosition = new Vector3 (defaultX, player.transform.position.y, (-1) * Camera.main.transform.position.z);  //Set default screenPosition
	}

	void FixedUpdate() {
		if (screenPosition.x <= defaultX - (ditherRangeX / 2)) {  //Farther left than intended
			directionX = 1;  //Change moving direction to right
			ditherRangeX = Random.Range (0, ditherMaxRangeX);  //Randomize dither range
		} 
		else if (screenPosition.x >= defaultX + (ditherRangeX / 2)) {  //Farther right than intended
			directionX = -1;  //Change moving direction to left
			ditherRangeX = Random.Range (0, ditherMaxRangeX);  //Randomize dither range
		}

		if (gameObject.transform.position.y <= player.transform.position.y - minY)  //Farther down than intended
			directionY = 1;  //Change moving direction to up
		else if (gameObject.transform.position.y >= player.transform.position.y + maxY)  //Farther up than intended
			directionY = -1;  //Change moving direction to down

		screenPosition.x += directionX * (movingPerSecond / 50);  //Change x-coordinate 
		screenPosition.y += directionY * (movingPerSecond / 50);  //Change y-coordinate
		gameObject.transform.position = Camera.main.ScreenToWorldPoint (screenPosition);  //Apply screenPosition to worldCoordinates
	}
}
