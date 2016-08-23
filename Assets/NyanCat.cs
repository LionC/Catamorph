using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class NyanCat : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	}


	public bool play = false;
	public bool started = false;


	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (!started && play) {
			started = true;
			AudioSource audio = GetComponent<AudioSource> ();
			audio.enabled = true;
			audio.Play ();
		}
		if (play) {
			var position = this.transform.position;
			position.x += 0.2f;
			this.transform.position = position;
		}
	}
}
