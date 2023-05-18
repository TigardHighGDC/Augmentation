// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerStatManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerStats PlayerStats;
    public PlayerStats DefaultPlayerStats;

    private void Awake()
    {
        PlayerStats = GetPlayerStats();

        if (PlayerStats == null)
        {
            PlayerStats = DefaultPlayerStats;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player 1");
        player.GetComponent<PlayerHealth>().Health = PlayerStats.MaxHealth;
        player.GetComponent<WeaponInventory>().NewWeapons = PlayerStats.StartingWeapons;
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

    private void OnApplicationQuit()
    {
        SavePlayerStats(PlayerStats);
    }
}
