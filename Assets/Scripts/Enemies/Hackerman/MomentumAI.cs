// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MomentumAI : MonoBehaviour
{
    public float Speed;
    public float MaxSpeed;
    public float UpdatePathRate = 0.5f;
    [HideInInspector]
    public Vector2 direction = new Vector2(0, 0);

    private float NextWaypointDistance = 3.0f;
    private GameObject player;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0.0f, UpdatePathRate);
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < NextWaypointDistance && currentWaypoint < path.vectorPath.Count - 1)
        {
            currentWaypoint += 1;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.AddForce(direction * Speed, ForceMode2D.Impulse);

        // Lowers the speed if it goes over the max
        float totalSpeed = Mathf.Abs(rb.velocity[0]) + Mathf.Abs(rb.velocity[1]);

        if (totalSpeed > MaxSpeed)
        {
            float limit = totalSpeed / MaxSpeed;
            rb.velocity = new Vector2(rb.velocity[0] / limit, rb.velocity[1] / limit);
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
