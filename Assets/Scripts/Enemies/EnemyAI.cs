// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public EnemyProjectileData Data;
    public Vector3 BulletSpawn = new Vector3(0, 0, 0);
    public float Volume = 1.0f;

    protected AudioSource audioSource;
    protected GameObject player;
    protected AIPhysics aiPath;
    protected Rigidbody2D rb;
    protected Animator anim;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Volume;
        aiPath = GetComponent<AIPhysics>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected float DistanceBetweenPlayer()
    {
        float y2 = player.transform.position.y;
        float y1 = transform.position.y;
        float x2 = player.transform.position.x;
        float x1 = transform.position.x;
        return Mathf.Sqrt(Mathf.Pow(y2 - y1, 2) + Mathf.Pow(x2 - x1, 2));
    }

    protected void Fire()
    {
        // Get angle to fire at player and convert to euler
        Vector3 relativePoint = (transform.position + BulletSpawn) - player.transform.position;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;
        Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation);

        // Spawn bullet and provide needed values
        GameObject bullet = Instantiate(Data.BulletPrefab, transform.position + BulletSpawn, eulerAngle);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<EnemyBullet>().Data = Data;
        rb.velocity = bullet.transform.up * Data.BulletSpeed;
    }
}
