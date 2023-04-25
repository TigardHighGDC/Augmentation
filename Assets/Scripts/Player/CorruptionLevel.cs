// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionLevel : MonoBehaviour
{
    public static bool FragmentationLock = false;
    public static float KnockbackIncrease;
    public static float PlayerSpeedIncrease;
    public static float AccuracyDecrease;
    public static float ShootIntervalDecrease;
    public static float corruptionMax = 100f;
    public static float currentCorruption = 0f;

    public void Add(float increase)
    {
        currentCorruption = Mathf.Min(corruptionMax, currentCorruption + increase);
        currentCorruption = Mathf.Max(currentCorruption, 0.0f);
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
        PlayerSpeedIncrease = 1.0f + PercentageIncrease(0.75f);
    }

    private void FragmentationController()
    {
        // Adds fragmentation when the player reaches level 80. It can no longer be removed when going below 80 if the
        // player reaches max corruption.
        if (!FragmentationLock)
        {
            if (currentCorruption == 100.0f)
            {
                FragmentationLock = true;
            }

            if (currentCorruption >= 80.0f && ItemStorage.Fragmentation == null)
            {
                GameObject[] fragmentationArray = Resources.LoadAll<GameObject>("Item/Fragmentation");
                int random = Random.Range(0, fragmentationArray.Length);
                ItemStorage.Fragmentation = ItemStorage.ReplaceItem(fragmentationArray[random]);
            }

            if (currentCorruption < 80.0f && ItemStorage.Fragmentation != null)
            {
                ItemStorage.DeleteItem(ItemStorage.Fragmentation);
            }
        }
    }

    private void Update()
    {
        UpdateEffects();
        FragmentationController();
    }
}
