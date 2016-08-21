using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollideMusicChanger : MonoBehaviour {

    // list of tags to change music
    public List<string> tags;
    // music type to change to during trigger
    public MusicController.MusicType targetType;
    // main music controller music change commands are sent to
    public MusicController controller;

    // variable to keep track of music type to return to
    private MusicController.MusicType previousType;
    // flag to keep track whether trigger is active or not
    private bool triggered;

	// Use this for initialization
	void Start () {
        // initalize triggered with false and previousType with the music type currently playing
        triggered = false;
        previousType = controller.currentMusicType;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        // ensure triggred is true and the tag is valid
        if (triggered || !VaildTag(other.tag)) return;

        // set triggered
        triggered = true;
        // store music type currently playing
        previousType = controller.currentMusicType;
        // make controller change the music type to the target type
        controller.TransitionToMusicType(targetType);
    }

    void OnTriggerExit2D(Collider2D other) {
        // ensure triggred is false and the tag is valid
        if (!triggered || !VaildTag(other.tag)) return;

        // clear triggerd
        triggered = false;
        // make controller change the music type to the previouse type
        controller.TransitionToMusicType(previousType);
    }

    private bool VaildTag(string tag) {
        return tags.Contains(tag);
    }
}
