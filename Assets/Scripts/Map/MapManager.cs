// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class MapManager : MonoBehaviour
{
    public MapConfig Config;
    public MapView View;
    public bool GenerateNewMapOnStart = true; // Always override the saved map

    public Map CurrentMap { get; private set; }

    private void Start()
    {
        // This prevents the generating of a new map until the player reaches the boss
        if (PlayerPrefs.HasKey("Map") && !GenerateNewMapOnStart)
        {
            var mapJson = PlayerPrefs.GetString("Map");
            var map = JsonConvert.DeserializeObject<Map>(mapJson);

            if (map == null || map.Path.Any(p => p.Equals(map.GetBossNode().Point)))
            {
                GenerateNewMap();
            }
            else
            {
                CurrentMap = map;
                View.ShowMap(map);
            }
        }
        else
        {
            GenerateNewMap();
        }
    }

    public void GenerateNewMap()
    {
        var map = MapGenerator.GetMap(Config);
        CurrentMap = map;
        Debug.Log(map.ToJson());
        View.ShowMap(map);
    }

    public void SaveMap()
    {
        if (CurrentMap == null)
        {
            return;
        }

        var json = JsonConvert.SerializeObject(
            CurrentMap, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        PlayerPrefs.SetString("Map", json);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveMap();
    }
}
