// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerStatManager : MonoBehaviour
{
    public PlayerStats PlayerStats;

    private static PlayerStatManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (PlayerStats == null)
        {
            PlayerStats = GetPlayerStats();
        }

        DontDestroyOnLoad(gameObject);
    }

    public static PlayerStatManager GetInstance()
    {
        return instance;
    }

    public static void SavePlayerStats(PlayerStats playerStats)
    {
        string json = JsonConvert.SerializeObject(
            playerStats, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        PlayerPrefs.SetString("PlayerStats", json);
        PlayerPrefs.Save();
    }

    private static PlayerStats GetPlayerStats()
    {
        if (!PlayerPrefs.HasKey("PlayerStats"))
        {
            return null;
        }

        string playerStatsJson = PlayerPrefs.GetString("PlayerStats");
        PlayerStats playerStats = JsonConvert.DeserializeObject<PlayerStats>(playerStatsJson);

        if (playerStats == null)
        {
            return null;
        }

        return playerStats;
    }
}
