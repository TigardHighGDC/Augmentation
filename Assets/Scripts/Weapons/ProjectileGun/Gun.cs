// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponData Data;
    public GameObject Bullet;
    public Camera Camera;
    public Transform SpawnPoint;

    private bool reloading = false;
    private bool shotDelay = false;
    private int ammoAmount;

    private void Start()
    {
        ammoAmount = Data.AmmoCapacity;
    }

    private void Update()
    {
        Controller();
    }

    private void Controller()
    {
        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            StartCoroutine((Reload()));
        }
        if (!reloading && !shotDelay && ammoAmount > 0 && Input.GetButton("Fire1"))
        {
            Fire();
            StartCoroutine(CanShoot());
        }
        else if (Data.AutoReload && !reloading && ammoAmount <= 0)
        {
            StartCoroutine((Reload()));
        }
    }

    private void Fire()
    {
        // Get player angle relative to mouse.
        Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePoint = transform.position - mousePosition;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;

        // Spawn bullets.
        for (int i = 0; i < Data.BulletPerTrigger; i++)
        {
            Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation + Random.Range(-Data.Spread, Data.Spread));
            GameObject bullet = Instantiate(Bullet, SpawnPoint.position, eulerAngle);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Bullet>().Data = Data;
            rb.velocity = bullet.transform.up * Data.BulletSpeed;
        }
    }

    private IEnumerator Reload()
    {
        reloading = true;
        Debug.Log("Reloading"); // TODO: Remove Debug.Log() when we have a working interface.

        // Yield is required to pause the function.
        yield return new WaitForSeconds(Data.ReloadSpeed);
        Debug.Log("Done"); // TODO: Remove Debug.Log() when we have a working interface.
        ammoAmount = Data.AmmoCapacity;
        reloading = false;
    }

    private IEnumerator CanShoot()
    {
        shotDelay = true;
        ammoAmount -= Data.BulletPerTrigger;

        // Yield is required to pause the function.
        yield return new WaitForSeconds(Data.BulletPerSecond);
        shotDelay = false;
    }
}
