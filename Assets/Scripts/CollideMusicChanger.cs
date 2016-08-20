using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollideMusicChanger : MonoBehaviour {

    public List<string> tags;
    public MusicController.MusicType targetType;
    public MusicController controller;

    private MusicController.MusicType previousType;
    private bool triggered;

	// Use this for initialization
	void Start () {
        triggered = false;
        previousType = controller.currentMusicType;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("enter tag:" + other.tag);
        if (triggered || !VaildTag(other.tag)) return;

        triggered = true;
        previousType = controller.currentMusicType;
        Debug.Log("changing music from " + previousType + " to " + targetType);
        controller.TransitionToMusicType(targetType);
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("exit tag:" + other.tag);
        if (!triggered || !VaildTag(other.tag)) return;
        triggered = false;
        Debug.Log("changing music back from " + targetType + " to " + previousType);
        controller.TransitionToMusicType(previousType);
    }

    private bool VaildTag(string tag) {
        return tags.Contains(tag);
    }
}
