using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Plays a sound when a trigger with a given tag enters or exits.
/// 
/// <see cref="disable"/> is a flag storing wheter the <see cref="audioSource"/>
/// is muted.
/// 
/// <see cref="cooldownSeconds"/> is the number of seconds that must pass after
/// playing a sound before the next one is played.
/// 
/// <see cref="audioSource"/> is the <see cref="AudioSource"/> to use
/// to play the music.
/// 
/// <see cref="targetTags"/> is a list of tags to trigger specific sounds.
/// <see cref="enterSounds"/> and <see cref="exitSounds"/> are lists of 
/// <see cref="AudioSource"/> to be played once a trigger enters
/// or exit with the tag in the same position in <see cref="targetTags"/>
/// </summary>
public class SoundOnCollide : MonoBehaviour {
    private static readonly long cdToTicks = TimeSpan.FromSeconds(1).Ticks;

    // bool storing whether audio source is muted
    public bool disable;
    // seconds to wait between 2 sounds
    public float cooldownSeconds;
    // audio source to play clips
    public AudioSource audioSource;

    // list of tags of triggering objects to play clips for
    public List<string> targetTags;
    // list of clips for trigger enter (same order as tags)
    public List<AudioClip> enterSounds;
    // list of clips for trigger exit (same order as tags)
    public List<AudioClip> exitSounds;

    // long to store time of last start to enforce cooldown between sound effects
    private long lastStart;

    // Use this for initialization
    void Start () {
        // initalize lastStart
        lastStart = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        // get enter clip
        AudioClip clip = GetAudioClipForTag(enterSounds, other.tag);
        // play clip if tag is valid (meaning clip is not null)
        if (clip != null) playClip(clip);
    }

    void OnTriggerExit2D(Collider2D other) {
        // get enter clip
        AudioClip clip = GetAudioClipForTag(exitSounds, other.tag);
        // play clip if tag is valid (meaning clip is not null)
        if (clip != null) playClip(clip);
    }

    /// <summary>
    /// Plays <paramref name="clip"/> on <see cref="audioSource"/> if the cooldown 
    /// has passed.
    /// </summary>
    /// <param name="clip">The <see cref="AudioClip"/> to play (must not be null)</param>
    /// <returns>Whether the clip was acutally played.</returns>
    private bool playClip(AudioClip clip) {
        // throw error if clip is null (easier error analysis
        if (clip == null) throw new ArgumentNullException("Clip must not be null!");

        // get current ticks
        long now = DateTime.Now.Ticks;
        // check if cooldown of last play is passed (return false if cd is still active)
        if (now - lastStart < cooldownSeconds * cdToTicks) return false;

        // stop current track if still playing
        if (audioSource.isPlaying) audioSource.Stop();

        // load and play clip
        audioSource.clip = clip;
        audioSource.Play();

        // store track start time to enforce cooldown and return true
        lastStart = now;
        return true;
    }
    
    /// <summary>
    /// Gets the position of <paramref name="tag"/> in <see cref="targetTags"/>
    /// </summary>
    /// <param name="tag">The tag to find</param>
    /// <returns><paramref name="tag"/> position in <see cref="targetTags"/> or -1 if 
    /// the tag is not included in <see cref="targetTags"/></returns>
    private int GetTagId(string tag) {
        return targetTags.IndexOf(tag);
    }

    // get audio clip from clips with the same index as the given tag in the tags list 
    // retruns null if tag is not included in the tags list
    private AudioClip GetAudioClipForTag(List<AudioClip> clips, string tag) {
        return targetTags.Contains(tag) ? clips[GetTagId(tag)] : null;
    }

    /// <summary>
    /// (Un-)Mutes <see cref="audioSource"/> and sets the disable tag accordingly.
    /// </summary>
    /// <param name="disable">Whether to mute or unmute the <see cref="audioSource"/></param>
    void setDisable(bool disable) {
        audioSource.mute = disable;
        this.disable = disable;
    }
}
