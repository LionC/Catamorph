using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float lives = 1;
	public bool isdestroyable = true;
	public string destroyableBy;
	public void damage(int damageCount) {
		lives -= damageCount;
		if (lives <= 0)
			Destroy (gameObject);
	}
	void OnCollisionEnter2D(Collision2D other) {
		Bullet shot = other.collider.gameObject.GetComponent<Bullet> ();
		if (other.collider.CompareTag(destroyableBy)) {
			if (shot.isEnemyShot = isdestroyable) {
				damage (shot.damage);
				Destroy (shot.gameObject);
			}
		}
	}
}
