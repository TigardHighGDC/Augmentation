// Copyright (c) TigardHighGDC
// SPDX-License SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerStats
{
    public float MaxHealth = 150.0f;
    public List<WeaponData> StartingWeapons;
    public int Money = 0;
    public int BossDefeats = 0;

    public PlayerStats(float maxHealth, List<WeaponData> startingWeapons)
    {
        MaxHealth = maxHealth;
        StartingWeapons = startingWeapons;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(
            this, Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    }
}
