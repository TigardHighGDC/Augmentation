using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour
{
    public string musicType;

    MusicPlayer musicPlayer;

    private void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("Music Player").GetComponent<MusicPlayer>();

        if (musicPlayer.CurrentMusic != musicType)
        {
            musicPlayer.FadeInTransition(musicType);
        }
    }
}
