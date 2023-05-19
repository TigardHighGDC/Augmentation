// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EY_AI : EnemyAI
{
    public LaserLine laser;

    public float BeginChase;
    public float EndChase;

    // Chase needs to be larger than to close chase
    public float TooCloseBeginChase;
    public float TooCloseEndChase;

    public AudioClip LockOnSound;
    public AudioClip ShootSound;

    private bool chase = false;
    private bool tooCloseChase = false;
    private bool canShoot = true;
    private bool wasPaused = true;

    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            wasPaused = true;
            audioSource.Pause();
            return;
        }

        if (wasPaused)
        {
            audioSource.Play();
            wasPaused = false;
        }

        if (BeginChase + 5.0f > DistanceBetweenPlayer() && canShoot)
        {
            StartCoroutine(BeginShot());
        }
        if (aiPath.direction[0] >= 0.5)
        {
            transform.localScale = new Vector3(-1, transform.localScale[1], transform.localScale[2]);
        }
        else if (aiPath.direction[0] <= -0.5)
        {
            transform.localScale = new Vector3(1, transform.localScale[1], transform.localScale[2]);
        }
    }

    private void FixedUpdate()
    {
        aiPath.DesiredLocation = player.transform.position;
        float distance = DistanceBetweenPlayer();

        if (chase && EndChase > distance)
        {
            chase = false;
        }
        else if (tooCloseChase && TooCloseEndChase < distance)
        {
            tooCloseChase = false;
        }
        else if (TooCloseBeginChase > distance)
        {
            tooCloseChase = true;
            aiPath.IsStopped = false;
        }
        else if (BeginChase < distance)
        {
            chase = true;
            aiPath.IsStopped = false;
        }
        else
        {
            chase = false;
            tooCloseChase = false;
            aiPath.IsStopped = true;
        }
    }

    private IEnumerator BeginShot()
    {
        // Attack separated in phases
        canShoot = false;
        laser.DrawLine = false;
        yield return new WaitForSeconds(3.0f);

        audioSource.clip = LockOnSound;
        audioSource.Play();
        laser.DrawLine = true;
        yield return new WaitForSeconds(2.0f);

        audioSource.clip = ShootSound;
        audioSource.Play();
        laser.DrawLine = false;
        canShoot = true;
        Fire();
    }
}
