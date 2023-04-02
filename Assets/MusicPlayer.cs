using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string CurrentMusic;
    public float MusicEndsIn = 99999f;
    
    private AudioSource source;
    private AudioClip[] boss;
    private AudioClip[] enemyRoom;
    private AudioClip[] menu;
    private AudioClip[] peaceful;
    private bool transitioning = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        boss = Resources.LoadAll<AudioClip>("Music/Boss");
        enemyRoom = Resources.LoadAll<AudioClip>("Music/EnemyRoom");
        menu = Resources.LoadAll<AudioClip>("Music/Menu");
        peaceful = Resources.LoadAll<AudioClip>("Music/Peaceful");
    }

    void Update()
    {
        Debug.Log(MusicEndsIn);
        if (!source.isPlaying)
        {
            Play(CurrentMusic);
        }
        else
        {
            MusicEndsIn = source.clip.length - source.time;
        }

        if (MusicEndsIn <= 5.0f && !transitioning)
        {
            StartCoroutine(FadeInTransition(5.0f));
        }
    }

    private IEnumerator FadeInTransition(float duration)
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

    private void Play(string musicType)
    {
        AudioClip[] music;
        switch (musicType.ToLower())
        {
            case "boss":
                music = boss;
                break;
            case "enemyroom":
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
                music = null;
                break;
        }
        int random = Random.Range(0, music.Length);
        source.clip = music[random];
        source.Play();
    }
}
