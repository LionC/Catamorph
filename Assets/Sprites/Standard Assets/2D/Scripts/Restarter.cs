using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D {
	
    public class Restarter : MonoBehaviour {

		public float damageValue = 1f;
		
        private void OnTriggerEnter2D(Collider2D other) {
			if (other.tag == "Player") {
				CatBehaviour catBehavior = other.gameObject.GetComponent<CatBehaviour> ();
				catBehavior.takeDamage (damageValue);
				if(catBehavior.lives > 0)
					catBehavior.fallOutOfLevel ();
			}
        }
    }
}
