using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets._2D;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundOnCollide : MonoBehaviour {
    private static readonly long cdToTicks = TimeSpan.FromSeconds(1).Ticks;

    public bool disable;
    public float cooldownSeconds;
    public AudioSource audioSource;
    public List<string> targetTags;
    public List<AudioClip> enterSounds;
    public List<AudioClip> exitSounds;

    private long lastStart;

    // Use this for initialization
    void Start () {
        lastStart = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        AudioClip clip = GetAudioClipForTag(enterSounds, other.tag);
        if (clip != null) playClip(clip);
    }

    void OnTriggerExit2D(Collider2D other) {
        AudioClip clip = GetAudioClipForTag(exitSounds, other.tag);
        if (clip != null) playClip(clip);
    }

    private bool playClip(AudioClip clip) {
        if (clip == null) throw new ArgumentNullException("Clip must not be null!");

        long now = DateTime.Now.Ticks;
        if (now - lastStart < cooldownSeconds * cdToTicks) return false;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
        lastStart = now;
        return true;
    }

    private int GetTagId(string tag) {
        return targetTags.IndexOf(tag);
    }

    private AudioClip GetAudioClipForTag(List<AudioClip> clips, string tag) {
        return targetTags.Contains(tag) ? clips[GetTagId(tag)] : null;
    }

    void setDisable(bool disable) {
        audioSource.mute = disable;
        this.disable = disable;
    }
}
