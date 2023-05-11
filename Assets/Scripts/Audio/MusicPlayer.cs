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

    private void Awake()
    {
        // Removes duplicates
        if (GameObject.FindGameObjectsWithTag("Music Player").Length != 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        boss = Resources.LoadAll<AudioClip>("Music/BossMusic");
        enemyRoom = Resources.LoadAll<AudioClip>("Music/EnemyRoom");
        menu = Resources.LoadAll<AudioClip>("Music/Menu");
        peaceful = Resources.LoadAll<AudioClip>("Music/Peaceful");
    }

    private void Update()
    {
        if (CurrentMusic.ToLower() == "boss")
        {
            if (BossPhaseManager.CurrentPhase != BossPhaseManager.PreviousPhase)
            {
                FadeInTransition(CurrentMusic);
            } 
            return;
        }
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
            FadeInTransition(CurrentMusic);
        }
    }

    public void FadeInTransition(string music, float duration = 2.5f)
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

        source.volume = 1.0f;
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
        case "enemy room":
            music = enemyRoom;
            break;
        case "menu":
            music = menu;
            break;
        case "peaceful":
            music = peaceful;
            break;
        case "boss":
            source.clip = boss[SelectBossMusic()];
            source.Play();
            return;
        default:
            Debug.LogError(musicType + " is a invalid music type");
            return;
        }

        int random = Random.Range(0, music.Length);
        source.clip = music[random];
        source.Play();
    }

    private int SelectBossMusic()
    {
        switch (BossPhaseManager.CurrentPhase)
        {
            case BossPhaseManager.Phases.Ignoring:
                return 0;
            case BossPhaseManager.Phases.Denial:   
                return 1;
            case BossPhaseManager.Phases.Anger:
                return 2;
            case BossPhaseManager.Phases.Bargaining:
                return 3;
            case BossPhaseManager.Phases.Depression:
                return 4;
            case BossPhaseManager.Phases.Acceptance:
                return 5;
            default:
                Debug.LogError("Invalid phase");
                return 0;
        }
    }

}
