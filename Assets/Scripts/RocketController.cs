using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	public float damage = 1.0f;  //Damage caused by Rocket
	public float livingTime = 20f;  //Time in sec, at which the rocket is destroyed at the latest
	private Animator animator;

		
	void Start () {
		animator = GetComponent<Animator>();
		DestroyRocket(20);  //Destroy rocket at the latest
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		animator.SetTrigger("Explode");
		DestroyRocket(0);  //Destroy rocket on collision
	}
	
	void DestroyRocket(float timeOut) {
		Destroy (gameObject, animator.GetCurrentAnimatorStateInfo(0).length + timeOut);
	}
}
