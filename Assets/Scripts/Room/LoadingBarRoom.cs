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
    public bool EndLoading = false;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = MaxTime;
        slider.value = MaxTime;
    }

    private void Update()
    {
        if (!EndLoading)
        {
            SliderIncrease();
        }
    }

    private void SliderIncrease()
    {
        slider.value -= Time.deltaTime;

        if (slider.value <= 0.0f)
        {
            EndLoading = true;
            GameObject[] weapons = Resources.LoadAll<GameObject>("PickupableWeapon");
            Instantiate(weapons[Random.Range(0, weapons.Length)], WeaponSpawnPosition, Quaternion.identity);

            EnemySpawner.LoadingBarFinished = true;
            slider.value = 0.0f;
        }
    }
}
