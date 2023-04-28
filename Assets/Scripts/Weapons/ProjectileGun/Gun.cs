// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponData Data;
    public GameObject Bullet;
    public Camera Camera;
    public Transform SpawnPoint;

    private AmmoCounter ammoCounter;
    private bool reloading = false;
    private bool shotDelay = false;
    private int ammoAmount;
    private AudioSource audioPlayer;

    private void Start()
    {
        ammoAmount = Data.AmmoCapacity;
        audioPlayer = gameObject.GetComponent<AudioSource>();
        ammoCounter = GetComponent<AmmoCounter>();
    }

    private void Update()
    {
        ammoCounter.Text(Data, ammoAmount);
        Controller();
    }

    private void Controller()
    {
        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            StartCoroutine((Reload()));
        }

        if (!reloading && !shotDelay && !PauseMenu.GameIsPaused && ammoAmount > 0 && Input.GetButton("Fire1"))
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
        // Get player angle relative to mouse
        Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePoint = transform.position - mousePosition;
        float rotation = Mathf.Atan2(relativePoint.y, relativePoint.x) * Mathf.Rad2Deg + 90;

        // Plays sound effect
        if (CorruptionLevel.currentCorruption >= 50.0f)
        {
            // Start 12.5 -> 10.5
            float completion = (CorruptionLevel.currentCorruption - 50.0f) / 50.0f;
            float compression = 12f - (2f * (completion));
            audioPlayer.PlayOneShot(AudioManipulation.BitCrusher(Data.GunShotSound, compression),
                                    Data.GunShotVolume * (3.5f * completion + 3f));
        }
        else
        {
            audioPlayer.PlayOneShot(Data.GunShotSound, Data.GunShotVolume);
        }

        // Spawn bullets
        for (int i = 0; i < Data.BulletPerTrigger; i++)
        {
            Quaternion eulerAngle =
                Quaternion.Euler(0, 0,
                                 rotation + Random.Range(-(Data.Spread * CorruptionLevel.AccuracyDecrease),
                                                         (Data.Spread * CorruptionLevel.AccuracyDecrease)));
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
        ammoAmount = Data.AmmoCapacity;
        reloading = false;
    }

    private IEnumerator CanShoot()
    {
        shotDelay = true;
        ammoAmount -= 1;

        // Yield is required to pause the function
        yield return new WaitForSeconds(Data.CanShootInterval * CorruptionLevel.ShootIntervalDecrease);
        shotDelay = false;
    }
}
