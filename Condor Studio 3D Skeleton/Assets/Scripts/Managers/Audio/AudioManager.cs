
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    [Space]
    [Header("The first element in the array will play at startup if true")]
    [Space]

    public bool wantStartupMusic;
    public Sound[] sounds;
    private Sound startupSound;

    public static AudioManager instance;
    public AudioSource audioSource;


    public float maxVolume = 1f;
    public float minVolume = 0f;

    private IEnumerator fadeIn;
    private IEnumerator fadeOut;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);        


        foreach (Sound sound in sounds) 
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        if (sounds[0] != null && wantStartupMusic)
        {
        startupSound = sounds[0];
        Play(startupSound.name);
        }
    }

    public void Play(string name)
    {
        Sound soundToPlay = Array.Find(sounds , sound => sound.name == name);
        if (soundToPlay == null)
        {
            Debug.LogWarning("Sound not found.");
        }
        else
        {
            soundToPlay.source.Play();
        }
    }

    IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float timer = 0f;
        float currentVolume = audioSource.volume;
        float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            var newVolume = Mathf.Lerp(currentVolume, targetValue, timer / duration);
            audioSource.volume = newVolume;
            yield return null;
        }

    }
    IEnumerator FadeOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float timer = 0f;
        float currentVolume = audioSource.volume;
        float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume);

        while (audioSource.volume > 0)
        {
            timer += Time.deltaTime;
            var newVolume = Mathf.Lerp(currentVolume, targetValue, timer / duration);
            audioSource.volume = newVolume;
            yield return null;
        }

    }





}




