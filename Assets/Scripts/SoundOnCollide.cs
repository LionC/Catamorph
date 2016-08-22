using UnityEngine;
using System.Collections.Generic;
using System;

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

    // plays given clip and returns true, retruns false if track is not played
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

    // get the position of the tag in the tags list (-1 if tag not in list)
    private int GetTagId(string tag) {
        return targetTags.IndexOf(tag);
    }

    // get audio clip from clips with the same index as the given tag in the tags list 
    // retruns null if tag is not included in the tags list
    private AudioClip GetAudioClipForTag(List<AudioClip> clips, string tag) {
        return targetTags.Contains(tag) ? clips[GetTagId(tag)] : null;
    }

    // (un-)mute audio source and set disable tag accordingly
    void setDisable(bool disable) {
        audioSource.mute = disable;
        this.disable = disable;
    }
}
