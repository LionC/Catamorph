using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float lives = 1;
	public bool isDestroyable = true;
	public string destroyableBy = "";
	public void damage(int damageCount) {
		lives -= damageCount;
		if (lives <= 0)
			Destroy (gameObject);
	}
	void OnCollisionEnter2D(Collision2D other) {
		Bullet shot = other.collider.gameObject.GetComponent<Bullet> ();
		if (isDestroyable) {
			if (other.collider.CompareTag(destroyableBy)) {
				damage (shot.damage);
				Destroy (gameObject);
			}
		}
	}
}
