// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerStatManager : MonoBehaviour
{
    public static void SavePlayerStats(PlayerStats playerStats)
    {
        string json = JsonConvert.SerializeObject(
            playerStats, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        PlayerPrefs.SetString("PlayerStats", json);
        PlayerPrefs.Save();
    }

    public static PlayerStats GetPlayerStats()
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
