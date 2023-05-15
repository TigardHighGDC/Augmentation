// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variables changeable by items
    public static float C_Damage = 1.0f;
    public static int C_BulletPierce = 1;
    public static int C_BulletBounce = 0;
    public static float C_Knockback = 1.0f;

    [HideInInspector]
    public WeaponData Data;

    private Rigidbody2D rb;
    private int remainingPierce;
    private int remainingBounce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyBullet", Data.DespawnTime);
        remainingPierce = Data.BulletPierce * C_BulletPierce;
        remainingBounce = C_BulletBounce;
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        switch (collide.gameObject.tag)
        {
        case "Enemy":
            NonPlayerHealth nonPlayerHealth = collide.gameObject.GetComponent<NonPlayerHealth>();
            collide.GetComponent<Rigidbody2D>().AddForce(
                transform.up * (Data.Knockback * CorruptionLevel.KnockbackIncrease * C_Knockback), ForceMode2D.Impulse);
            nonPlayerHealth.Damage(Data.Damage * C_Damage);
            remainingPierce--;

            if (remainingPierce <= 0)
            {
                DestroyBullet();
            }
            
            break;
        case "Wall":
            if (remainingBounce > 0)
            {
                remainingBounce--;
                BounceBullet(collide);
            }
            else
            {
                DestroyBullet();
            }
            break;
        }
    }

    private void BounceBullet(Collider2D collide)
    {
        rb.velocity = Vector3.Reflect(rb.velocity, collide.transform.up);
    }

    // DestroyBullet() is called in the invoke function
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
