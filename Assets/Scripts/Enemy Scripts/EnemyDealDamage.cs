// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    Rigidbody2D rb;
    public float Damage;

    // Player takes damage.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Damage(damage);
        }
    }
}
