// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class AssetStore : MonoBehaviour
{
    public List<WeaponData> AvailableWeapons;
    public int HealthUpgradeCost = 10;
    public int PistolCost = 10;
    public int ShotgunCost = 20;
    public int SniperCost = 30;
    public int AssaultRifleCost = 40;

    public TextMeshProUGUI PlayerMoneyText;

    private bool hasBought = false;
    private PlayerStats playerStats;

    private float healthIncrease = 50.0f;
    private float maxHealthCap = 300.0f;

    private void Start()
    {
        playerStats = PlayerStatManager.Instance.PlayerStats;
    }

    private void Update()
    {
        if (playerStats == null)
        {
            PlayerMoneyText.text = "$0";
        }
        else
        {
            PlayerMoneyText.text = "$" + playerStats.Money.ToString();
        }
    }

    public void OnHealthUpgradeButton()
    {
        Debug.Log("Health Upgrade Button Pressed");

        if (playerStats.Money >= HealthUpgradeCost && playerStats.MaxHealth + healthIncrease < maxHealthCap)
        {
            playerStats.Money -= HealthUpgradeCost;
            playerStats.MaxHealth += healthIncrease;
            hasBought = true;
            Debug.Log("Health Upgraded");
        }
    }

    public void OnBuyPistolButton()
    {
        if (playerStats.Money < PistolCost)
        {
            return;
        }

        playerStats.Money -= PistolCost;
        playerStats.StartingWeapons.Add("Pistol");
        hasBought = true;
        Debug.Log("Pistol Bought");
    }

    public void OnBuyShotgunButton()
    {
        if (playerStats.Money < ShotgunCost)
        {
            return;
        }

        playerStats.Money -= ShotgunCost;
        playerStats.StartingWeapons.Add("Shotgun");
        hasBought = true;
        Debug.Log("Shotgun Bought");
    }

    public void OnBuySniperButton()
    {
        if (playerStats.Money < SniperCost)
        {
            return;
        }

        playerStats.Money -= SniperCost;
        playerStats.StartingWeapons.Add("Sniper");
        hasBought = true;
        Debug.Log("Sniper Bought");
    }

    public void OnBuyAssaultRifleButton()
    {
        if (playerStats.Money < AssaultRifleCost)
        {
            return;
        }

        playerStats.Money -= AssaultRifleCost;
        playerStats.StartingWeapons.Add("Assault Rifle");
        hasBought = true;
        Debug.Log("Assault Rifle Bought");
    }

    public void OnContinueButton()
    {
        if (hasBought)
        {
            PlayerStatManager.SavePlayerStats(playerStats);
        }

        AsyncSceneLoader.GetInstance().LoadScene("MainMenu");
    }
}
