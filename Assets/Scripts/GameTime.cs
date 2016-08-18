using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameTime : MonoBehaviour {
	public float totalTime;
	public int second, minute, hour, timeRound;
	public bool gamePaused = false;
	public Text timeText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (gamePaused == false) {
			totalTime += Time.deltaTime;
		}
		timeRound = (int)Mathf.Round (totalTime);
		hour = (timeRound - (timeRound % 3600)) / 3600;
		minute = ((timeRound % 3600)-((timeRound % 3600) % 60))/60;
		second = ((timeRound % 3600) % 60);
		timeText.text = hour.ToString()+" Stunden "+ minute.ToString()+" Minuten "+ second.ToString()+" Sekunden Spielzeit";
	}
}
