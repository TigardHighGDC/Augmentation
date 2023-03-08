// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPhysics : MonoBehaviour
{
    public float MaxSpeed = 4f;
    public float Brake = 0.8f;
    public float NextWaypointDistance = 3f;
    public bool IsStopped = false;
    public Vector3 DesiredLocation;
    public bool PauseScript = false;
    public float UpdatePathRate = 0.1f;
    [HideInInspector]
    public Vector2 direction = new Vector2(0, 0);

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

        InvokeRepeating("UpdatePath", 0f, UpdatePathRate);
    }

    private void FixedUpdate()
    {
        if (path == null || PauseScript)
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

        direction = ((Vector2)mpath.vectorPath[currentWaypoint] - rb.position).normalized;

        if (!IsStopped)
        {
            BrakeSpeed();
        }
        else
        {
            Stopped();
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, DesiredLocation, OnPathComplete);
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

    private void BrakeSpeed()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 directionChange = new Vector2(0, 0);

        float distance = (direction[0] * MaxSpeed) - rb.velocity[0];

        // Limit for lerp.
        float max = Mathf.Min(Mathf.Abs((direction[0] * Brake) / distance), 1);

        // Edits velocity towards desired velocity.
        directionChange[0] = Mathf.Lerp(rb.velocity[0], direction[0] * MaxSpeed, max);

        distance = (direction[1] * MaxSpeed) - rb.velocity[1];
        max = Mathf.Min(Mathf.Abs((direction[1] * Brake) / distance), 1);
        directionChange[1] = Mathf.Lerp(rb.velocity[1], direction[1] * MaxSpeed, max);

        rb.velocity = directionChange;
    }

    private void Stopped()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 directionChange = new Vector2(0, 0);

        float distance = (direction[0] * MaxSpeed) - rb.velocity[0];
        float max = Mathf.Min(Mathf.Abs((direction[0] * Brake) / distance), 1);
        directionChange[0] = Mathf.Lerp(rb.velocity[0], 0, max);

        distance = (direction[1] * MaxSpeed) - rb.velocity[1];
        max = Mathf.Min(Mathf.Abs((direction[1] * Brake) / distance), 1);
        directionChange[1] = Mathf.Lerp(rb.velocity[1], 0, max);

        rb.velocity = directionChange;
    }
}
