using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI : EnemyAI
{
    private void Update()
    {
        if (rb.velocity[0] >= 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity[0] <= -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        aiPath.DesiredLocation = player.transform.position;
    }
}
