using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceAutoPlay : MonoBehaviour {
    // how close the track position has to be to the track length to
    // consider a track as at its end
    public float endPrecision = 0.1f;

    // audio source to play tracks on
    public AudioSource audioSource;
    // list of clips to loop through
    public List<AudioClip> clips;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // start a new song if the current song is at the end
        if (AtEndOfSong()) PlayRandomSong();
    }

    void PlayRandomSong() {
        // get a random index for the clip list
        int clipId = Random.Range(0, clips.Count);
        // load and play the clip with the randomly generated index
        audioSource.clip = clips[clipId];
        audioSource.Play();
    }

    // check if audioSource is at the end of a song
    private bool AtEndOfSong() {
        // check if audio source is playing
        if (audioSource.isPlaying) return false;
        // check if audio source has a clip loaded
        if (audioSource.clip == null) return true;
        // check if track length is equal to current position in track
        // (with the offset of endPrecision)
        return audioSource.clip.length - audioSource.time <= endPrecision;
    }
}
