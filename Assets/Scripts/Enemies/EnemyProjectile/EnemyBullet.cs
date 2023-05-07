// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector]
    public EnemyProjectileData Data;
    private bool hit = false;

    private void Start()
    {
        Invoke("DestroyBullet", 20.0f);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player" && !hit)
        {
            hit = true;
            PlayerHealth playerHealth = collide.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Damage(Data.Damage);
            DestroyBullet();
        }
        else if (collide.gameObject.tag == "Wall")
        {
            DestroyBullet();
        }
    }

    // DestroyBullet() is called in the invoke function
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
