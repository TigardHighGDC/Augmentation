// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionWellSelector : MonoBehaviour
{
    private bool canUse = true;
    private float time = 0f;
    private void OnTriggerStay2D(Collider2D collide)
    {
        if (!canUse)
        {
            return;
        }
        if (collide.tag == "Player" && Input.GetKeyDown(KeyCode.F) && time <= 0.0f)
        {
            time = 1.0f;
            EnemySpawner spawn = GetComponent<EnemySpawner>();
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            if (spawn.Ambush())
            {
                canUse = false;
                MusicPlayer playerMusic = GameObject.FindGameObjectWithTag("Music Player").GetComponent<MusicPlayer>();
                playerMusic.SuddenTransition("Enemy Room", "Enemy Transition");
            }
        }
    }
    private void Update()
    {
        time -= Time.deltaTime;
    }
}
