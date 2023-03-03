// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopGoonBullet : MonoBehaviour
{
    [HideInInspector]
    public EnemyProjectileData Data;

    private void Start()
    {
        Invoke("DestroyBullet", 20f);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collide.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Damage(Data.Damage);
            DestroyBullet();
        }
    }

    // DestroyBullet() is called in the invoke function.
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
