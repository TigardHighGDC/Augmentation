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

    private bool chase = false;
    private bool tooCloseChase = false;
    private bool canShoot = true;

    private void Update()
    {
        if (BeginChase + 5f > DistanceBetweenPlayer() && canShoot)
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
        else
        {
            if (TooCloseBeginChase > distance)
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
    }

    private IEnumerator BeginShot()
    {
        // Attack separated in phases
        canShoot = false;
        laser.DrawLine = false;
        yield return new WaitForSeconds(3f);
        laser.DrawLine = true;
        yield return new WaitForSeconds(2f);
        laser.DrawLine = false;
        canShoot = true;
        Fire();
    }
}
