// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarRoom : MonoBehaviour
{
    public float MaxTime = 30.0f;
    public Vector3 WeaponSpawnPosition;

    private Slider slider;
    private bool endLoading = false;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = MaxTime;
        slider.value = MaxTime;
    }

    private void Update()
    {
        if (!endLoading)
        {
            SliderIncrease();
        }
    }

    private void SliderIncrease()
    {
        slider.value -= Time.deltaTime;

        if (slider.value <= 0.0f)
        {
            endLoading = true;
            GameObject[] weapons = Resources.LoadAll<GameObject>("PickupableWeapon");
            Instantiate(weapons[Random.Range(0, weapons.Length)], WeaponSpawnPosition, Quaternion.identity);

            EnemySpawner.LoadingBarFinished = true;
            slider.value = 0.0f;
        }
    }
}
