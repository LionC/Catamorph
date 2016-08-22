using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityStandardAssets._2D;
using System;

/// <summary>
/// Enables music effects for inverted controlles and has functions to change
/// the music type curently playing.
/// 
/// <see cref="invertedAudio"/> is the <see cref="AudioMixerSnapshot"/> to transition
/// to with inverted controlles.
/// 
/// <see cref="defaultAudio"/> is the <see cref="AudioMixerSnapshot"/> to transition
/// to without inverted controlles.
/// 
/// <see cref="mainMixer"/> is the <see cref="AudioMixer"/> to control.
/// 
/// <see cref="player"/> is the <see cref="GameObject"/> representing the player. Used
/// to detect whether the controls are inverted.
/// 
/// <see cref="musicLowValue"/> and <see cref="musicHighValue"/> are the volumes
/// to set en- and disabled <see cref="MusicType"/> to.
/// 
/// <see cref="defaultMusicTransisionTime"/> is the time in seconds to transition
/// between two <see cref="MusicType"/> if no other time is given .
/// 
/// <see cref="invertedTransisionIn"/> and <see cref="invertedTransisionOut"/> are 
/// the times in seconds to transiton to and from the <see cref="invertedAudio"/>
/// <see cref="AudioMixerSnapshot"/>.
/// 
/// <see cref="currentMusicType"/> is the <see cref="MusicType"/> to start with.
/// </summary>
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
    public float musicHighValue = 0f;
    public float musicLowValue = -80f;

    // transition time to switch between music types
    public float defaultMusicTransisionTime = 10f;

    // transition time to switch from and to inverted music
    public float invertedTransisionIn = 0f;
    public float invertedTransisionOut = 0f;

    // starting music type
    public MusicType currentMusicType = MusicType.Default;

    // flag to keep track whether music is inverted
    private bool isMusicInverted;
    // character to check whether controles are inverted
    private PlatformerCharacter2D character;

    // variables to store data about music type transitions
    private Dictionary<MusicType, float> transisionSpeed;
    private Dictionary<MusicType, float> transisionStartFloats;
    private long transitionStart;
    private bool inTransition;
    private long transitionTicks;

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

    /// <summary>
    /// Calculates the volume to set the <see cref="MusicType"/> <paramref name="type"/>
    /// to during a transition where <paramref name="progress"/> os the fraction of the
    /// transition which has passed
    /// </summary>
    /// <param name="type">The <see cref="MusicType"/> whose volume is to be calculated</param>
    /// <param name="progress">The fraction of the transition which has passed</param>
    /// <returns></returns>
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

    /// <summary>
    /// Maps the progress like <see cref="MapProgress(float)"/> but
    /// mirrors it at (0.5, 0.5) (Used for negatve speeds).
    /// </summary>
    /// <param name="progress">The progress to map</param>
    /// <returns>The mapped progress</returns>
    private float MapProgressInverted(float progress) {
        // reverse progress mapping (used for negative speeds)
        return 1 - MapProgress(1 - progress);
    }

    /// <summary>
    /// Maps the linear <paramref name="progress"/> between 0 and 1 to a logarithmic one.
    /// </summary>
    /// <param name="progress">The progress to map</param>
    /// <returns>The mapped progress</returns>
    private float MapProgress(float progress) {
        // map linear progress between to logarithmic progress between 0 and 1
        return (float) Math.Log10(progress * 9 + 1);
    }

    /// <summary>
    /// Transitions to <see cref="defaultAudio"/> (false) or <see cref="invertedAudio"/> 
    /// (true) according to <paramref name="state"/>.
    /// </summary>
    /// <param name="state">To which <see cref="AudioMixerSnapshot"/> to transition to</param>
    public void SetInvertedMusic (bool state) {
        // updated inverted state
        isMusicInverted = state;
        
        // trasition to snapshot
        if (state) invertedAudio.TransitionTo(invertedTransisionIn);
        else defaultAudio.TransitionTo(invertedTransisionOut);
    }

    /// <summary>
    /// Transitions to <paramref name="type"/> with the 
    /// <see cref="defaultMusicTransisionTime"/> as transition time.
    /// </summary>
    /// <param name="type">The <see cref="MusicType"/> to transition to</param>
    public void TransitionToMusicType(MusicType type) {
        // fuction overload using default transition time
        TransitionToMusicType(type, defaultMusicTransisionTime);
    }

    /// <summary>
    /// Transitions to <paramref name="type"/>. It will take 
    /// <paramref name="tansitionTime"/> seconds to complete the transition.
    /// </summary>
    /// <param name="targetType">The <see cref="MusicType"/> to transition to</param>
    /// <param name="transitionTime">The number of seconds the transition should take</param>
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
            else targetVolume = musicLowValue;
          
            // store start value and to change for current music type
            transisionSpeed[currentType] = targetVolume - value;
            transisionStartFloats[currentType] = value;
        }
    }

    /// <summary>
    /// A short-cut function to read the volume for <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The <see cref="MusicType"/> whose volume to return</param>
    /// <returns><paramref name="type"/> current volume</returns>
    private float GetVolume(MusicType type) {
        float value;
        mainMixer.GetFloat(TypeVolumeName(type), out value);
        return value;
    }

    /// <summary>
    /// A short-cut function to set the volume for <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The <see cref="MusicType"/> whose volume to set</param>
    /// <param name="value">The volume to set <see cref="MusicType"/> to</param>
    private void SetVolume(MusicType type, float value){
        mainMixer.SetFloat(TypeVolumeName(type), value);
    }

    /// <summary>
    /// Retruns the current time in ticks (Used for timing).
    /// </summary>
    /// <returns>The current time in ticks</returns>
    private long currentTicks() {
        return DateTime.Now.Ticks;
    }

    /// <summary>
    /// Short-cut functon to get the name of <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The <see cref="MusicType"/> whose name to retrun</param>
    /// <returns>The name of <paramref name="type"/></returns>
    private string TypeName(MusicType type) {
        return Enum.GetName(typeof(MusicType), type);
    }

    /// <summary>
    /// Short-cut function to get the name of <paramref name="type"/> with 
    /// <see cref="volumePostfix"/> appended.
    /// </summary>
    /// <param name="type">The <see cref="MusicType"/> whose name to use</param>
    /// <returns><paramref name="type"/> name with <see cref="volumePostfix"/> appended</returns>
    private string TypeVolumeName(MusicType type) {
        return TypeName(type) + volumePostfix;
    }

    // diffenet music types 
    /// <summary>
    /// enum of the different music types (the name is the beginning of 
    /// the music sub-group to control)
    /// </summary>
    public enum MusicType  {
        Default = 1,
        Action = 2
    };
}
