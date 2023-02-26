using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPhysics : MonoBehaviour
{   
    public float MaxSpeed = 1f;
    public float Brake = 2f;
    public float nextWaypointDistance = 3f;
    public bool isStopped = false;

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

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void FixedUpdate() 
    {
        if (path != null)
        {
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
            if (distance < nextWaypointDistance && currentWaypoint < path.vectorPath.Count - 1)
            {
                currentWaypoint += 1;
            }

            if (!isStopped)
            {
                BrakeSpeed();
            }
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

    private void BrakeSpeed()
    {
        path.vectorPath.ForEach(p => Debug.Log(p));
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 directionChange = new Vector2(0, 0);

        float distance = (direction[0] * MaxSpeed) - rb.velocity[0]; 
        float max = Mathf.Min(Mathf.Abs((direction[0] * Brake) / distance), 1);
        directionChange[0] = Mathf.Lerp(rb.velocity[0], direction[0] * MaxSpeed, max);

        distance = (direction[1] * MaxSpeed) - rb.velocity[1]; 
        max = Mathf.Min(Mathf.Abs((direction[1] * Brake) / distance), 1);
        directionChange[1] = Mathf.Lerp(rb.velocity[1], direction[1] * MaxSpeed, max);

        rb.velocity = directionChange;
    }

}
