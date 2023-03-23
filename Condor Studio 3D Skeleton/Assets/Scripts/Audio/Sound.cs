
using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound 
{
    [HideInInspector] public int trackNumber;
    public string name;
    public AudioClip clip;
    [Range(0f,1f)] public float volume;
    [Range(0.1f, 3f)] public float pitch;
    public bool loop;
    public bool eventMusic;
   
    [HideInInspector]
    public AudioSource source;

    public void Stop()
    {
        source.Stop();
    }
}
