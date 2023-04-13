// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public WeaponData Data;

    private int remainingPierce;

    private void Start()
    {
        Invoke("DestroyBullet", Data.DespawnTime);
        remainingPierce = Data.BulletPierce;
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        switch (collide.gameObject.tag)
        {
        case "Enemy":
            NonPlayerHealth nonPlayerHealth = collide.gameObject.GetComponent<NonPlayerHealth>();
            collide.GetComponent<Rigidbody2D>().AddForce(
                transform.up * (Data.Knockback * CorruptionLevel.KnockbackIncrease), ForceMode2D.Impulse);
            nonPlayerHealth.Damage(Data.Damage);
            ItemHandling.BulletHit?.Invoke();
            remainingPierce--;

            if (remainingPierce <= 0)
            {
                DestroyBullet();
            }

            break;
        case "Wall":
            DestroyBullet();
            break;
        }
    }

    // DestroyBullet() is called in the invoke function
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
