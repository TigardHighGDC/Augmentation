using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    public string MusicType;
    MusicPlayer musicPlayer;
    void Start()
    {
        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        if (musicPlayer.CurrentMusic != MusicType)
        {
            musicPlayer.FadeInTransition(MusicType);
        }

        Destroy(gameObject);
    }
}
