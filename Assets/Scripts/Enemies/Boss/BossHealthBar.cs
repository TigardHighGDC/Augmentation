// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider Slider;
    public EnemyHealth enemyHealth;

    private void Start()
    {
        SetMaxHealth(enemyHealth.Health);
    }

    private void Update()
    {
        Slider.value = enemyHealth.Health;
    }

    // Set the number that it considered the max.
    public void SetMaxHealth(float health)
    {
        Slider.maxValue = health;
        Slider.value = health;
    }
}
