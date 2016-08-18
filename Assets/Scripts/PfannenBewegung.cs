using UnityEngine;
using System.Collections;

public class PfannenBewegung : MonoBehaviour {
	public float timeLeft=0f;
	private Rigidbody2D rigidbodyComponent;
	private CircleCollider2D CircleCollider2DVar;
	// Use this for initialization
	void Start () {
		rigidbodyComponent = GetComponent<Rigidbody2D>();
		CircleCollider2DVar = GetComponentInParent<CircleCollider2D>();
	}
	private void OnCollision2DEnter(CircleCollider2D other) 
	{
		if (other.GetComponent<Collider>().tag=="player")
		{
			;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		timeLeft += Time.deltaTime;
		rigidbodyComponent = GetComponent<Rigidbody2D>();
		rigidbodyComponent.rotation = Mathf.Sin ((timeLeft) * (Mathf.PI / 180)*100)*8;
		}
}
