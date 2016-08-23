using UnityEngine;
using System.Collections;

public class LaserPointerController : MonoBehaviour {
	public float maxY = 2f;
	public float minY = 2f;
	public float ditherMaxRangeX = 40f;  //Maximum dither range on x-axis (in px)
	public float movingPerSecond = 100f;  //Movement pace
	public float pixelOffsetX = 100f;  //Offset position on x-axis (in px, from right)
	private int directionX = 1;  //Current direction on x-axis
	private int directionY = 1;  //Current direction on y-axis
	private float ditherRangeX = 0;  //Current dither range on x-axis
	private float defaultX = 0;  //Default x-coordinate


	void Start () {
		defaultX = Camera.main.pixelWidth - pixelOffsetX;  //Calculation of default x-coordinate
	}

	void FixedUpdate() {
		if (transform.position.x <= defaultX - (ditherRangeX / 2)) {  //Farther left than intended
			directionX = 1;  //Change moving direction to right
			ditherRangeX = Random.Range (0, ditherMaxRangeX);  //Randomize dither range
		} 
		else if (transform.position.x >= defaultX + (ditherRangeX / 2)) {  //Farther right than intended
			directionX = -1;  //Change moving direction to left
			ditherRangeX = Random.Range (0, ditherMaxRangeX);  //Randomize dither range
		}

		if (transform.position.y < minY || transform.position.y > Camera.main.pixelHeight - maxY) {
			directionY *= -1;
		}

		transform.position = new Vector3 (transform.position.x + directionX * (movingPerSecond / 50), transform.position.y + directionY * (movingPerSecond / 50), 0);
	}
}
