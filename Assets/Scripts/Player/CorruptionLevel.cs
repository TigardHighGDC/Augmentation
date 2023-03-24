// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionLevel : MonoBehaviour
{
    private static float corruptionMax = 100f;
    private static float currentCorruption = 0f;
    public static float KnockbackIncrease;
    public static float AccuracyDecrease;
    public static float ShootIntervalDecrease;

    public void Add(float increase)
    {
        currentCorruption = Mathf.Min(corruptionMax, currentCorruption + increase);
    }

    public float GetLevel()
    {
        return currentCorruption;
    }

    private float PercentageIncrease(float maxIncrease)
    {
        float rate = (currentCorruption / corruptionMax) * maxIncrease;
        return rate;
    }

    private void UpdateEffects()
    {
        KnockbackIncrease = 1.0f + PercentageIncrease(0.65f);
        AccuracyDecrease = 1.0f + PercentageIncrease(1.2f);
        ShootIntervalDecrease = 1.0f - PercentageIncrease(0.5f);
    }

    private void Update()
    {
        UpdateEffects();
    }
}
