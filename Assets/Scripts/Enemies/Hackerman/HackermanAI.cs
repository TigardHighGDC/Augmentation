// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackermanAI : EnemyAI
{
    public AudioClip ShootSound;

    private bool canShoot = true;

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(BeginFiring());
        }
    }

    private IEnumerator BeginFiring()
    {
        canShoot = false;
        audioSource.PlayOneShot(ShootSound);
        Fire();
        yield return new WaitForSeconds(Data.BulletPerSecond);
        canShoot = true;
    }
}
