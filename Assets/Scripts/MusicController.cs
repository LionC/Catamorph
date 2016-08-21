using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityStandardAssets._2D;
using System;

public class MusicController : MonoBehaviour {
    private static readonly long ticksToSeconds = TimeSpan.FromSeconds(1).Ticks;
    // post-fix of music sub-group volume expose names
    private static readonly string volumePostfix = "MusicVolume";

    // music snapshots to use with and without inverted controlles
    public AudioMixerSnapshot defaultAudio;
    public AudioMixerSnapshot invertedAudio;

    // main audio mixer
    public AudioMixer mainMixer;
    public GameObject player;

    // music volumes for music sub-groups while en- and disabled
    public float muiscLowValue = -80f;
    public float musicHighValue = 0f;

    // transition time to switch between music types
    public float defaultMusicTransisionTime = 10f;

    // transition time to switch from and to inverted music
    public float invertedTransisionIn = 0f;
    public float invertedTransisionOut = 0f;

    // starting music type
    public MusicType currentMusicType = MusicType.Default;

    private bool isMusicInverted;
    private PlatformerCharacter2D character;

    private Dictionary<MusicType, float> transisionSpeed;
    private Dictionary<MusicType, float> transisionStartFloats;
    private long transitionStart;
    private bool inTransition;

    private long transitionTicks;

    // Use this for initialization
    void Start () {
        // ensure music is not inverted
        SetInvertedMusic(false);
        // find character to check if controles are inverted
        character = player.GetComponent<PlatformerCharacter2D>();

        // setup music transition variables
        transitionStart = 0;
        inTransition = false;
        transisionSpeed = new Dictionary<MusicType, float>();
        transisionStartFloats = new Dictionary<MusicType, float>();

        transitionTicks = (long) defaultMusicTransisionTime * ticksToSeconds;

        // ensure only starting music type is playing
        TransitionToMusicType(currentMusicType, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        // if charecter movement and inverted music have different values, update inverted music
        if (character.inverted ^ isMusicInverted) SetInvertedMusic(character.inverted);

        if (inTransition) {
            // calculate transition progress
            long ticks = currentTicks();
            long ticksPassed = ticks - transitionStart;
            float progress = 1;

            // calculate fraction of ticks passed
            if (transitionTicks != 0) progress = ((float)ticksPassed) / transitionTicks;
            
            // ensure progess is not greate than one and finish transition if it is
            if (1 <= progress) {
                progress = 1;
                inTransition = false;
            }

            // update all values for all music types
            foreach (MusicType currentType in Enum.GetValues(typeof(MusicType))) {
                SetVolume(currentType, EvaluateVolume(currentType, progress));
            }
        }
	}

    private float EvaluateVolume(MusicType type, float progress) {
        // get speed and start volume for given type
        float speed = transisionSpeed[type];
        float start = transisionStartFloats[type];

        // calculate volume
        if (0 < speed)
            return start + speed * MapProgress(progress);
        else if (speed < 0)
            return start + speed * MapProgressInverted(progress);
        else
            return start;
    }

    private float MapProgressInverted(float progress) {
        // reverse progress mapping (used for negative speeds)
        return 1 - MapProgress(1 - progress);
    }

    private float MapProgress(float progress) {
        // map linear progress between to logarithmic progress between 0 and 1
        return (float) Math.Log10(progress * 9 + 1);
    }

    public void SetInvertedMusic (bool state) {
        // updated inverted state
        isMusicInverted = state;
        
        // trasition to snapshot
        if (state) invertedAudio.TransitionTo(invertedTransisionIn);
        else defaultAudio.TransitionTo(invertedTransisionOut);
    }

    public void TransitionToMusicType(MusicType type) {
        // fuction overload using default transition time
        TransitionToMusicType(type, defaultMusicTransisionTime);
    }

    public void TransitionToMusicType(MusicType targetType, float transitionTime) {
        // set transtition variables
        transitionStart = currentTicks();
        currentMusicType = targetType;
        inTransition = true;

        // calculate time transition will take
        transitionTicks = (long) transitionTime * ticksToSeconds;

        foreach (MusicType currentType in Enum.GetValues(typeof(MusicType))) {
            // get volume of current music type
            float value = GetVolume(currentType);

            // get target volume depenting on target music type
            float targetVolume;
            if (currentType.Equals(targetType)) targetVolume = musicHighValue;
            else targetVolume = muiscLowValue;
          
            // store start value and to change for current music type
            transisionSpeed[currentType] = targetVolume - value;
            transisionStartFloats[currentType] = value;
        }
    }

    // short cut function to read volume for a given music type
    private float GetVolume(MusicType type) {
        float value;
        mainMixer.GetFloat(TypeVolumeName(type), out value);
        return value;
    }

    // short cut function to write volume for a given music type
    private void SetVolume(MusicType type, float value){
        mainMixer.SetFloat(TypeVolumeName(type), value);
    }

    // get current time in ticks (for timing)
    private long currentTicks() {
        return DateTime.Now.Ticks;
    }

    // short cut function to get name of a music type
    private string TypeName(MusicType type) {
        return Enum.GetName(typeof(MusicType), type);
    }

    // short cut function to get name of a music type with post-fix
    private string TypeVolumeName(MusicType type) {
        return TypeName(type) + volumePostfix;
    }

    // diffenet music types (name beginnings of music sub-groups)
    public enum MusicType  {
        Default = 1,
        Action = 2
    };
}
