// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI LevelCountText;
    public TextMeshProUGUI BossCountText;
    public TextMeshProUGUI MoneyText;

    public void Start()
    {
        LevelCountText.text = RunTracker.Instance.LevelsCompleted.ToString();
        BossCountText.text = RunTracker.Instance.BossesDefeated.ToString();
        MoneyText.text = RunTracker.Instance.MoneyCollected.ToString();

        PlayerStatManager.Instance.PlayerStats.Money += RunTracker.Instance.MoneyCollected;
        PlayerStatManager.SavePlayerStats(PlayerStatManager.Instance.PlayerStats);
    }

    public void OnContinueButton()
    {
        AsyncSceneLoader.GetInstance().LoadScene("MainMenu");
    }
}
