using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityStandardAssets._2D;

public class MusicController : MonoBehaviour { 

    public AudioMixerSnapshot defaultAudio;
    public AudioMixerSnapshot invertedAudio;
    public AudioClip[] tracks;
    public AudioSource audioSource;

    public GameObject player;

    public float invertedTransisionIn = 1f;
    public float invertedTransisionOut = 1f;

    private bool isMusicInverted;
    private PlatformerCharacter2D character;


    // Use this for initialization
    void Start () {
        defaultAudio.TransitionTo(0);
        character = player.GetComponent<PlatformerCharacter2D>();
        playRandomSong();
    }
	
	// Update is called once per frame
	void Update () {
        // if charecter movement and inverted music have different values, change inverted music
        if (character.inverted ^ isMusicInverted) setInvertedMusic(!isMusicInverted);
        if (!audioSource.isPlaying) playRandomSong();
	}

    void setInvertedMusic (bool state) {
        isMusicInverted = state;
        if (state) invertedAudio.TransitionTo(invertedTransisionIn);
        else defaultAudio.TransitionTo(invertedTransisionOut);
    }

    void playRandomSong() {
        int song = Random.Range(0, tracks.Length);
        audioSource.clip = tracks[song];
        audioSource.Play();
    }
}
