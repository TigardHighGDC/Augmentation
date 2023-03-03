// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LaptopGoonAI : EnemyAI
{
    public float DangerDistance;
    public float FollowDistance;
    public float RunAwayDistance;

    private bool runAway = false;
    private bool canFire = true;

    private void Update()
    {
        if (!runAway && canFire)
        {
            StartCoroutine(CanFire());
        }

        LookTowardsMovement();
        keepDistance();
    }

    private void keepDistance()
    {
        aiPath.IsStopped = false;
        float distance = DistanceBetweenPlayer();

        if (runAway && RunAwayDistance <= distance)
        {
            runAway = false;
        }
        else if (!runAway && DangerDistance >= distance)
        {
            runAway = true;
        }

        if (runAway)
        {
            // Goes opposite direction of player
            aiPath.DesiredLocation = (2 * transform.position) - player.transform.position;
        }
        else
        {
            if (distance > FollowDistance)
            {
                aiPath.DesiredLocation = player.transform.position;
            }
            else
            {
                aiPath.IsStopped = true;
            }
        }
    }

    private void LookTowardsMovement()
    {
        // Look towards the player if stopped and if not look towards the direction it is following.
        if (aiPath.IsStopped)
        {
            if (transform.position[0] < player.transform.position[0])
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (rb.velocity[0] >= 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity[0] <= -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator CanFire()
    {
        canFire = false;
        Fire();
        yield return new WaitForSeconds(Data.BulletPerSecond);
        canFire = true;
    }
}
