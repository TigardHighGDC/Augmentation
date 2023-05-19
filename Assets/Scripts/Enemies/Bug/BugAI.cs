// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI : EnemyAI
{
    public AudioClip BeginMoveSound;
    public AudioClip LoopMoveSound;

    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Volume;
        aiPath = GetComponent<AIPhysics>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource.PlayOneShot(BeginMoveSound);
        audioSource.clip = LoopMoveSound;
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            audioSource.Pause();
            return;
        }
        // Movement animation
        anim.speed = (Mathf.Abs(aiPath.direction[0]) + Mathf.Abs(aiPath.direction[1])) / 2;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (aiPath.direction[0] >= 0.5)
        {
            transform.localScale = new Vector3(1, transform.localScale[1], transform.localScale[2]);
        }
        else if (aiPath.direction[0] <= -0.5)
        {
            transform.localScale = new Vector3(-1, transform.localScale[1], transform.localScale[2]);
        }
    }

    private void FixedUpdate()
    {
        aiPath.DesiredLocation = player.transform.position;
    }
}
