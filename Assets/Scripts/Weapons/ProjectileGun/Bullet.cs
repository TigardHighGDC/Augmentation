// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;

    [HideInInspector]
    public WeaponData Data;

    private void Start()
    {
        Invoke("DestroyBullet", Data.DespawnTime);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag != "Bullet")
        {
            EnemyHealth enemyHealth = collide.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Damage(damage);
            Destroy(collide.gameObject);
        }
    }

    // DestroyBullet() is called in the invoke function.
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
