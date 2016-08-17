using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public int hp=1;
	public bool isdestroyable = true;
	public string DestroyableBye="";
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		if (hp <= 0)
			Destroy (gameObject);
	}
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		Bullet shot =
			otherCollider.gameObject.GetComponent<Bullet> ();
		if (otherCollider.CompareTag(DestroyableBye)) {
			if (shot.isEnemyShot = isdestroyable) {
				Damage (shot.damage);
				Destroy (shot.gameObject);
			}
		}
	}
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
