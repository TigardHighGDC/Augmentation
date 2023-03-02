// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LaptopGoonAI : EnemyAI
{
    public GameObject Bullet;
    public float BulletPerSecond;
    public float BulletSpeed;
    public float DangerDistance;
    public float FollowDistance;
    public float RunAwayDistance;
    public float Damage;

    private bool runAway = false;
    private bool canFire = true;

    private void Update()
    {
        if (!runAway && canFire)
        {
            StartCoroutine(Fire());
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

    private IEnumerator Fire()
    {
        canFire = false;

        // Get angle to fire at player and convert to euler.
        Vector3 relativePoint = transform.position - player.transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;
        Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation);

        // Spawn bullet and provide needed values.
        GameObject bullet = Instantiate(Bullet, transform.position, eulerAngle);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<LaptopGoonBullet>().Damage = Damage;
        rb.velocity = bullet.transform.up * BulletSpeed;

        // Yield is required to pause the function.
        yield return new WaitForSeconds(BulletPerSecond);
        canFire = true;
    }
}
