using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LiveBarController : MonoBehaviour {

	public Sprite liveFull;
	public Sprite liveEmpty;
	private CatBehaviour player;

	private List<GameObject> displayLives = new List<GameObject>();

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<CatBehaviour> ();

		foreach (Transform tChild in this.transform) {
			displayLives.Add (tChild.gameObject);
		}

		displayLives.Sort (delegate(GameObject a, GameObject b) {
			return a.transform.position.x.CompareTo (b.transform.position.x);
		});
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int lives = (int) player.getLives();
		int i = 0;
		foreach (GameObject live in displayLives) {
			Image image = live.GetComponent<Image> ();
			if (i < lives) {
				image.sprite = liveFull;
			} else {
				image.sprite = liveEmpty;
			}
			i++;
		}
	}
}
