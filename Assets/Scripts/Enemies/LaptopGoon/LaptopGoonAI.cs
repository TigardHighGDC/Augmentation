// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LaptopGoonAI : BasicEnemy
{
    public GameObject Bullet;
    public float BulletPerSecond;
    public float BulletSpeed;
    public float DangerDistance;
    public float FollowDistance;
    public float RunAwayDistance;
    public float Damage;

    private AIPath aiPath;
    private AIDestinationSetter locationSetter;
    private Seeker seeker;

    private bool runAway = false;
    private bool canFire = true;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();
    }

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
        aiPath.isStopped = false;
        float distance = DistanceBetweenPlayer(Player);

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
            seeker.StartPath(transform.position, (2 * transform.position) - Player.transform.position);
        }
        else
        {
            if (distance > FollowDistance)
            {
                seeker.StartPath(transform.position, Player.transform.position);
            }
            else
            {
                aiPath.isStopped = true;
            }
        }
    }

    // Calculates the distance between the player and enemy.
    private float DistanceBetweenPlayer(GameObject player)
    {
        float y2 = player.transform.position.y;
        float y1 = transform.position.y;
        float x2 = player.transform.position.x;
        float x1 = transform.position.x;
        return Mathf.Sqrt(Mathf.Pow(y2 - y1, 2) + Mathf.Pow(x2 - x1, 2));
    }

    private void LookTowardsMovement()
    {
        // Look towards the player if stopped and if not look towards the direction it is following.
        if (aiPath.isStopped)
        {
            if (transform.position[0] < Player.transform.position[0])
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (aiPath.desiredVelocity.x >= 0.15f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (aiPath.desiredVelocity.y <= 0.15f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator Fire()
    {
        canFire = false;

        // Get angle to fire at player and convert to euler.
        Vector3 relativePoint = transform.position - Player.transform.position;
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
