// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public float Damage;
    public float ResetHitSpeed = 1f;
    private bool canHit = true;

    private Rigidbody2D rb;

    // Give player damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canHit)
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Damage(Damage);
        }
    }

    private IEnumerator RepeatHit()
    {
        canHit = false;
        yield return new WaitForSeconds(ResetHitSpeed);
        canHit = true;
    }
}
