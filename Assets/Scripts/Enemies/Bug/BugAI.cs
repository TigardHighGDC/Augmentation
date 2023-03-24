// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI : EnemyAI
{
    private void MovementAnimation()
    {
        anim.speed = (Mathf.Abs(aiPath.direction[0]) + Mathf.Abs(aiPath.direction[1])) / 2;
    }

    private void Update()
    {
        MovementAnimation();
        if (aiPath.direction[0] >= 0.5)
        {
            transform.localScale = new Vector3(1, transform.localScale[1], transform.localScale[2]);
        }
        else if (aiPath.direction[0] <= -0.5)
        {
            transform.localScale = new Vector3(-1, transform.localScale[1], transform.localScale[2]);
        }
    }

    private void FixedUpdate()
    {
        aiPath.DesiredLocation = player.transform.position;
    }
}
