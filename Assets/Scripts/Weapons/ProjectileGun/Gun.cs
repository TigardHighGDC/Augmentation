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

    [HideInInspector]
    public int AmmoAmount;

    private bool reloading = false;
    private bool shotDelay = false;
    private AudioSource audioPlayer;

    private void Start()
    {
        AmmoAmount = Data.AmmoCapacity;
        audioPlayer = gameObject.GetComponent<AudioSource>();
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

        if (!reloading && !shotDelay && AmmoAmount > 0 && Input.GetButton("Fire1"))
        {
            Fire();
            StartCoroutine(CanShoot());
        }
        else if (Data.AutoReload && !reloading && AmmoAmount <= 0)
        {
            StartCoroutine((Reload()));
        }
    }

    private void Fire()
    {
        // Get player angle relative to mouse
        Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePoint = transform.position - mousePosition;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;

        // Plays sound effect
        audioPlayer.PlayOneShot(Data.GunShotSound, Data.GunShotVolume);

        // Spawn bullets
        for (int i = 0; i < Data.BulletPerTrigger; i++)
        {
            Quaternion eulerAngle = Quaternion.Euler(0, 0, rotation + Random.Range(-Data.Spread, Data.Spread));
            GameObject bullet = Instantiate(Bullet, SpawnPoint.position, eulerAngle);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Bullet>().Data = Data;
            bullet.transform.localScale = new Vector3(Data.Size, Data.Size, 1);
            rb.velocity = bullet.transform.up * Data.BulletSpeed;
        }
    }

    private IEnumerator Reload()
    {
        reloading = true;
        audioPlayer.PlayOneShot(Data.ReloadSound, Data.ReloadVolume);
        Debug.Log("Reloading"); // TODO: Remove Debug.Log() when we have a working interface.

        // Yield is required to pause the function
        yield return new WaitForSeconds(Data.ReloadSpeed);
        Debug.Log("Done"); // TODO: Remove Debug.Log() when we have a working interface.
        AmmoAmount = Data.AmmoCapacity;
        reloading = false;
    }

    private IEnumerator CanShoot()
    {
        shotDelay = true;
        AmmoAmount -= 1;

        // Yield is required to pause the function
        yield return new WaitForSeconds(Data.CanShootInterval);
        shotDelay = false;
    }
}
