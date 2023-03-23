using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    [SerializeField] private bool _fading;
    [SerializeField] private float _timeToFade;
    private AudioSource audioSource;
    public Sound[] musicClips;
    public int currentSong;
    public int lastPlaylistSong;
    //private int _songListLenght;
    private bool _manualStopped;
    private bool _paused;
    private bool _isPlaying;
    private bool _notAltTab;
    //public int lastPlayedTrack;
    private GeneralManager generalManager;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        generalManager = FindObjectOfType<GeneralManager>();
        audioSource = GetComponent<AudioSource>();
        _manualStopped = false;
        currentSong = 0;
        audioSource.clip = musicClips[currentSong].clip;
        audioSource.volume = musicClips[currentSong].volume * generalManager.GetVolume();
        audioSource.pitch = musicClips[currentSong].pitch;
        audioSource.loop = musicClips[currentSong].loop;
        Resume();     
    }

    public void Update()
    {
        if (!audioSource.isPlaying && !_manualStopped && !musicClips[currentSong].loop && _notAltTab)
        {
            NextSong(currentSong);
            _notAltTab = false;
        }
    }

    public void CallRefreshVolume()
    {
        audioSource.volume = musicClips[currentSong].volume * generalManager.GetVolume();
    }

    public void Resume()
    {
        //lastPlayedTrack = currentSong;
        if (_paused)
        {
            audioSource.UnPause();
            return;
        }
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
        _paused = true;
        _manualStopped = true;
        _notAltTab = true;
    }

    public void Stop()
    {
        audioSource.Stop();
        _paused = false;
        _manualStopped = true;
        _notAltTab = true;
    }

    public void RestartSong()
    {
        Stop();
        Resume();
    }

    public void NextSong(int startingSongIndex)
    {
        var i = startingSongIndex;
        foreach (Sound track in musicClips)
        {
            if (!track.eventMusic)
            {
                SwapTrack(i);
                return;
            }
            i++;
        }
        if (i > musicClips.Length)
        {
            NextSong(0);
        }
    }

    public void SwapTrack(int trackNumber) 
    {
        if (!musicClips[currentSong].eventMusic) //Musica de eventos = Combate, menu, victoria, derrota, etc.
        {
            lastPlaylistSong = currentSong;
        }

        if (!_fading)
        {
            StartCoroutine(Fade(trackNumber));
            _manualStopped = false;
        }
    }

    public IEnumerator Fade(int trackNumber)
    {
        _fading = true;

        float startVolume = audioSource.volume;
        float endVolume = musicClips[trackNumber].volume * generalManager.GetVolume();

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / _timeToFade;
            yield return null;
        }

        Stop();

        audioSource.clip = musicClips[trackNumber].clip;
        audioSource.pitch = musicClips[trackNumber].pitch;
        audioSource.loop = musicClips[trackNumber].loop;

        currentSong = trackNumber;

        Resume();

        while (audioSource.volume < endVolume)
        {
            audioSource.volume += endVolume * Time.deltaTime / _timeToFade;
            yield return null;
        }

        _fading = false;
    }
}
