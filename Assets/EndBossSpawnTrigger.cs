using UnityEngine;
using System.Collections;

public class EndBossSpawnTrigger : MonoBehaviour {

	public Vector3 spawn;
	public GameObject boss;

	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (triggered || other.gameObject.tag != "Player")
			return;

		triggered = true;

		GameObject spawnedBoss = Instantiate (boss);
		spawnedBoss.transform.position = spawn;

		/*EnemyController enemyController = spawnedBoss.GetComponent<EnemyController> ();
		enemyController.delayHit = 3;
		enemyController.delayJump = 1.5;
		enemyController.damage = 1;*/
	}
}
