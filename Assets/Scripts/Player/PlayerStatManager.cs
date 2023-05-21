// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerStatManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerStats PlayerStats;

    public static PlayerStatManager Instance { get; private set; }

    private GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("PlayerStatManager already exists");
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        PlayerStats = GetPlayerStats();

        if (PlayerStats == null)
        {
            PlayerStats = new PlayerStats();
        }
    }

    public void UpdateState()
    {
        PlayerStats = GetPlayerStats();

        if (PlayerStats == null)
        {
            PlayerStats = new PlayerStats();
        }
    }

    public static void SavePlayerStats(PlayerStats playerStats)
    {
        string json = JsonConvert.SerializeObject(
            playerStats, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        PlayerPrefs.SetString("PlayerStats", json);
        PlayerPrefs.Save();

        Debug.Log("Player stats saved"); // TODO: Remove debug.log
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
