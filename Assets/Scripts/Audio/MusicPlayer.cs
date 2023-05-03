// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string CurrentMusic;
    public float MusicEndsIn = 99999.0f;
    public AudioClip EnemyTransition;

    private AudioSource source;
    private AudioClip[] boss;
    private AudioClip[] enemyRoom;
    private AudioClip[] menu;
    private AudioClip[] peaceful;
    private bool transitioning = false;

    private void Start()
    {
        // Removes duplicates
        if (GameObject.FindGameObjectsWithTag("Music Player").Length != 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        boss = Resources.LoadAll<AudioClip>("Music/Boss");
        enemyRoom = Resources.LoadAll<AudioClip>("Music/EnemyRoom");
        menu = Resources.LoadAll<AudioClip>("Music/Menu");
        peaceful = Resources.LoadAll<AudioClip>("Music/Peaceful");
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            Play(CurrentMusic);
        }
        else
        {
            if (source.clip != null)
            {
               MusicEndsIn = source.clip.length - source.time; 
            }
        }

        if (MusicEndsIn <= 5.0f && !transitioning)
        {
            FadeInTransition(CurrentMusic, 5.0f);
        }
    }

    public void FadeInTransition(string music, float duration = 5.0f)
    {
        CurrentMusic = music;

        if (!transitioning)
        {
            StartCoroutine(IFadeInTransition(duration));
        }
    }

    public void SuddenTransition(string music, string sfxType)
    {
        AudioClip clip;

        switch (sfxType.ToLower())
        {
        case "enemy transition":
            clip = EnemyTransition;
            break;
        default:
            Debug.LogError(sfxType + " is a invalid music type");
            return;
        }

        CurrentMusic = music;

        if (!transitioning)
        {
            StartCoroutine(ISuddenTransition(clip));
        }
    }

    private IEnumerator IFadeInTransition(float duration)
    {
        float timePassed = 0.0f;
        transitioning = true;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            source.volume = 1.0f - (timePassed / duration);
            yield return null;
        }

        source.volume = 1f;
        Play(CurrentMusic);
        transitioning = false;
    }

    private IEnumerator ISuddenTransition(AudioClip clip)
    {
        transitioning = true;
        source.clip = clip;
        source.Play();

        while (source.isPlaying)
        {
            yield return null;
        }

        Play(CurrentMusic);
        transitioning = false;
    }

    private void Play(string musicType)
    {
        AudioClip[] music;

        switch (musicType.ToLower())
        {
        case "boss":
            music = boss;
            break;
        case "enemy room":
            music = enemyRoom;
            break;
        case "menu":
            music = menu;
            break;
        case "peaceful":
            music = peaceful;
            break;
        default:
            Debug.LogError(musicType + " is a invalid music type");
            return;
        }

        int random = Random.Range(0, music.Length);
        source.clip = music[random];
        source.Play();
    }
}
