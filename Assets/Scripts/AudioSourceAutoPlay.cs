using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceAutoPlay : MonoBehaviour {
    public float endPrecision = 0.1f;

    public AudioSource audioSource;
    public List<AudioClip> clips;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (AtEndOfSong()) PlayRandomSong();
    }

    void PlayRandomSong() {
        int clipId = Random.Range(0, clips.Count);
        audioSource.clip = clips[clipId];
        audioSource.Play();
    }

    private bool AtEndOfSong() {
        if (audioSource.isPlaying) return false;
        if (audioSource.clip == null) return true;
        return audioSource.clip.length - audioSource.time <= endPrecision;
    }
}
