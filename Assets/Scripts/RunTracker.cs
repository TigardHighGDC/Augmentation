// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTracker : MonoBehaviour
{
    public static RunTracker Instance { get; private set; }

    [HideInInspector]
    public int LevelsCompleted = 0;
    [HideInInspector]
    public int BossesDefeated = 0;
    [HideInInspector]
    public int MoneyCollected = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LevelCompleted()
    {
        Debug.Log("Level Completed");

        LevelsCompleted++;

        MoneyCollected += 5;
    }

    public void BossDefeated()
    {
        BossesDefeated++;

        MoneyCollected += 25;
    }

    public void Reset()
    {
        LevelsCompleted = 0;
        BossesDefeated = 0;
        MoneyCollected = 0;
    }
}
