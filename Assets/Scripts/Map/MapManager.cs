// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;

    public Map CurrentMap { get; private set; }

    private void Start()
    {
        // This prevents the generating of a new map until the player reaches the boss
        if (PlayerPrefs.HasKey("Map"))
        {
            var mapJson = PlayerPrefs.GetString("Map");
            var map = JsonConvert.DeserializeObject<Map>(mapJson);

            if (map.Path.Any(p => p.Equals(map.GetBossNode().Point)))
            {
                GenerateNewMap();
            }
            else
            {
                CurrentMap = map;
                view.ShowMap(map);
            }
        }
        else
        {
            GenerateNewMap();
        }
    }

    public void GenerateNewMap()
    {
        var map = MapGenerator.GetMap(config);
        CurrentMap = map;
        Debug.Log(map.ToJson());
        view.ShowMap(map);
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
