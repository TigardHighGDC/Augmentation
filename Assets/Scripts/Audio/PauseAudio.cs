using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAudio : MonoBehaviour
{
    private void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (PauseMenu.GameIsPaused)
        {
            audio.Pause();
        }
        else
        {
            audio.Play();
        }
    }
}
