using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;

    public AudioMixer bulletMixer;

    public AudioMixer musicMixer;

    public AudioMixer soundFXMixer;

    public AudioMixer enemyMixer;

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetBulletVolume(float bulletVolume)
    {
        bulletMixer.SetFloat("bulletVolume", bulletVolume);
    }

    public void SetSoundFXVolume(float soundFXVolume)
    {
        soundFXMixer.SetFloat("soundFXVolume", soundFXVolume);
    }

    public void SetEnemyVolume(float enemyVolume)
    {
        soundFXMixer.SetFloat("enemyVolume", enemyVolume);
    }



    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
