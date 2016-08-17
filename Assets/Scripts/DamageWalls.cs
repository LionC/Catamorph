using UnityEngine;
using System.Collections;

public class DamageWalls : MonoBehaviour {
	public GameObject player;
	public string PlayerName;
	public float TimeLeftHit;
	void Start () {
		TimeLeftHit = 5;
		PlayerName= "Catelyn";
		GameObject Cat = GameObject.Find (PlayerName);
		CatBehaviour CatBehaviour = Cat.GetComponent<CatBehaviour> ();
		CatBehaviour.CharacterType = CatBehaviour.CharacterType;
	}
	void FixedUpdate(){
		if (TimeLeftHit > 0) {
			TimeLeftHit -= Time.deltaTime;
		}
	}
	private void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.collider.tag == "Player") 
		{	if (TimeLeftHit <= 0) {
				Debug.Log (TimeLeftHit);
				GameObject Cat = GameObject.Find (PlayerName);
				CatBehaviour CatBehaviour = Cat.GetComponent<CatBehaviour> ();
				CatBehaviour.lives = CatBehaviour.Damage (CatBehaviour.lives);
				TimeLeftHit = 5;
				if (CatBehaviour.lives == 0)
					CatBehaviour.gameOver ();
			}
		}
	}
	void Update () {
		
	}
}
