// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variables changeable by items
    public static float C_Damage = 1f;
    public static int C_BulletPierce = 1;
    public static float C_Knockback = 1f;
    [HideInInspector] public WeaponData Data;

    public AudioClip PlayerHitSFX;
    public AudioClip EnemyHitSFX;

    private int remainingPierce;
    private AudioSource audio;

    private void Start()
    {
        Invoke("DestroyBullet", Data.DespawnTime);
        audio = GameObject.FindWithTag("UI_SFX").GetComponent<AudioSource>();
        remainingPierce = Data.BulletPierce;
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

            audio.clip = EnemyHitSFX;
            audio.Play();

            remainingPierce--;
            if (remainingPierce * C_BulletPierce <= 0)
            {
                DestroyBullet();
            }
            break;
        case "Wall":
            DestroyBullet();
            break;
        case "Player":
            PlayerHealth PlayerHealth = collide.gameObject.GetComponent<PlayerHealth>();
            PlayerHealth.Damage(Data.Damage * C_Damage);

            audio.clip = PlayerHitSFX;
            audio.Play();

            remainingPierce--;
            if (remainingPierce * C_BulletPierce <= 0)
            {
                DestroyBullet();
            }
            break;
        }
    }

    // DestroyBullet() is called in the invoke function
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
