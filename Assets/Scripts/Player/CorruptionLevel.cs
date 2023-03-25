// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionLevel : MonoBehaviour
{
    public static float corruptionMax = 100f;
    public static float currentCorruption = 0f;

    public void Add(float increase)
    {
        currentCorruption = Mathf.Min(corruptionMax, currentCorruption + increase);
    }

    public float GetLevel()
    {
        return currentCorruption;
    }
}
