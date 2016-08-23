using UnityEngine;
using System.Collections;

public class ObjectHealthController : MonoBehaviour {
	
	public float lives = 1.0f;  //Amount of lives 
	public bool isDestroyable = true;  //Whether the object is destroyable or not


	public void damage(float damageCount) {
		lives -= damageCount;  //Reduce lives by taken damage
		if (lives <= 0)  //No lives any more
			Destroy (gameObject);  //Destroy object
	}

	void OnCollisionEnter2D(Collision2D other) {
		RocketController shot = other.collider.gameObject.GetComponent<RocketController> ();  //Initialization of rocketController

		if (other.gameObject.tag == "Rocket") {  //Collision by rocket
			
			if (isDestroyable)  //Object is destroyable
				damage (shot.damage);  //Taking damage
		}
	}
}
