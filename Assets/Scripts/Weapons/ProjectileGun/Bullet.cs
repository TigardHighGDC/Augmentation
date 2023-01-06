// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        Invoke("DestroyBullet", 10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag != "Bullet")
        {
            var enemy = collide.gameObject.GetComponent<EnemyHealth>();
            enemy.Damage(damage);
        }
    }

    // DestroyBullet() is called in the invoke function.
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
