// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackermanAI : EnemyAI
{
    public AudioClip ShootSound;

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

        if (canShoot)
        {
            StartCoroutine(BeginFiring());
        }
    }

    private IEnumerator BeginFiring()
    {
        canShoot = false;
        audioSource.clip = ShootSound;
        audioSource.Play();
        Fire();
        yield return new WaitForSeconds(Data.BulletPerSecond);
        canShoot = true;
    }
}
