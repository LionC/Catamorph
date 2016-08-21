using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityStandardAssets._2D;
using System;

public class MusicController : MonoBehaviour {
    private static readonly long ticksToSeconds = TimeSpan.FromSeconds(1).Ticks;
    private static readonly string volumePostfix = "MusicVolume";

    public AudioMixerSnapshot defaultAudio;
    public AudioMixerSnapshot invertedAudio;

    public AudioMixer mixer;

    public GameObject player;

    public float muiscLowValue = -80f;
    public float musicHighValue = 0f;

    public float defaultMusicTransisionTime = 10f;
    public float invertedTransisionIn = 0f;
    public float invertedTransisionOut = 0f;

    public MusicType currentMusicType = MusicType.Default;

    public List<string> musicNames;

    private bool isMusicInverted;
    private PlatformerCharacter2D character;

    private Dictionary<MusicType, float> transisionSpeed;
    private Dictionary<MusicType, float> transisionStartFloats;
    private long transitionStart;
    private bool inTransition;

    private long transitionTicks;

    // Use this for initialization
    void Start () {
        SetInvertedMusic(false);
        character = player.GetComponent<PlatformerCharacter2D>();

        transitionStart = 0;
        inTransition = false;
        transisionSpeed = new Dictionary<MusicType, float>();
        transisionStartFloats = new Dictionary<MusicType, float>();

        transitionTicks = (long) defaultMusicTransisionTime * ticksToSeconds;

        TransitionToMusicType(currentMusicType, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        // if charecter movement and inverted music have different values, update inverted music
        if (character.inverted ^ isMusicInverted) SetInvertedMusic(character.inverted);

        long ticks = currentTicks();
        if (inTransition) {
            long ticksPassed = ticks - transitionStart;
            float progress = 1;

            if (transitionTicks != 0) progress = ((float)ticksPassed) / transitionTicks;

            if (1 <= progress) {
                progress = 1;
                inTransition = false;
            }


            foreach (MusicType currentType in Enum.GetValues(typeof(MusicType))) {
                SetVolume(currentType, EvaluateVolume(currentType, progress));
            }
        }
	}

    private float EvaluateVolume(MusicType type, float progress) {
        float speed = transisionSpeed[type];
        float start = transisionStartFloats[type];
        if (0 < speed)
            return start + speed * MapProgress(progress);
        else if (speed < 0)
            return start + speed * MapProgressInverted(progress);
        else
            return start;
    }

    private float MapProgressInverted(float progress) {
        return 1 - MapProgress(1 - progress);
    }

    private float MapProgress(float progress) {
        return (float) Math.Log10(progress * 9 + 1);
    }

    public void SetInvertedMusic (bool state) {
        isMusicInverted = state;
        if (state) invertedAudio.TransitionTo(invertedTransisionIn);
        else defaultAudio.TransitionTo(invertedTransisionOut);
    }

    public void TransitionToMusicType(MusicType type) {
        TransitionToMusicType(type, defaultMusicTransisionTime);
    }

    public void TransitionToMusicType(MusicType type, float transitionTime) {
        transitionStart = currentTicks();
        currentMusicType = type;
        inTransition = true;

        transitionTicks = (long) transitionTime * ticksToSeconds;

        foreach (MusicType currentType in Enum.GetValues(typeof(MusicType))) {
            float targetVolume; 
            float value = GetVolume(currentType);

            if (currentType.Equals(type)) targetVolume = musicHighValue;
            else targetVolume = muiscLowValue;
          
            transisionSpeed[currentType] = targetVolume - value;
            transisionStartFloats[currentType] = value;
        }
    }

    private float GetVolume(MusicType type) {
        float value;
        mixer.GetFloat(TypeVolumeName(type), out value);
        return value;
    }

    private void SetVolume(MusicType type, float value){
        mixer.SetFloat(TypeVolumeName(type), value);
    }

    private long currentTicks() {
        return DateTime.Now.Ticks;
    }

    private string TypeName(MusicType type) {
        return Enum.GetName(typeof(MusicType), type);
    }

    private string TypeVolumeName(MusicType type) {
        return TypeName(type) + volumePostfix;
    }

    public enum MusicType  {
        Default = 1,
        Action = 2
    };
}
