using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	public Text text;


	bool running = false;
	float timePassed = 0;

	void startClock(){
		running = true;
	
	}

	void stopClock(){
		running = false;
	}
	// Use this for initialization
	void Start () {
		startClock ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!running)
			return;

		timePassed += Time.fixedDeltaTime;
		
		var minutes = timePassed / 60;  
		var seconds = timePassed % 60;

		text.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
	}
}
