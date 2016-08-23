using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Changes the music type to <see cref="targetType"/> while at least one 
/// element with one of <see cref="tags"/> as tag is inside the trigger or collision
/// (as determined by <see cref="useTrigger"/> and <see cref="useCollision"/>).
/// 
/// <see cref="controller"/> is the music controller whose music type is to be changed.
/// 
/// <see cref="useTrigger"/> is a flag determinig if triggers are used.
/// <see cref="useCollision"/> is a flag determinig if collisions are used.
/// </summary>
public class CollideMusicChanger : MonoBehaviour {

    // list of tags to change music
    public List<string> tags;
    // music type to change to during trigger
    public MusicController.MusicType targetType;
    // main music controller music change commands are sent to
    public MusicController controller;

    // flags determining if triggering and/or collision is used
    public bool useTrigger;
    public bool useCollision;

    // variable to keep track of music type to return to
    private MusicController.MusicType previousType;
    // counter to keep track who many elements are inside
    private int triggered;

	void Start () {
        // initalize triggered with 0 and previousType with the music type currently playing
        triggered = 0;
        previousType = controller.currentMusicType;
    }

    void OnTriggerEnter2D(Collider2D other) {
        // ensure triggres are used and the tag is valid
        if (!useTrigger || !VaildTag(other.tag)) return;
      
        incrementTriggered();
    }

    void OnTriggerExit2D(Collider2D other) {
        // ensure triggres are used and the tag is valid
        if (!useTrigger || !VaildTag(other.tag)) return;

        decrementTriggered();
    }

    void OnCollisionEnter2D(Collision2D other) {
        // ensure collisions are used and the tag is valid
        if (!useCollision || !VaildTag(other.gameObject.tag)) return;

        incrementTriggered();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // ensure collisions are used and the tag is valid
        if (!useCollision || !VaildTag(other.gameObject.tag)) return;

        decrementTriggered();
    }

    /// <summary>
    /// Reduces <see cref="triggered"/> by 1 and retruns to <see cref="previousType"/>
    /// if <see cref="triggered"/> is 0.
    /// </summary>
    private void decrementTriggered() {
        // update triggerd
        triggered -= 1;
        // check that element was the last one insde
        if (0 < triggered) return;
        // make controller change the music type to the previouse type
        controller.TransitionToMusicType(previousType);
    }

    /// <summary>
    /// Increases <see cref="triggered"/> by 1 and changes to <see cref="targetType"/>
    /// if <see cref="triggered"/> was 0.
    /// </summary>
    private void incrementTriggered() {
        // update triggered
        triggered += 1;
        // check that it is the first element that entered
        if (triggered == 1) return;
        // store music type currently playing
        previousType = controller.currentMusicType;
        // make controller change the music type to the target type
        controller.TransitionToMusicType(targetType);
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
