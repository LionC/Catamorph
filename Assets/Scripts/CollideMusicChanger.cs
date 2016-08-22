using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Changes the music type to <see cref="targetType"/> while at least one 
/// element with one of <see cref="tags"/> as tag is inside the trigger.
/// 
/// <see cref="controller"/> is the music controller whose music type is to be changed
/// </summary>
public class CollideMusicChanger : MonoBehaviour {

    // list of tags to change music
    public List<string> tags;
    // music type to change to during trigger
    public MusicController.MusicType targetType;
    // main music controller music change commands are sent to
    public MusicController controller;

    // variable to keep track of music type to return to
    private MusicController.MusicType previousType;
    // counter to keep track who many elements are inside
    private int triggered;

	// Use this for initialization
	void Start () {
        // initalize triggered with 0 and previousType with the music type currently playing
        triggered = 0;
        previousType = controller.currentMusicType;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        // ensure triggred is true and the tag is valid
        if (!VaildTag(other.tag)) return;

        // set triggered
        triggered += 1;
        // check that it is the first element that entered
        if (triggered == 1) return;
        // store music type currently playing
        previousType = controller.currentMusicType;
        // make controller change the music type to the target type
        controller.TransitionToMusicType(targetType);
    }

    void OnTriggerExit2D(Collider2D other) {
        // ensure triggred is false and the tag is valid
        if (!VaildTag(other.tag)) return;

        // clear triggerd
        triggered -= 1;
        // check that element was the last one insde
        if (0 < triggered) return;
        // make controller change the music type to the previouse type
        controller.TransitionToMusicType(previousType);
    }

    /// <summary>
    /// Checks if <paramref name="tag"/> is in the list of triggering tags
    /// </summary>
    /// <param name="tag">The tag to check</param>
    /// <returns>Whether the given tag is in <paramref name="tag"/></returns>
    private bool VaildTag(string tag) {
        return tags.Contains(tag);
    }
}
