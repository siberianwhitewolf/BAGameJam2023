using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    private float volume = 1f;
    MusicManager musicManager;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        musicManager.CallRefreshVolume();
    }

    public float GetVolume()
    {
        return volume;
    }

    public void UpdateMusicManager()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }
}
