using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EY_AI : EnemyAI
{
    public float BeginChase;
    public float EndChase;

    // Chase needs to be larger than to close chase
    public float TooCloseBeginChase;
    public float TooCloseEndChase;

    private bool chase = false;
    private bool tooCloseChase = false;

    private void Update()
    {
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
        if (chase)
        {
            if (EndChase > distance)
            {
                chase = false;
            }
        }
        else if (tooCloseChase)
        {
            if (TooCloseEndChase < distance)
            {
                tooCloseChase = false;
            }
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


}
