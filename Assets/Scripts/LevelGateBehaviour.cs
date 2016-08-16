using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelGateBehaviour : MonoBehaviour {

	public string sceneName;

	void OnTriggerEnter2D(Collider2D other) {
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}
}
