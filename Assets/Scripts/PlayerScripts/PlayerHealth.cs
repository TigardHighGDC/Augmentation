// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    public float MaxTimer;

    private float timer;

    private void Start()
    {
        timer = maxTimer;
    }

    // Invincibility frames (i-frames).
    private void Update()
    {
        if (timer <= maxTimer && timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Damage(float damageAmount)
    {
        if (timer <= 0)
        {
            health = health - damageAmount;
            timer = maxTimer;
        }
    }
}
