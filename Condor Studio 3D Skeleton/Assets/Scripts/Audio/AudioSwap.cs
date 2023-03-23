using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwap : MonoBehaviour
{
    MusicManager myMC_;
    int _lastTrackInQueue;
    
    private void Start()
    {
        myMC_ = FindObjectOfType<MusicManager>();
    }

    public void ChangeTrack(int trackNumber)
    {
        if (myMC_.currentSong != trackNumber)
        {
            _lastTrackInQueue = myMC_.lastPlaylistSong;
            myMC_.SwapTrack(trackNumber);
        }
    }

    public void ReturnToReproductionList()
    {
        myMC_.SwapTrack(_lastTrackInQueue);
    }
}


