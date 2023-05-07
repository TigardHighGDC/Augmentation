// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer MainMixer;
    public AudioMixer BulletMixer;
    public AudioMixer MusicMixer;
    public AudioMixer SoundFXMixer;
    public AudioMixer EnemyMixer;

    public void SetVolume(float volume)
    {
        MainMixer.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float musicVolume)
    {
        MusicMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetBulletVolume(float bulletVolume)
    {
        BulletMixer.SetFloat("bulletVolume", bulletVolume);
    }

    public void SetSoundFXVolume(float soundFXVolume)
    {
        SoundFXMixer.SetFloat("soundFXVolume", soundFXVolume);
    }

    public void SetEnemyVolume(float enemyVolume)
    {
        SoundFXMixer.SetFloat("enemyVolume", enemyVolume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
